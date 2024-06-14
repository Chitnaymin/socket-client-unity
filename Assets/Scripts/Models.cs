[System.Serializable]
public class PlayerPosition {
	public float x { get; set; }
	public float y { get; set; }
}

[System.Serializable]
public class PlayerData {
	public string id { get; set; }
	public PlayerPosition position { get; set; }
}
