using System;
using UnityEngine;

namespace Models {

	[Serializable]
	public class PlayerData {
		
		public string playerId { get; set; }
		public Position position { get; set; }

		public PlayerData(string id, Position pos) {
			playerId = id;
			position = pos;
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
