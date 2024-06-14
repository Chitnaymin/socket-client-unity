using UnityEngine;

public class CameraFollow : MonoBehaviour {
	Transform target;        // The target that the camera will follow
	public float smoothSpeed = 0.125f;  // The smoothing speed
	public Vector3 offset;          // The offset from the target

	void LateUpdate() {
		if (GameManager.Instance().IsCreated)
        {
			target = GameObject.FindGameObjectWithTag("Player").transform;
			if (target != null) {
				Vector3 desiredPosition = target.position + offset;
				Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
				transform.position = smoothedPosition;
			}
		}
	}
}
