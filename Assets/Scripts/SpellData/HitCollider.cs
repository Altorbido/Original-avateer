using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : Photon.MonoBehaviour {
	public Transform hitPoint;
	public Transform hitPointLeft;

	public float Radius = 5f;
	public float Damdge = 50;
	public GameObject blood;
	public Transform Owner;
	public void hitRight(){
		Debug.Log ("hit");
		// Apply damage to close by objects first
		if (photonView.isMine) {
			Collider[] colliders  = Physics.OverlapSphere (hitPoint.position, Radius);

			foreach (Collider hit in colliders) {

				if (hit.transform.root.GetComponent<PhotonView> () && hit.tag == "Player" && Owner != hit.transform) {
					// Tell the rigidbody or any other script attached to the hit object how much damage is to be applied!
					hit.transform.root.GetComponent<PhotonView> ().RPC ("ApplyDamage", PhotonTargets.AllBuffered, Damdge, PhotonNetwork.player.NickName);
					Instantiate (blood, hit.ClosestPoint(transform.position), Quaternion.identity);

					Debug.Log (hit.name);

				}
			}
		}
	}
	public void hitLeft(){
		Debug.Log ("hit");
		// Apply damage to close by objects first
		if (photonView.isMine) {
			Collider[] colliders  = Physics.OverlapSphere (hitPoint.position, Radius);

			foreach (Collider hit in colliders) {

				if (hit.transform.root.GetComponent<PhotonView> () && hit.tag == "Player" && Owner != hit.transform) {
					// Tell the rigidbody or any other script attached to the hit object how much damage is to be applied!
					hit.transform.root.GetComponent<PhotonView> ().RPC ("ApplyDamage", PhotonTargets.AllBuffered, Damdge, PhotonNetwork.player.NickName);
					Instantiate (blood, hit.ClosestPoint(transform.position), Quaternion.identity);

					Debug.Log (hit.name);

				}
			}
		}
	}
	void  OnDrawGizmosSelected(){
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (hitPoint.position, Radius);
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (hitPointLeft.position, Radius);

	}
}
