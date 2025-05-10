using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MatchUIView : MonoBehaviour
{
    [SerializeField] private GameObject waitPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button exitButton;

    private IDisposable timerDisposable;
    private float elapsedTime;

    public Action OnExitClicked;

    private void Awake()
    {
        exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        waitPanel.SetActive(true);
        timerDisposable?.Dispose();

        timerDisposable = Observable.Interval(TimeSpan.FromSeconds(1))
            .ObserveOnMainThread()
            .Subscribe(_ =>
            {
                elapsedTime += 1f;
                TimeSpan time = TimeSpan.FromSeconds(elapsedTime);
                timerText.text = time.ToString(@"mm\:ss");
            });
    }

    public void StopTimer()
    {
        waitPanel.SetActive(false);
        timerDisposable?.Dispose();
        timerDisposable = null;
    }
}