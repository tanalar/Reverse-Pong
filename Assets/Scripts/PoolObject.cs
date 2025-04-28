using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (!particle.isPlaying)
        {
            ReturnToPool();
        }
    }
}
