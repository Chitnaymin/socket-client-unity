using Models;
using System;
using UnityEngine;

public class GlobalFunction : MonoBehaviour
{
    public static event Action<PlayerData> EvntPlayerData;

	public static void InvokePlayerData(PlayerData player) {
		EvntPlayerData?.Invoke(player);
	}
}
