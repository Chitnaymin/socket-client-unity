using System;
using UnityEngine;

namespace Models {

	[Serializable]
	public class PlayerData {
		public string playerId { get; set; }
		//public float x { get; set; }
		//public float y { get; set; }
		public Position position { get; set; }

		public PlayerData(string id, Position pos) {
			playerId = id;
			position = pos;
		}

		public PlayerData() {
		}

		public void Print() {
			Debug.Log("I am " + playerId);
		}
	}

	[Serializable]
	public class Position {

		public float x;
		public float y;
		public float z;

		public Position(float posX, float posY, float posZ) {
			x = posX;
			y = posY;
			z = posZ;
		}
	}
}
