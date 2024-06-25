using UnityEngine;
using Models;
using System.Collections.Generic;
using TMPro;

public class GameManager : Singleton<GameManager> {
	public GameObject playerPrefab;
	public GameObject localPlayer;

	private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

	private Vector3 previousPosition;
	public bool hasMoved;
	public float movementThreshold = 0.01f; // Set a small threshold to ignore minor movements

	private bool isCreated = false;

	public bool IsCreated { get => isCreated; set => isCreated = value; }

	private void Update() {
		if (localPlayer != null) {
			// Check if the position has changed significantly
			if (Vector3.Distance(localPlayer.transform.position, previousPosition) > movementThreshold) {
				hasMoved = true;
				// Do something when the object moves
				SocketManager.Instance().SendPlayerPosition(localPlayer.transform.position);

				// Update the previous position
				previousPosition = localPlayer.transform.position;
			} else {
				hasMoved = false;
			}
		}
	}

	public void CreatePlayer(PlayerData playerData, bool isLocalPlayer) {
		if (!players.ContainsKey(playerData.playerId)) {
			Vector3 position = new Vector3(playerData.position.x, 0.5f, playerData.position.z);
			GameObject playerInstance = Instantiate(playerPrefab, position, Quaternion.identity);
			Debug.Log("Player : " + playerInstance.transform.position);
			playerInstance.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "player_" + playerData.playerId.Substring(0, 5);
			playerInstance.GetComponent<PlayerController>().playerId = playerData.playerId;
			playerInstance.GetComponent<PlayerController>().isLocalPlayer = isLocalPlayer;
			players[playerData.playerId] = playerInstance;

			if (isLocalPlayer) {
				localPlayer = playerInstance;
				previousPosition = localPlayer.transform.position;
				isCreated = true;
			}
		}
	}

	public void RemovePlayer(string playerId) {
		if (players.ContainsKey(playerId)) {
			Destroy(players[playerId]);
			players.Remove(playerId);
		}
	}

	public void UpdatePlayerPosition(string playerId, Vector3 position) {
		if (players.ContainsKey(playerId)) {
			players[playerId].transform.position = position;
		}
	}
}
