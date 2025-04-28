using UnityEngine;

public class LevelsContentController : MonoBehaviour
{
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();

        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 1 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 4)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0, rect.localPosition.z);
        }

        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 5 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 8)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 1, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 9 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 12)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 2, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 13 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 16)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 3, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 17 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 20)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 4, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 21 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 24)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 5, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 25 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 28)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 6, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 29 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 32)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 7, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 33 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 36)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 8, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 37 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 40)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 9, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 41 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 44)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 10, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 45 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 48)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 11, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 49 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 52)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 12, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 53 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 56)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 13, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 57 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 60)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 14, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 61 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 64)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 15, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 65 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 68)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 16, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 69 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 72)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 17, rect.localPosition.z);
        }
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 73 && PlayerPrefs.GetInt("CurrentLevel", 1) <= 76)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 0 + 240 * 18, rect.localPosition.z);
        }
        
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= 77)
        {
            rect.transform.localPosition = new Vector3(rect.localPosition.x, 4530f, rect.localPosition.z);
        }
    }
}
