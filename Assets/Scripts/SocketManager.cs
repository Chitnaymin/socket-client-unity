using UnityEngine;
using System.Collections.Generic;
using Models;
using SocketIOClient.Newtonsoft.Json;
using static SocketIOUnity;

public class SocketManager : Singleton<SocketManager> {
	private SocketIOUnity client;
	private string _playerID;

	public async void ClientConnect() {
		client = new SocketIOUnity("http://localhost:3000");
		client.unityThreadScope = UnityThreadScope.Update;
		client.JsonSerializer = new NewtonsoftJsonSerializer();

		client.OnConnected += (sender, e) => {
			Debug.Log("Connected to server" + client.Id);
			client.EmitAsync("newPlayer");
			_playerID = client.Id;

			UnityThread.executeInUpdate(() => {
				StartCoroutine(UIManager.Instance().SetUID(_playerID));
			});
		};

		client.OnUnityThread("currentPlayers", response => {
			var currentPlayers = response.GetValue<Dictionary<string, PlayerData>>();
			foreach (var player in currentPlayers) {
				bool isLocalPlayer = player.Key == _playerID;
				GameManager.Instance().CreatePlayer(player.Value, isLocalPlayer);
			}
			UIManager.Instance().txtStatus.text = "Player connected";
			UIManager.Instance().HideBtnPlayer();
		});

		client.OnUnityThread("newPlayer", response => {
			var newPlayer = response.GetValue<PlayerData>();
			bool isLocalPlayer = newPlayer.playerId == _playerID;
			GameManager.Instance().CreatePlayer(newPlayer, isLocalPlayer);
		});

		client.OnUnityThread("playerMoved", response => {
			var movedPlayer = response.GetValue<PlayerData>();
			GameManager.Instance().UpdatePlayerPosition(movedPlayer.playerId, new Vector3(movedPlayer.position.x,movedPlayer.position.y, movedPlayer.position.z));
		});

		client.On("playerCount", response => {
			UnityThread.executeInUpdate(() => {
				int pCount = response.GetValue<int>();
				Debug.Log("Current player count: " + pCount);
			});
		});

		client.OnUnityThread("playerDisconnected", response => {
			string playerId = response.GetValue<string>();
			GameManager.Instance().RemovePlayer(playerId);
		});

		await client.ConnectAsync();
	}

	public void SendPlayerPosition(Vector3 position) {
		client.EmitAsync("playerMoved", new { position = new { x = position.x,y = position.y, z = position.z } });//pyin ya ohn ml
	}
}