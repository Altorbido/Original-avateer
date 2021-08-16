using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public enum SpallGOPos{
        Hand,
        Leg,
        GroundWall, 
		        Ground,  
 
    }

[System.Serializable]
public class Spell{
	public string SpellName;
public SpallGOPos SGP; //Spall Go pos
public GameObject SpellPrefab;
public float ManaCoast;
public float CoolDown;
	public float timer = 0;
	public string SpellButton;
	public string SpellTriger;
	public float AnimationID;
	public List<SPC> Compos;
	public int AnimationNumber;
		[HideInInspector]
	public float Combotimer = 0;
	public bool Parent;
	public bool NeedAim;
	[HideInInspector]
	public Transform SpellGO;
	public bool CanLockOn;
	public bool FollowCam;
}

[System.Serializable]
public class SPC{
		[HideInInspector]
	public Transform SPellGO1;
	public string ComponAinmName;
	public bool Done;
}
public class SpellSystem : Photon.MonoBehaviour {
	public List<Spell> MYSpellsList;


	public float Mana;
	public bool Aim;

[Header("CrossHair")]
	public Camera Cam;
	public Texture2D CrossHair;
	public float reticuleSize = 15.0f;

[Header("Mana&Regin")]
	public float ManaRegain = 10f;
	public float ManaRateMax = 10f;
	private float timer = 0;
	private float timer2 = 0;


	public bool SpellPlaying;

[Header("Animation")]
	public SpellAnimiton SA;
	public FixingAnimaor FA;

[Header("Targeting")]
public Targeting Locking;
public float angle;
public float angle2;
public bool CIA; // Camera in Angle


public Transform CamerTarget;
public Vector3 NormailPos;
public Vector3 AimPos;
public Transform GroundWallGo;
	 private void Start() {
		 		Animator anim = GetComponent<Animator>();

		 				foreach (Spell s in MYSpellsList) {
        if(s.SpellTriger == "Hand"){
                   s.SpellGO = anim.GetBoneTransform(HumanBodyBones.RightHand);
              }else if(s.SpellTriger == "Leg"){
				 s.SpellGO = anim.GetBoneTransform(HumanBodyBones.RightFoot);
			  }
	if(s.Compos.Count > 0){
			foreach (SPC C in s.Compos) {
				 if(C.ComponAinmName == "Hand"){
                   C.SPellGO1 = anim.GetBoneTransform(HumanBodyBones.RightHand);
              }else if(C.ComponAinmName == "Leg"){
				 C.SPellGO1 = anim.GetBoneTransform(HumanBodyBones.RightFoot);
			  }
		 }
	} 
						 
	 }
	}
	void CheckAngel(){
if(!Cam){
	Cam  = Camera.main; 
	return;
}
		Vector3 toTarget = (transform.position - Cam.transform.position).normalized;
				angle2 = Vector3.Dot (toTarget, transform.forward);
				CIA = angle2 >= 0;
	}
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			CheckAngel();
			if(Aim){
				if(CIA){
CamerTarget.localPosition = Vector3.Lerp (CamerTarget.localPosition, AimPos, Time.deltaTime * 10f);
			}else{
				CamerTarget.localPosition = Vector3.Lerp (CamerTarget.localPosition, NormailPos, Time.deltaTime * 10f);
			}
			}else{
CamerTarget.localPosition = Vector3.Lerp (CamerTarget.localPosition, NormailPos, Time.deltaTime * 10f);


			}
			if(!Cam){
				Cam= Camera.main;
			}
			Mana = Mathf.Clamp (Mana, 0, 100);
			if(FA && FA.m_ClipName != "Jap"  && SpellPlaying){
				SpellPlaying =false;
			}

