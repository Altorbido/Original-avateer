using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SimpleMenu : Photon.MonoBehaviour
{

	public InputField NameField;
	public ConnectAndJoinRandom CJR;
	public ShowStatusWhenConnecting SSWC;
	public GameObject MenuHud;
	public Text ErrorText;
	public void Connecict(){
		if(!string.IsNullOrEmpty(NameField.text)){
		PhotonNetwork.player.NickName = NameField.text;
		CJR.enabled = true;
		SSWC.enabled = true;
		MenuHud.SetActive(false);
		}else{
			ErrorText.text = "Please Enter a fucking Name";
		}
	}
}
