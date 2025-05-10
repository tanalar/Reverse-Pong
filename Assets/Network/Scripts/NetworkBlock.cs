using System;
using Fusion;
using UnityEngine;

public class NetworkBlock : NetworkBehaviour
{

    [Networked] public bool IsActive { get; set; } = true;

    public static Action<int, Vector3> onExplosion;

    public override void Render()
    {
        gameObject.SetActive(IsActive);
    }

    public void DestroyBlock()
    {
        if (HasStateAuthority && IsActive)
        {
            IsActive = false;
        }
    }

    public void Explosion()
    {
        onExplosion?.Invoke(UnityEngine.Random.Range(1,3), transform.position);
        Debug.Log("Explosion effect triggered");
    }
}