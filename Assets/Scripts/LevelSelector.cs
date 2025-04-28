using Firebase.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Sprite greenFrame;
    [SerializeField] private GameObject number;
    [SerializeField] private GameObject _lock;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        if (PlayerPrefs.GetInt("CurrentLevel", 1) > int.Parse(gameObject.name))
        {
            button.image.sprite = greenFrame;
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) < int.Parse(gameObject.name))
        {
            button.interactable = false;
            number.SetActive(false);
            _lock.SetActive(true);
        }
    }
    public void LoadLevel()
    {
        FirebaseAnalytics.LogEvent("user_click_level_choise");

        Time.timeScale = 1;
        SceneManager.LoadScene(int.Parse(gameObject.name));
    }
}
