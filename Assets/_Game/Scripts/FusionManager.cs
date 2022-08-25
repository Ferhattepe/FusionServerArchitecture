using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class FusionManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkRunner Runner { get; private set; }
    public NetworkPrefabRef playerPrefab;
    public Transform spawnPoint;
    private SceneLoadManager loadManager;
    public VoidEvent createSessionEvent;
    public VoidEvent joinSessionEvent;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public async void CreateSession()
    {
        Debug.LogError("Begin CreateSession");
        if (Runner == null)
        {
            Runner = gameObject.AddComponent<NetworkRunner>();
            loadManager = gameObject.AddComponent<SceneLoadManager>();
        }
        Runner.ProvideInput = false;
        await Runner.StartGame(new StartGameArgs()
        {
            CustomLobbyName = "Dota",
            GameMode = GameMode.Server,
            PlayerCount = 2,
            SceneManager = loadManager
        });
        createSessionEvent.Raise();
    }

    public async void JoinSession()
    {
        Debug.LogError("Begin JoinSession");
        if (Runner == null)
        {
            Runner = gameObject.AddComponent<NetworkRunner>();
            loadManager = gameObject.AddComponent<SceneLoadManager>();
        }
        Runner.ProvideInput = true;
        await Runner.StartGame(new StartGameArgs()
        {
            CustomLobbyName = "Dota",
            GameMode = GameMode.Client,
            SceneManager = loadManager
        });
        joinSessionEvent.Raise();
    }


    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.LogError("OnConnectedToServer");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.LogError("OnConnectFailed");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.LogError("OnConnectRequest");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.LogError("OnCustomAuthenticationResponse");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.LogError("OnDisconnectedFromServer");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.LogError("OnHostMigration");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        if (Input.GetKey(KeyCode.W))
            data.direction += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            data.direction += Vector3.back;

        if (Input.GetKey(KeyCode.A))
            data.direction += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            data.direction += Vector3.right;

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.LogError("OnPlayerJoined");
        if (runner.IsServer)
        {
            var networkPlayer = runner.Spawn(playerPrefab, spawnPoint.position, Quaternion.identity, player);
            _spawnedCharacters.Add(player, networkPlayer);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.LogError("OnPlayerLeft");
        if (runner.IsServer)
        {
            if (_spawnedCharacters.TryGetValue(player, out var networkObject))
            {
                runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
            }
        }
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.LogError("OnReliableDataReceived");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.LogError("OnSceneLoadDone");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.LogError("OnSceneLoadStart");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.LogError("OnSessionListUpdated");
    }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.LogError("OnShutdown");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.LogError("OnUserSimulationMessage");
    }
}
