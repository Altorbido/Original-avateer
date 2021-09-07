using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAnimiton : Photon.MonoBehaviour
{
    // Start is called before the first frame update
	public string CurrSpellName;
 	public SpellSystem SS;

public Animator anim;

  	[PunRPC]
	void AttackAnim(string TN ,string SpellName,bool Aim,float AnimID,float HandID){ // TN = Trigger Name
		if(Aim){
			anim.SetBool("Aim",Aim);
			Invoke ("DisableAim", 0.3f);
		}
		/*
		if(TN == "Hand" || anim.GetBool("Hand")){
			anim.SetLayerWeight(2, 1f);
		}
		*/
		float AI = 0f;
		if ( AnimID <= 2)
		{
			int randId = Random.Range(0, 2);
			AI = (float)randId;
		}
		else {
			AI = AnimID;
		}
		float NewHandID = (float)Random.Range(0,1);
		anim.SetFloat("AAID", AI);
		anim.SetFloat("HandNumber", NewHandID);
		anim.SetTrigger (TN);

		CurrSpellName = SpellName;
		StartCoroutine(DisableAttac(TN));
	}
    IEnumerator DisableAttac( string TN)
	{
		yield return new WaitForSeconds(0.001f);
		anim.SetBool (TN,false);
	}
    void DisableAim(){
		anim.SetBool("Aim",false);
	}
    public void SpawnEffect(){
        if(photonView.isMine)
		SS.FindEffect(CurrSpellName);
	}
}
