using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CountPing : Photon.MonoBehaviour {
	Hashtable PlayerCustomProps = new Hashtable();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		PlayerCustomProps["Ping"] = PhotonNetwork.GetPing();
		PhotonNetwork.player.SetCustomProperties(PlayerCustomProps);
*/
	}

}
