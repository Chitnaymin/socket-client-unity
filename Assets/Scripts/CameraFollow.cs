using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	Transform target;        // The target that the camera will follow
	public float smoothSpeed = 0.125f;  // The smoothing speed
	public Vector3 offset;          // The offset from the target

	void Start() {
		StartCoroutine(FindLocalPlayer());
	}

	void LateUpdate() {
		if (target != null) {
			Vector3 desiredPosition = target.position + offset;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
			transform.position = smoothedPosition;
		}
	}

	IEnumerator FindLocalPlayer() {
		while (target == null) {
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in players) {
				if (player.GetComponent<PlayerController>().isLocalPlayer) {
					target = player.transform;
					break;
				}
			}
			yield return new WaitForSeconds(0.5f); // Wait before trying again
		}
	}

}
