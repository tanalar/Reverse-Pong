using Firebase.Analytics;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject adsButtonGO;
    [SerializeField] private GameObject pauseButtonGO;
    [SerializeField] private GameObject adsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject soundOff;
    [SerializeField] private GameObject musicOn;
    [SerializeField] private GameObject musicOff;

    public static Action onMusic;

    private void Start()
    {
        SetAudioIcons();
        CheckAdsPurchase();
    }

    private void OnEnable()
    {
        IAPPurchaser.onAdsRemoved += CheckAdsPurchase;
        Ball.onTimeStopped += GameOver;
    }
    private void OnDisable()
    {
        IAPPurchaser.onAdsRemoved -= CheckAdsPurchase;
        Ball.onTimeStopped -= GameOver;
    }

    public void AdsMenu()
    {
        if (adsMenu.activeInHierarchy == false)
        {
            Time.timeScale = 0;

            adsMenu.SetActive(true);
            adsButtonGO.SetActive(false);
            pauseButtonGO.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;

            adsMenu.SetActive(false);
            adsButtonGO.SetActive(true);
            pauseButtonGO.SetActive(true);
        }
    }
    public void PauseMenu()
    {
        if (pauseMenu.activeInHierarchy == false)
        {
            //FirebaseAnalytics.LogEvent("user_click_pause");

            Time.timeScale = 0;

            pauseMenu.SetActive(true);
            adsButtonGO.SetActive(false);
            pauseButtonGO.SetActive(false);
        }
        else
        {
            //FirebaseAnalytics.LogEvent("user_click_resume");

            Time.timeScale = 1;

            pauseMenu.SetActive(false);
            adsButtonGO.SetActive(true);
            pauseButtonGO.SetActive(true);

            CheckAdsPurchase();
        }
    }
    public void Quit()
    {
        //FirebaseAnalytics.LogEvent("user_click_quit");

        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Next()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Sound()
    {
        //FirebaseAnalytics.LogEvent("user_click_sound");

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        SetAudioIcons();
    }
    public void Music()
    {
        //FirebaseAnalytics.LogEvent("user_click_music");

        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        SetAudioIcons();
        onMusic?.Invoke();
    }



    private void SetAudioIcons()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
        else
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
    }
    private void CheckAdsPurchase()
    {
        if (PlayerPrefs.GetInt("AdsRemoved", 0) == 1)
        {
            adsButtonGO.SetActive(false);
        }

        adsMenu.SetActive(false);
        pauseButtonGO.SetActive(true);
        Time.timeScale = 1;
    }
    private void GameOver(bool playerWin)
    {
        if (playerWin)
        {
            winMenu.SetActive(true);
        }
        else
        {
            loseMenu.SetActive(true);
        }
    }
}
