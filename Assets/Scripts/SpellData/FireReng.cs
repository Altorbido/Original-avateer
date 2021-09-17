using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireReng : Photon.MonoBehaviour {
	public float Radius = 5f;
	public float Damdge = 50;
	public Transform Owner;
public LayerMask Mask;
	public bool loop;
	public float LoopTime = 0.5f;
	public float timer;
	// Use this for initialization

	// Update is called once per frame
	void Start () {
		if (photonView.isMine && loop) {
			Collider[] colliders  = Physics.OverlapSphere (transform.position, Radius,Mask);

			foreach (Collider hit in colliders) {
				
				if (hit.transform.root.GetComponent<PhotonView> () && hit.tag == "Player" && Owner != hit.transform && !hit.transform.IsChildOf(Owner.transform)) {
					// Tell the rigidbody or any other script attached to the hit object how much damage is to be applied!
					hit.transform.root.GetComponent<PhotonView> ().RPC ("ApplyDamage", PhotonTargets.AllBuffered, Damdge, PhotonNetwork.player.NickName);

					Debug.Log (hit.name);

				}
			}
		}

	}
    private void Update()
    {
		if (!photonView)
		{
			this.enabled = false;
			return;
		}
		timer = Mathf.Clamp(timer, 0, 10);
			if (timer > 0 && loop) timer -= Time.deltaTime;
	
		if (loop && timer == 0) {
			Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, Mask);

			foreach (Collider hit in colliders)
			{

				if (hit.transform.root.GetComponent<PhotonView>() && hit.tag == "Player" && Owner != hit.transform && !hit.transform.IsChildOf(Owner.transform))
				{
					// Tell the rigidbody or any other script attached to the hit object how much damage is to be applied!
					hit.transform.root.GetComponent<PhotonView>().RPC("ApplyDamage", PhotonTargets.AllBuffered, Damdge, PhotonNetwork.player.NickName);
					timer = LoopTime;

					Debug.Log(hit.name);

				}
			}
		}
    }
    void  OnDrawGizmosSelected(){
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (transform.position, Radius);
	

	}
}
