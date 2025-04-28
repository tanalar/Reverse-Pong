using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> wallSounds;
    [SerializeField] private List<AudioSource> explosionSounds;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private List<AudioClip> soundtrack;

    private int wallSoundsCounter = 0;
    private int explosionSoundsCounter = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("SoundtrackNumber", PlayerPrefs.GetInt("SoundtrackNumber", 0) + 1);
        if (PlayerPrefs.GetInt("SoundtrackNumber", 0) >= soundtrack.Count)
        {
            PlayerPrefs.SetInt("SoundtrackNumber", 0);
        }
        music.clip = soundtrack[PlayerPrefs.GetInt("SoundtrackNumber", 0)];
        Music();
    }

    private void OnEnable()
    {
        Ball.onWallTouch += PlayWallSound;
        Ball.onBlockDestroyed += PlayExplosionSound;
        UI.onMusic += Music;
        Player.onEnemyWin += PlayGameOverSound;
        Player.onPlayerWin += PlayGameOverSound;
    }
    private void OnDisable()
    {
        Ball.onWallTouch -= PlayWallSound;
        Ball.onBlockDestroyed -= PlayExplosionSound;
        UI.onMusic -= Music;
        Player.onEnemyWin -= PlayGameOverSound;
        Player.onPlayerWin -= PlayGameOverSound;
    }

    private void PlayWallSound()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            wallSounds[wallSoundsCounter].pitch = Random.Range(1.0f, 1.2f);
            wallSounds[wallSoundsCounter].Play();

            wallSoundsCounter++;
            if ((wallSoundsCounter) >= wallSounds.Count)
            {
                wallSoundsCounter = 0;
            }
        }
    }

    private void PlayExplosionSound()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            explosionSounds[explosionSoundsCounter].pitch = Random.Range(1.0f, 1.2f);
            explosionSounds[explosionSoundsCounter].Play();

            explosionSoundsCounter++;
            if (explosionSoundsCounter >= explosionSounds.Count)
            {
                explosionSoundsCounter = 0;
            }
        }
    }
    private void Music()
    {
        if(PlayerPrefs.GetInt("Music", 1) == 1)
        {
            music.Play();
        }
        else
        {
            music.Stop();
        }
    }
    private void PlayGameOverSound()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            gameOverSound.Play();
        }
    }
}
