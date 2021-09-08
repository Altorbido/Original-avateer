using UnityEngine;
using System.Collections;
 using System.Collections.Generic;

public class PlayerVitals : Photon.MonoBehaviour
{
	public  Texture2D hitTexture ;
    public GameObject obj;
        public GameObject ML;
        public GameObject TPC;

    public float hitPoints;
    public Transform hitCam;
    public Transform hitWep;
    public  string playerName;
private bool once = true;
public bool Dead = false;
private bool Destroymyself;
private bool Hit;
	private bool once2;
	public GameObject DeadReplacment;
	public Transform Spawnpoint;
	public float HealRate = 10f;
	public float HealRateMax = 10f;
	public float timer = 0;
	public bool HealMode;

	public float HealRateTimer = 10f;
	private float timer2 = 0;
	public AudioClip ScreamAC;
	public AudioClip[] Hurt;
	public AudioSource AS;
	public CharacterAnimator PA;
	//	public Annocer A;
	public Character C;
    void Start()
    {
	PA = GetComponent<CharacterAnimator>();
    }

    void Update()
    {
		if(photonView.isMine){
			/*
			if (AI  && Brain &&playerName != Brain.Weap.playerName) {
				playerName = Brain.Weap.playerName;
			} else if(!AI){
				playerName = PhotonNetwork.playerName;


			}
			*/
							playerName = PhotonNetwork.playerName;

		if(HealMode ){
				timer = Mathf.Clamp(timer, 0, HealRateMax);
		timer2 = Mathf.Clamp(timer2, 0, 10);

		if (timer > 0) timer -= Time.deltaTime;
			if (timer2 > 0) timer2 -= Time.deltaTime;
			if(timer == 0 && timer2 == 0 && hitPoints < 80){
				this.transform.GetComponent<PhotonView> ().RPC ("Heal", PhotonTargets.AllBuffered);
				timer2 = HealRateTimer ;
			}
		}
				hitPoints = Mathf.Clamp (hitPoints, 0, 100);
			
				hitCam.localRotation = Quaternion.Lerp (hitCam.localRotation, Quaternion.identity, Time.deltaTime * 5);
				hitWep.localRotation = Quaternion.Lerp (hitWep.localRotation, Quaternion.identity, Time.deltaTime * 5);
			
			
		}
	
		if (hitPoints <= 0) {
			photonView.RPC ("Die", PhotonTargets.AllBuffered);
			
			if (photonView.isMine) {
				PhotonNetwork.Destroy (this.gameObject);
			}
			Destroy (this.gameObject);
			
		}
		if (Destroymyself) {
			if (photonView.isMine) {
				PhotonNetwork.Destroy (this.gameObject);
			} else {
				GameObject.Destroy (this.gameObject);
			}
		}
    }
	[PunRPC]
	public void Heal(){
		hitPoints ++;
	}
	
    [PunRPC]
    public void ApplyDamage(float dmg,  string Mykiller , PhotonMessageInfo info)
    {
		hitPoints -= dmg;
		timer = HealRate; 

		if(	PA)PA.GetHit ();
			StartCoroutine (Kick3 (hitWep, new Vector3 (-3f * dmg / 10, Random.Range (-3, 3) * dmg / 10, 0), 0.1f));
			StartCoroutine (Kick3 (hitCam, new Vector3 (-5f * dmg / 10, Random.Range (-5, 5) * dmg / 10, 0), 0.1f));
			StartCoroutine (GetHit ());
			if (hitPoints <= 0) {
			
				Dead = false;
				
				if (once) {

					photonView.RPC ("Die", PhotonTargets.AllBuffered);

				}
				
			}
	
	}
    IEnumerator Kick3(Transform goTransform, Vector3 kbDirection, float time)
    {
        Quaternion startRotation = goTransform.localRotation;
        Quaternion endRotation = goTransform.localRotation * Quaternion.Euler(kbDirection);
        float rate = 1.0f / time;
        var t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            goTransform.localRotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
    }


   
	[PunRPC]
    protected void Die()
	{
		
		if (!once2 &&DeadReplacment){
			GameObject T = Instantiate (DeadReplacment, Spawnpoint.position, transform.rotation) as GameObject;
			once2 = true;
			if(ScreamAC){
			AS.PlayOneShot(ScreamAC);
			}
			if(Hurt.Length >0){
				int Rend =Random.Range(0,Hurt.Length -1);
				AS.enabled =true;
				AS.PlayOneShot(Hurt[Rend]);

			}
		}
			if (photonView.isMine) {
			Screen.lockCursor = false;
			if (C._playerCamera)
				Destroy(C._playerCamera.gameObject);
			//	if (spawn) {
			//	spawn.Die ();
			//}

			this.photonView.RPC ("DestroyRpc", PhotonTargets.AllBuffered);

				// This is my actual PLAYER object, then initiate the respawn process
				Destroy (obj.GetComponentInChildren<PlayerAnim> ());
       
      
				// Destroy(wep, 10);
				Destroy (obj, 10);
				PhotonNetwork.Destroy (gameObject);
				Destroymyself = true;


				this.photonView.RPC ("DestroyRpc", PhotonTargets.AllBuffered);
				this.photonView.RPC ("Delete", PhotonTargets.AllBuffered);

		

		}
    }

	[PunRPC]
	protected void Delete()
	{

		this.photonView.RPC("DestroyRpc", PhotonTargets.AllBuffered);
		if (photonView.isMine) {

			Destroy (obj, 10);
			PhotonNetwork.Destroy (gameObject);
			Destroymyself = true;
		}
	}
	[PunRPC]
	public IEnumerator DestroyRpc()
	{
		GameObject.Destroy(this.gameObject);
		yield return 1; // if you allow 1 frame to pass, the object's OnDestroy() method gets called and cleans up references.
		PhotonNetwork.UnAllocateViewID(this.photonView.viewID);
	}
    void OnGUI()
	{
          if (photonView.isMine)
        {
		if(!Hit) return;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hitTexture);
        }
	}
	IEnumerator GetHit()
    {
		Hit = true;
        yield return new WaitForSeconds(0.3f);
        Hit = false;
    }

	[PunRPC]
	public void RemoveBot(){
		if (PhotonNetwork.isMasterClient) {
//			if (spawn.BotsInGame.Contains (this.gameObject)) {
		//		spawn.BotsInGame.Remove (this.gameObject);
		//	}
		}
	}
void AddKilles(PhotonPlayer Killer){
		if (Killer == PhotonNetwork.player){
			return;
	}
		Killer.AddKilles(1);
}
}
