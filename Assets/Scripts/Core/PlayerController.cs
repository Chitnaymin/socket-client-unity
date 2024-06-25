using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
    private string _playerId;
    private bool _isLocalPlayer;

    public string playerId { get => _playerId; set => _playerId = value; }
    public bool isLocalPlayer { get => _isLocalPlayer; set => _isLocalPlayer = value; }

    private void Update() {
        if (!isLocalPlayer) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
