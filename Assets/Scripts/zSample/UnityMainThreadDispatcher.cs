using System;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour {
	private static readonly Queue<Action> executionQueue = new Queue<Action>();

	public void Update() {
		lock (executionQueue) {
			while (executionQueue.Count > 0) {
				executionQueue.Dequeue().Invoke();
			}
		}
	}

	public static void Enqueue(Action action) {
		if (action == null) {
			throw new ArgumentNullException(nameof(action));
		}

		lock (executionQueue) {
			executionQueue.Enqueue(action);
		}
	}

	private static UnityMainThreadDispatcher instance;
	public static UnityMainThreadDispatcher Instance() {
		if (!instance) {
			instance = FindObjectOfType<UnityMainThreadDispatcher>();
			if (!instance) {
				var obj = new GameObject("UnityMainThreadDispatcher");
				instance = obj.AddComponent<UnityMainThreadDispatcher>();
				DontDestroyOnLoad(obj);
			}
		}
		return instance;
	}
}
