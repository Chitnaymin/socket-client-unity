using UnityEngine;
using SocketIOClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SimpleJSON;

public class SocketManager : Singleton<SocketManager> {
	private SocketIOUnity client;
	private string _playerID;
	private string _strStatus;

	public string playerID {
		get { return _playerID; }
		set { _playerID = value; }
	}

	public string strStatus { get => _strStatus; set => _strStatus = value; }

	public async void ClientConnect() {
		client = new SocketIOUnity("http://localhost:3000");

		client.OnConnected += (sender, e) => {
			Debug.Log("Connected to server"+client.Id);
			client.EmitAsync("newPlayer");
			_playerID = client.Id;
		};

		client.On("currentPlayers", response => {
			Debug.Log("Player Connected: " + response);
			try {
				// Assuming response is a JSON string
				string json = response.ToString();
				Debug.Log($"{json}");
				ReadAsData(json);

				// Deserialize JSON string to dictionary
				Dictionary<string, PlayerData> currentPlayers = JsonConvert.DeserializeObject<Dictionary<string, PlayerData>>(json);

				// Handle existing players
				foreach (var player in currentPlayers) {
					Debug.Log("Processing player ID: " + player.Key);
					GameManager.Instance().CreatePlayer(player.Key, player.Value);
					UIManager.Instance().txtStatus.text = "Player connected";
				}
			} catch (Exception ex) {
				//Debug.LogError("Exception in handling currentPlayers event: " + ex.Message);
				Debug.Log(ex.Message);
			}
		});

		client.On("newPlayer", response => {
			Debug.Log("New player connected: " + response);
			// Handle new player
			Dictionary<string, PlayerData> currentPlayers = response.GetValue<Dictionary<string, PlayerData>>();
			foreach (var player in currentPlayers) {
				Debug.Log(player.Key);
				GameManager.Instance().CreatePlayer(player.Key,player.Value);
				UIManager.Instance().txtStatus.text = "Player connected";
			}
		});

		client.On("playerDisconnected", response => {
			Debug.Log("Player disconnected: " + response);
			// Handle player disconnection
		});

		client.On("playerMoved", response => {
			Debug.Log("Player moved: " + response);
			// Handle player movement
		});

		await client.ConnectAsync();
	}

	new void OnApplicationQuit() {
		client.DisconnectAsync();
	}

	public void SendPlayerPosition(Vector3 position) {
		client.EmitAsync("move", new {
			position = new { x = position.x, y = position.y }
		});
	}

	void ReadAsData(string jsonText)
    {
        JSONNode Node = JSONNode.Parse(jsonText);

        int maxNum = Node.Count;
        string playerDataid = "";
        string playerPositionX = "";
        string playerPositionY = "";
        foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)Node[0])
        {
            Debug.Log(string.Format("SMM : {0}: {1} {2}",
                kvp.Key, kvp.Value["position"]["x"].Value, kvp.Value["position"]["y"].Value));

            Debug.Log("SMM kvp.Key " + kvp.Key.ToString());
            Debug.Log("SMM playerData.position.x " + kvp.Value["position"]["x"].Value.ToString());
            Debug.Log("SMM playerData.position.y " + kvp.Value["position"]["x"].Value.ToString());

            playerDataid = kvp.Key.ToString();
            playerPositionX = kvp.Value["position"]["x"].Value.ToString();
            playerPositionY = kvp.Value["position"]["x"].Value.ToString();
        }
        Debug.Log("SMM almost Printing");

        Debug.Log("SMM kvp.Key " + playerDataid);
        Debug.Log("playerPositionX " + playerPositionX);
        Debug.Log("playerPositionY " + playerPositionY);
        
        SMMPlayerData SMMplayerData = new SMMPlayerData(playerDataid, int.Parse(playerPositionX), int.Parse(playerPositionY));
        SMMplayerData.Print();
        

    }
}
