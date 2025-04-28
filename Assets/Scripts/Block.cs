using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int number;

    public static Action<int, Vector3> onExplosion;

    private void OnDisable()
    {
        onExplosion?.Invoke(number, transform.position);
    }

    public void Explosion()
    {
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
        onExplosion?.Invoke(1, transform.position);
        onExplosion?.Invoke(2, transform.position);
        onExplosion?.Invoke(3, transform.position);
    }
}
