using Firebase.Analytics;
using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPPurchaser : MonoBehaviour
{
    public static Action onAdsRemoved;
    //public static Action onCoinsPurchased;

    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "removeads":
                //FirebaseAnalytics.LogEvent("user_click_pay");
                PlayerPrefs.SetInt("AdsRemoved", 1);
                onAdsRemoved?.Invoke();
                break;
            //case "250coins":
            //    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 250);
            //    onCoinsPurchased?.Invoke();
            //    break;
            //case "500coins":
            //    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 500);
            //    onCoinsPurchased?.Invoke();
            //    break;
            //case "1000coins":
            //    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1000);
            //    onCoinsPurchased?.Invoke();
            //    break;
            //case "2500coins":
            //    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 2500);
            //    onCoinsPurchased?.Invoke();
            //    break;
        }
    }
}
