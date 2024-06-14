using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text txtUID, txtStatus;
    public Button btnPlayer1;

	private new void Awake() {
		btnPlayer1.onClick.AddListener(onclickBtnPlayer1);
	}
	// Start is called before the first frame update
	void Start()
    {
        StartCoroutine(setUID());
    }

	IEnumerator setUID() {
		yield return new WaitForSeconds(1f);
		txtUID.text = ($"UID : {SocketManager.Instance().playerID}");
	}

	IEnumerator setStatus() {
		yield return new WaitForSeconds(1f);
		txtUID.text = SocketManager.Instance().strStatus;
	}

	void onclickBtnPlayer1() {
		SocketManager.Instance().ClientConnect();
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
