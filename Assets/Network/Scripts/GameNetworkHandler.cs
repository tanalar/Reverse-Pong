using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameNetworkHandler : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private MatchUIView matchUIView;

    private NetworkRunner Runner;
    private bool ballSpawnScheduled = false;

    private readonly string[] levelPrefabs = {
            "LevelBlueVariant",
            "LevelPinkVariant",
            "LevelYellowVariant"
        };

    private int playersInRoom = 0;
    private bool opponentFound = false;
    private bool isMatchSearching = false;

    private IDisposable searchTimeoutDisposable;

    private void Awake()
    {
        Runner = FindAnyObjectByType<NetworkRunner>();
        Runner.ProvideInput = true;

        playersInRoom = 0;

        Runner.AddCallbacks(this);

        matchUIView.OnExitClicked += ReturnToMenu;
    }

    private void OnDestroy()
    {
        matchUIView.OnExitClicked -= ReturnToMenu;
    }

    private void InitLevel(NetworkRunner runner)
    {
        string levelPrefabName = levelPrefabs[UnityEngine.Random.Range(0, levelPrefabs.Length)];

        runner.Spawn(
            prefab: Resources.Load<NetworkObject>(levelPrefabName),
            position: Vector3.zero,
            rotation: Quaternion.identity
        );
    }

    private void CreateMatchWithBot()
    {
        Debug.Log("No player joined after 10 seconds. Starting match with bot.");
        StopMatchSearch();
        SceneManager.LoadScene(UnityEngine.Random.Range(1, 90));
    }

    public void StopMatchSearch()
    {
        Debug.Log("StopMatchSearch called");

        isMatchSearching = false;
        searchTimeoutDisposable?.Dispose();
        searchTimeoutDisposable = null;

        if (Runner != null)
        {
            Runner.Shutdown();
            Runner = null;
        }
    }


    private Vector3 GetSpawnPositionForPlayer(PlayerRef player)
    {
        int index = player.RawEncoded % 2;
        return new Vector3(0f, 0f, index == 0 ? 1.5f : -1.5f);
    }

    public async void ReturnToMenu()
    {
        matchUIView.StopTimer();
        await Runner.Shutdown();
        await SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }


    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("Scene load done");
        if (runner.IsServer)
        {
            InitLevel(runner);
            matchUIView.StartTimer();
        }
    }

    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        playersInRoom++;

        Vector3 spawnPos = GetSpawnPositionForPlayer(player);

        if (runner.IsServer)
        {
            runner.Spawn(
                prefab: Resources.Load<NetworkObject>("NetworkPlayer"),
                position: spawnPos,
                rotation: Quaternion.identity,
                inputAuthority: player
            );

            if (playersInRoom == 2 && !ballSpawnScheduled)
            {
                ballSpawnScheduled = true;

                Observable.Timer(TimeSpan.FromSeconds(3))
                    .ObserveOnMainThread()
                    .Subscribe(_ =>
                    {
                        var ballPrefab = Resources.Load<NetworkObject>("NetworkBall");
                        if (ballPrefab == null)
                        {
                            Debug.LogError("NetworkBall prefab not found in Resources!");
                            return;
                        }

                        runner.Spawn(
                            prefab: ballPrefab,
                            position: Vector3.zero,
                            rotation: Quaternion.identity
                        );
                    }).AddTo(this);
            }
        }

        if (!isMatchSearching)
        {
            isMatchSearching = true;
            opponentFound = false;

            searchTimeoutDisposable = Observable.Timer(TimeSpan.FromSeconds(12))
            .ObserveOnMainThread()
            .Subscribe(_ =>
            {
                if (!opponentFound && playersInRoom <= 1 && isMatchSearching)
                {
                    CreateMatchWithBot();
                }
            }).AddTo(this);
        }

        if (!player.Equals(runner.LocalPlayer))
        {
            matchUIView.StopTimer();

            opponentFound = true;
            isMatchSearching = false;

            searchTimeoutDisposable?.Dispose();
            searchTimeoutDisposable = null;
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {
        StopMatchSearch();
    }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    public void OnInput(NetworkRunner runner, NetworkInput input) {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, floorLayer))
            {
                NetworkInputData data = new NetworkInputData
                {
                    TargetPoint = hit.point,
                };
                input.Set(data);
            }
        }
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, System.ArraySegment<byte> data) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, System.ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
}