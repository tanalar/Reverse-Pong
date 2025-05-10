using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform))]
public class NetworkBall : NetworkBehaviour
{
    [Networked] private Vector3 Velocity { get; set; }
    [Networked] private Vector3 NetworkPosition { get; set; }

    private float speed = 2.5f;
    private float radius = 0.05f;
    private bool wall = false;
    private bool gameOver = false;
    private bool playerWin;

    public static Action onBlockDestroyed;
    public static Action onWallTouch;
    public static Action<bool> onTimeStopped;

    private NetworkRunner runner;
    private int collisionMask;

    public override void Spawned()
    {
        runner = Runner;

        collisionMask = LayerMask.GetMask("Player", "Walls");

        if (runner.IsServer)
        {
            Launch();
        }
    }

    private void OnEnable()
    {
        Player.onEnemyWin += EnemyWin;
        Player.onPlayerWin += PlayerWin;
    }

    private void OnDisable()
    {
        Player.onEnemyWin -= EnemyWin;
        Player.onPlayerWin -= PlayerWin;
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority || gameOver) return;

        float deltaTime = Runner.DeltaTime;
        Vector3 direction = Velocity.normalized;
        float distance = speed * deltaTime;

        if (Physics.SphereCast(NetworkPosition, radius, direction, out RaycastHit hit, distance, collisionMask))
        {
            Vector3 reflectNormal = hit.normal;
            reflectNormal.y = 0;  
            reflectNormal.Normalize();

            Vector3 reflected = Vector3.Reflect(direction, reflectNormal);
            reflected.y = 0;
            Velocity = reflected.normalized * speed;

            Vector3 hitPoint = hit.point - reflectNormal * radius;
            hitPoint.y = 0;
            NetworkPosition = hitPoint;

            if (hit.collider.CompareTag("Block"))
            {
                hit.collider.gameObject.SetActive(false);
                onBlockDestroyed?.Invoke();

                if (hit.collider.TryGetComponent<NetworkBlock>(out var block))
                {
                    block.DestroyBlock();
                }
            }
        }
        else
        {
            Vector3 move = Velocity * deltaTime;
            move.y = 0;
            NetworkPosition += move;

            bool bounced = false;

            if (Mathf.Abs(NetworkPosition.x) >= 0.9f)
            {
                var tempVelocity = Velocity;
                tempVelocity.x = -Velocity.x;
                Velocity = tempVelocity;

                var x = Mathf.Sign(NetworkPosition.x) * 0.9f;
                NetworkPosition = new Vector3(x, 0, NetworkPosition.z);
                bounced = true;
            }

            if (Mathf.Abs(NetworkPosition.z) >= 2.25f)
            {
                var tempVelocity = Velocity;
                tempVelocity.z = -Velocity.z;
                Velocity = tempVelocity;

                var z = Mathf.Sign(NetworkPosition.z) * 2.25f;
                NetworkPosition = new Vector3(NetworkPosition.x, 0, z);
                bounced = true;
            }

            if (bounced)
            {
                onWallTouch?.Invoke();
            }
        }

        transform.position = NetworkPosition;

        if (Velocity.magnitude > 10f)
        {
            var tempVelocity = Velocity.normalized * speed;
            tempVelocity.y = 0;
            Velocity = tempVelocity;
        }
    }

    private void Update()
    {
        if (!HasStateAuthority || !runner.IsServer) return;

        if (gameOver)
        {
            if (Time.timeScale > 0.01f)
            {
                Time.timeScale -= 3f * Time.deltaTime;
            }
            else
            {
                Time.timeScale = 0;
                onTimeStopped?.Invoke(playerWin);
            }
        }
    }

    public void Launch()
    {
        int x = UnityEngine.Random.Range(-1, 2);
        int z = UnityEngine.Random.Range(-1, 2);

        while (x == 0 || z == 0)
        {
            x = UnityEngine.Random.Range(-1, 2);
            z = UnityEngine.Random.Range(-1, 2);
        }

        Velocity = new Vector3(x, 0, z).normalized * speed;
        NetworkPosition = transform.position;
    }

    private void PlayerWin()
    {
        playerWin = true;
        GameOver();
    }

    private void EnemyWin()
    {
        playerWin = false;
        GameOver();
    }

    private void GameOver()
    {
        gameOver = true;
        speed = 0;
        Velocity = Vector3.zero;
    }
}