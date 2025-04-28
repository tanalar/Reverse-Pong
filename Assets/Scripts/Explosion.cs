using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Pool explosion;

    private void Start()
    {
        explosion = GetComponent<Pool>();
    }

    public void StartExplosion(Vector3 position)
    {
        explosion.GetFreeElement(position);
    }
}
