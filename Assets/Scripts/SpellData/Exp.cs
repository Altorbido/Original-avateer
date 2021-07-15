using UnityEngine;
using System.Collections;

public class Exp : Photon.MonoBehaviour {
	public float explosionRadius = 5.0f;
	public float explosionPower = 10.0f;
	 public float explosionDamage = 100.0f;
	public float explosionTimeout = 2.0f;
	public LayerMask Mask;
public Transform Owner;
	// Use this for initialization
	void Start () {

		var explosionPosition = transform.position;
		Collider[] colliders  = Physics.OverlapSphere (explosionPosition, explosionRadius,Mask);

		// Apply damage to close by objects first
		if (photonView.isMine) {

			foreach (Collider hit in colliders) {
				// Calculate distance from the explosion position to the closest point on the collider
				Vector3 closestPoint = hit.ClosestPointOnBounds (explosionPosition);
				float distance = Vector3.Distance (closestPoint, explosionPosition);
			
				// The hit points we apply fall decrease with distance from the explosion point
				float hitPoints = 1.0f - Mathf.Clamp01 (distance / explosionRadius);
				hitPoints *= explosionDamage;
				
				if (hit.transform != Owner && hit.transform.root.GetComponent<PhotonView> () && hit.tag == "Player" || hit.tag == "Enemy") {
					if(photonView.isMine){
					// Tell the rigidbody or any other script attached to the hit object how much damage is to be applied!
					hit.transform.root.GetComponent<PhotonView> ().RPC ("ApplyDamage", PhotonTargets.AllBuffered, hitPoints, PhotonNetwork.player.NickName);
					}}
				
			}
		}
		
		// Apply explosion forces to all rigidbodies
		// This needs to be in two steps for ragdolls to work correctly.
		// (Enemies are first turned into ragdolls with ApplyDamage then we apply forces to all the spawned body parts)
		colliders = Physics.OverlapSphere (explosionPosition, explosionRadius);
		foreach (Collider hit in colliders) {
			if (hit.GetComponent<Rigidbody>())
				hit.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionPosition, explosionRadius, 3.0f);
		}
		
	
		
		// destroy the explosion after a while
		Destroy (gameObject, explosionTimeout);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
