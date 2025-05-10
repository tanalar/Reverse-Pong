using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNetworkHandler : MonoBehaviour
{
    [SerializeField] private NetworkRunner networkRunnerPrefab;
    [SerializeField] private Button startMatch;

    private NetworkRunner networkRunner;
    private int playerRating;


    void Awake()
    {
        startMatch.onClick.RemoveAllListeners();
        startMatch.onClick.AddListener(StartMatchSearch);

        var existing = FindAnyObjectByType<NetworkRunner>();
        if (existing != null)
        {
            Destroy(existing.gameObject); 
        }

        Time.timeScale = 1;
    }


    private NetworkRunner SetupNetworkRunner()
    {
        var runner = Instantiate(networkRunnerPrefab);
        return runner;
    }


    public void StartMatchSearch()
    {
        Debug.Log("StartMatchSearch called");
        startMatch.interactable = false;

        playerRating = GetLocalPlayerRating();

        networkRunner = SetupNetworkRunner();

        int ratingBucket = playerRating / 50; 
        string sessionName = $"MatchRating_{ratingBucket}";

        Debug.Log($"Trying to join or create session: {sessionName}");

        var sessionProps = new Dictionary<string, SessionProperty>()
        {
            { "rating", playerRating }
        };

        networkRunner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = sessionName,
            CustomLobbyName = "Lobby",
            SceneManager = GetSceneManager(networkRunner),
            SessionProperties = sessionProps,
            PlayerCount = 2,
            Scene = SceneRef.FromIndex(91)
        });
    }

  

    private INetworkSceneManager GetSceneManager(NetworkRunner runner)
    {
        return runner.GetComponent<INetworkSceneManager>()
               ?? runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
    }

    private int GetLocalPlayerRating()
    {
        return 100;
    }
}
