using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	private Vector3 lastPosition;

	void Start() {
		lastPosition = transform.position;
	}

	void Update() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
		transform.Translate(movement * speed * Time.deltaTime);

		if (Vector3.Distance(transform.position, lastPosition) > 0.1f) {
			lastPosition = transform.position;
			SocketManager.Instance().SendPlayerPosition(transform.position);
		}
	}
	
}
