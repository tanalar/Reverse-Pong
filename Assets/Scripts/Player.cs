using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks;
    [SerializeField] private bool enemy = false;
    [SerializeField] private bool menu = false;

    public static Action onEnemyWin;
    public static Action onPlayerWin;

    private void OnEnable()
    {
        Ball.onBlockDestroyed += HealthCheck;
    }
    private void OnDisable()
    {
        Ball.onBlockDestroyed -= HealthCheck;
    }

    private void HealthCheck()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i].activeSelf == false)
            {
                if (blocks.Count == 1)
                {
                    blocks[i].GetComponent<Block>().Explosion();
                }
                blocks.RemoveAt(i);
            }
        }
        if (blocks.Count <= 0)
        {
            if (!menu)
            {
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
}