			foreach (Spell S in MYSpellsList) {
				if(CIA){
if(S.CanLockOn){
	if(Input.GetButtonDown (S.SpellButton)){
		Locking.LockOn(true);
	}
	if (SA && Input.GetButtonUp (S.SpellButton) && S.timer == 0 && !SpellPlaying) {
			SA.photonView.RPC ("AttackAnim", PhotonTargets.AllBuffered, S.SpellTriger, S.SpellName,Aim,S.AnimationID,0f);					
			Locking.LockOn(false);
	}
	if(Input.GetButtonUp (S.SpellButton)&& Locking.lockedOn){
					Locking.LockOn(false);
	}
}else{
				if (SA && Input.GetButtonDown (S.SpellButton) && S.timer == 0 && !SpellPlaying) {
			SA.photonView.RPC ("AttackAnim", PhotonTargets.AllBuffered, S.SpellTriger, S.SpellName,Aim,S.AnimationID,0f);
					SpellPlaying =true;
				}
}
				foreach (Spell s in MYSpellsList) {
					s.timer = Mathf.Clamp(s.timer, 0, s.CoolDown);
					if (s.timer > 0) s.timer -= Time.deltaTime;
					if(S.Compos.Count > 0){
						s.Combotimer = Mathf.Clamp(s.Combotimer, 0, 1);
						if (s.Combotimer > 0) s.Combotimer -= Time.deltaTime;
						RsetCombo1(S);
				}
        }

			Aim = Input.GetButton ("Fire2");

			timer = Mathf.Clamp(timer, 0, ManaRateMax);
				timer2 = Mathf.Clamp(timer2, 0, 10);

				if (timer > 0) timer -= Time.deltaTime;
				if (timer2 > 0) timer2 -= Time.deltaTime;
				if(timer == 0 && timer2 == 0 && Mana < 100){
					this.transform.GetComponent<PhotonView> ().RPC ("ReginMana", PhotonTargets.AllBuffered);
				timer2 = ManaRegain ;
				}
			
		}
			}
	}
	}

	[PunRPC]
	public void ReginMana(){
		Mana ++;
	}
	public void SpawnEffect(string SpellName){
		if (photonView.isMine)
		{
			foreach (Spell S in MYSpellsList)
			{
				if (SpellName == S.SpellName && S.ManaCoast <= Mana)
				{
					if (S.NeedAim && !Aim)
					{
						Debug.Log("noAim Return" + S.NeedAim);
						return;
						break;
					}
					GameObject TheEffect = S.SpellPrefab;
					GameObject Ef = null;
					if (S.Compos.Count == 0)
					{
						Quaternion SpellRotation = Quaternion.identity;
						if (S.FollowCam)
						{
							SpellRotation = Cam.transform.rotation;
						}
						else
						{
							SpellRotation = Quaternion.Euler(S.SpellGO.forward);
						}
						Ef = PhotonNetwork.Instantiate(TheEffect.name, S.SpellGO.position, SpellRotation, 0, null);
						if (Ef.GetComponent<FireReng>())
						{
							Ef.GetComponent<FireReng>().Owner = this.transform;
						}
						if (Locking.target)
							Ef.transform.LookAt(Locking.target.position + Locking.Offset);
						Mana -= S.ManaCoast;
						//	Ef.transform.rotation = -S.SpellGO.rotation;
						if (S.Parent)
						{
							Ef.transform.SetParent(S.SpellGO);
						}
						Ef.gameObject.layer = 9;
						S.timer = S.CoolDown;
					}
					else
					{
						//Compo
						Debug.Log("We have a Cmopo");
						foreach (SPC C in S.Compos)
						{
							if (!C.Done && C.SPellGO1)
							{
								Debug.Log("Compo Spawoning");
								S.AnimationNumber = S.Compos.IndexOf(C);
								if (C == S.Compos[0])
								{
									S.Combotimer = 1f;
								}
								S.SpellTriger = S.Compos[S.AnimationNumber].ComponAinmName;

								if (Aim)
								{
									Ef = PhotonNetwork.Instantiate(TheEffect.name, C.SPellGO1.position, Cam.transform.rotation, 0, null);
								}
								else
								{
									Ef = PhotonNetwork.Instantiate(TheEffect.name, C.SPellGO1.position, Cam.transform.rotation, 0, null);
									if (Locking.target)
										Ef.transform.LookAt(Locking.target.position + Locking.Offset);
								}
								Mana -= S.ManaCoast;

								Ef.gameObject.layer = 9;
								C.Done = true;
								if (S.AnimationNumber < S.Compos.Count - 1)
								{
									S.AnimationNumber++;
								}
								//	S.AnimationNumber ;
								S.SpellTriger = S.Compos[S.AnimationNumber].ComponAinmName;
								Debug.Log("End");

								if (S.SpellTriger == "Finish")
								{
									S.timer = S.CoolDown;
									RsetCombo(S);
									S.SpellTriger = "Hand";
								}
								return;
							}
						}
					}
				}
			}
		}
		else {
			return;
		}
		
	}

	void RsetCombo(Spell S){
		foreach (SPC C2 in S.Compos) {
			C2.Done = false;
		}
	}
	void RsetCombo1(Spell s){
		if( s.Compos[0].Done && s.Combotimer == 0){
			s.timer = s.CoolDown;
			s.Compos[0].Done = false;
			s.SpellTriger = "Hand";
		}
	}
	void OnGUI()
	{
		
		if(Aim  ){
				//position = new Rect((Screen.width - CrossHair.width) / 2, (Screen.height -  CrossHair.height) /2, CrossHair.width, CrossHair.height);
			Rect position = new Rect((Screen.width - reticuleSize) * 0.5f, (Screen.height - reticuleSize) * 0.5f, reticuleSize, reticuleSize);
			GUI.DrawTexture(position, CrossHair);
			}

		}

	void  OnDrawGizmosSelected(){

		Gizmos.color = Color.red;
		Gizmos.DrawSphere (MYSpellsList[1].SpellGO.position, 0.2f);

	}
}
