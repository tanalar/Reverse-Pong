using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private Explosion explosion1;
    [SerializeField] private Explosion explosion2;
    [SerializeField] private Explosion explosion3;

    private void OnEnable()
    {
        Block.onExplosion += StartExplosion;
    }
    private void OnDisable()
    {
        Block.onExplosion -= StartExplosion;
    }

    private void StartExplosion(int explosionNumber, Vector3 position)
    {
        if (explosionNumber == 1)
        {
            explosion1.StartExplosion(position);
        }
        if (explosionNumber == 2)
        {
            explosion2.StartExplosion(position);
        }
        if (explosionNumber == 3)
        {
            explosion3.StartExplosion(position);
        }
    }
}
