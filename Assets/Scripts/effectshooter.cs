using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectshooter : Photon.MonoBehaviour {
	public GameObject TheEffect;
	public GameObject TheEffect2;

	public Transform SpawnPoint;
	public Transform Leg;

	public PlayerAnim PA;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			if (Input.GetButtonDown ("Fire1")) {
				PA.photonView.RPC ("FireEfeect", PhotonTargets.AllBuffered,"Fire");

			}
			if (Input.GetButtonDown ("Fire2")) {
				PA.photonView.RPC ("FireEfeect", PhotonTargets.AllBuffered,"Fire2");

			}
		}
	}
	public void SpawnEffect(){
		if(photonView.isMine){
			GameObject Ef = PhotonNetwork.Instantiate (TheEffect.name, SpawnPoint.position, transform.rotation,0, null);
		Ef.gameObject.layer = 9;
	//	Ef.GetComponent<RFX1_Target> ().Target = this.gameObject;
		}
	}
	public void SpawnEffect2(){
		if(photonView.isMine){
			GameObject Ef = PhotonNetwork.Instantiate (TheEffect2.name, SpawnPoint.position, transform.rotation,0, null);
			Ef.transform.SetParent (Leg);
			Ef.gameObject.layer = 9;
			//	Ef.GetComponent<RFX1_Target> ().Target = this.gameObject;
		}
	}
}
