using Fusion;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkPlayerView : NetworkBehaviour
{
    [SerializeField] private List<NetworkBlock> blocks;
    [SerializeField] private bool enemy = false;
    [SerializeField] private bool menu = false;

    [Networked] private bool GameEnded { get; set; }

    public static Action onEnemyWin;
    public static Action onPlayerWin;

    private void Awake()
    {
        if (blocks.Count == 0)
        {
            blocks.AddRange(GetComponentsInChildren<NetworkBlock>());
        }
    }

    public override void Spawned()
    {
        Ball.onBlockDestroyed += HealthCheck;
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        Ball.onBlockDestroyed -= HealthCheck;
    }

    private void HealthCheck()
    {
        if (!HasStateAuthority || GameEnded)
            return;

        for (int i = blocks.Count - 1; i >= 0; i--)
        {
            if (!blocks[i].IsActive)
            {
                if (blocks.Count == 1)
                {
                    blocks[i].Explosion();
                }

                blocks.RemoveAt(i);
            }
        }

        if (blocks.Count <= 0)
        {
            GameEnded = true;

            if (menu) return;

            if (enemy)
            {
                onPlayerWin?.Invoke();
                PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                onEnemyWin?.Invoke();
            }
        }
    }


}