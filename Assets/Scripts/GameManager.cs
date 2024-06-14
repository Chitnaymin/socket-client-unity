using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab, parentGO;
	PlayerData playerData;
	bool isCreated = false;

	private void Start() {
		playerData = new PlayerData();
	}

	public bool IsCreated { get => isCreated; set => isCreated = value; }

	public void CreatePlayer(string id, PlayerData playerData) {
		GameObject player = Instantiate(playerPrefab, new Vector3(playerData.position.x, playerData.position.y, 0), Quaternion.identity);
		player.transform.SetParent(parentGO.transform, false);
		Debug.Log("Player instantiated with ID: " + id);

		playerData.id = id;
		playerData.position.x = player.transform.position.x;
		playerData.position.y = player.transform.position.y;
		isCreated = true;
	}
}
