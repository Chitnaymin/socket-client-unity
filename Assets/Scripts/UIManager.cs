using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text txtUID, txtStatus;
    public Button btnPlayer;

	// Start is called before the first frame update
	private void Start()
    {
		btnPlayer.onClick.AddListener(onclickBtnPlayer);
	}

	public IEnumerator SetUID(string id) {
		yield return new WaitForSeconds(1f);
		txtUID.text = ($"UID : {id}");
	}

	IEnumerator setStatus() {
		yield return new WaitForSeconds(1f);
		//txtUID.text = SocketManager.Instance().strStatus;
	}

	void onclickBtnPlayer() {
		SocketManager.Instance().ClientConnect();
	}

	public void HideBtnPlayer() {
		btnPlayer.gameObject.SetActive(false);
	}
}
