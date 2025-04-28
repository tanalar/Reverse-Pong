using Firebase.Analytics;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject difficultyMenu;
    [SerializeField] private GameObject chooseLevelMenu;
    [SerializeField] private GameObject adsMenu;

    [SerializeField] private GameObject adsButton;

    private void Start()
    {
        CheckAdsPurchase();
    }

    private void OnEnable()
    {
        IAPPurchaser.onAdsRemoved += CheckAdsPurchase;
    }
    private void OnDisable()
    {
        IAPPurchaser.onAdsRemoved -= CheckAdsPurchase;
    }

    public void MainMenu()
    {
        FirebaseAnalytics.LogEvent("user_click_back");

        mainMenu.SetActive(true);

        difficultyMenu.SetActive(false);
        chooseLevelMenu.SetActive(false);
        adsMenu.SetActive(false);
    }

    public void Difficulty()
    {
        difficultyMenu.SetActive(true);

        mainMenu.SetActive(false);
        chooseLevelMenu.SetActive(false);
        adsMenu.SetActive(false);
    }

    public void ChooseLevel()
    {
        FirebaseAnalytics.LogEvent("user_click_champoinship");

        chooseLevelMenu.SetActive(true);

        mainMenu.SetActive(false);
        difficultyMenu.SetActive(false);
        adsMenu.SetActive(false);
    }

    public void Ads()
    {
        FirebaseAnalytics.LogEvent("user_click_removead");

        adsMenu.SetActive(true);

        mainMenu.SetActive(false);
        difficultyMenu.SetActive(false);
        chooseLevelMenu.SetActive(false);
    }

    private void CheckAdsPurchase()
    {
        if (PlayerPrefs.GetInt("AdsRemoved", 0) == 1)
        {
            adsButton.SetActive(false);
        }

        mainMenu.SetActive(true);
        adsMenu.SetActive(false);
    }
}
