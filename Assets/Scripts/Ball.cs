using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private float speed = 2;
    private Vector3 lastFrameVelocity;
    private Rigidbody rb;
    private bool wall = false;
    private bool gameOver = false;
    private bool playerWin;

    public static Action onBlockDestroyed;
    public static Action onWallTouch;
    public static Action<bool> onTimeStopped;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = 2 + 0.175f * (SceneManager.GetActiveScene().buildIndex - 1);

        Launch();
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

    private void Update()
    {
        lastFrameVelocity = rb.linearVelocity;
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

    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);

        if (collision.gameObject.tag == "Wall")
        {
            wall = true;
            onWallTouch?.Invoke();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            wall = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Block" && wall)
        {
            if (other.gameObject.activeSelf)
            {
                other.gameObject.SetActive(false);
            }
            onBlockDestroyed?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            if (other.gameObject.activeSelf)
            {
                other.gameObject.SetActive(false);
            }
            onBlockDestroyed?.Invoke();
        }
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        if (direction.x <= 0)
        {
            if (direction.x > -0.25f)
            {
                direction.x = -0.25f;
            }
        }
        else
        {
            if (direction.x < 0.25f)
            {
                direction.x = 0.25f;
            }
        }
        if (direction.z <= 0)
        {
            if (direction.z > -0.25f)
            {
                direction.z = -0.25f;
            }
        }
        else
        {
            if (direction.z < 0.25f)
            {
                direction.z = 0.25f;
            }
        }




        if (speed < 10f)
        {
            speed += 0.025f;
            if (speed > 10f)
            {
                speed = 10f;
            }
        }
        rb.linearVelocity = direction * speed;
    }

    private void Launch()
    {
        int x = Random.Range(-1, 2);
        int z = Random.Range(-1, 2);
        if (x == 0 || z == 0)
        {
            do
            {
                x = Random.Range(-1, 2);
                z = Random.Range(-1, 2);
            }
            while (x == 0 || z == 0);
        }
        rb.linearVelocity = new Vector3(x * speed, 0, z * speed);
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
        rb.linearVelocity = Vector3.zero;
    }
}
