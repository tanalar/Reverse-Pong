using Fusion;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform))]
public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private NetworkPlayerView playerView;


    public override void Spawned()
    {
        playerView = GetComponentInChildren<NetworkPlayerView>();

        if (transform.position.z > 0 && HasInputAuthority)
        {
            Camera.main.transform.rotation = Quaternion.Euler(45, 180f, 0);
            Camera.main.transform.position = new Vector3(0, 4.1f, 3.5f);
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
    }



    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        if (GetInput<NetworkInputData>(out var input))
        {
            Vector3 direction = input.TargetPoint - transform.position;
            direction.y = 0f;

            if (direction.magnitude > 0.1f)
            {
                Vector3 move = direction.normalized * moveSpeed * Runner.DeltaTime;
                Vector3 nextPos = transform.position + move;

                //TODO: размер плаформы меняется значит должны меняться отсупы
                nextPos.x = Mathf.Clamp(nextPos.x, -0.65f, 0.65f);
                nextPos.z = Mathf.Clamp(nextPos.z, transform.position.z > 0 ? 0.25f : -2f, transform.position.z > 0 ? 2f : -0.25f);

                transform.position = Vector3.Lerp(transform.position, nextPos, 35f * Runner.DeltaTime);
            }
        }
    }

}
