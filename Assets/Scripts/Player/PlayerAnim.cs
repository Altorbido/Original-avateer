using UnityEngine;
using System.Collections;

public class PlayerAnim : Photon.MonoBehaviour {
	public Animator anim;
	public CharacterValues cv;
	public bool Crouch;
	public effectshooter ES;

	public SpellSystem SS;
	// Use this for initialization
	public string CurrSpellName;
	public CharacterController CC;
	public bool dontMove;
	// Update is called once per frame
	void Update () {
	
		if (cv.ver > 0) {
			anim.speed = cv.velPercent;
		} else {
			anim.speed =  1;
		}

		bool walkToggle = cv.running;
		// We select appropriate speed based on whether we're walking by default, and whether the walk/run toggle button is pressed:
		float walkMultiplier = (true ? walkToggle ? 1 : 0.5f : walkToggle ? 0.5f : 1);
		float newVer = cv.ver * walkMultiplier;
			
		anim.SetFloat ("Horizontal", cv.hor);
		anim.SetFloat ("Vertical",newVer );
		anim.SetFloat("Jump", cv.JumpY +10);
		anim.SetBool ("OnGround", cv.grounded);
		anim.SetBool ("FF", cv.FrontFlip);

		if(cv.hor == 0 && cv.Rothor != 0 ){
			anim.SetFloat ("Horizontal", cv.Rothor);
		} 
		/*
		if (cv.state == 1) {
			Crouch = true;
				
		} else {
			Crouch = false;
				
		}

		anim.SetBool ("Crouch", Crouch);
*/
		//anim.speed =  cv.velPercent;
		//anim.SetBool("LeftWall",cv.ROLW);
		//anim.SetBool("RightWall",cv.RORW);
		float runCycle =
			Mathf.Repeat(
				anim.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.2f, 1);
		float jumpLeg = (runCycle < 0.5f ? 1 : -1)*cv.ver;
		if (cv.grounded)
		{
			anim.SetFloat("JumpLeg", jumpLeg);
		}
	}
	/*
	public void OnAnimatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
	//	CC.transform.rotation = anim.rootRotation;
		if (CC.isGrounded && Time.deltaTime > 0)
		{
			Vector3 v = (anim.deltaPosition*1)/Time.deltaTime;

			// we preserve the existing y part of the current velocity.
		//	v.y = cv.JumpY;
			CC.Move ( v);

		}
	}*/
	/*
	[PunRPC]
	void FireEfeect(string TN ,string SpellName,bool Aim){ // TN = Trigger Name
		if(Aim){
			anim.SetBool("Aim",Aim);
			Invoke ("DisableAim", 0.3f);
		}
		anim.SetTrigger (TN);
		CurrSpellName = SpellName;
	}
*/

	[PunRPC]
	void AttackAnim(string TN ,string SpellName,bool Aim,float AnimID,float HandID){ // TN = Trigger Name
		if(Aim){
			anim.SetBool("Aim",Aim);
			Invoke ("DisableAim", 0.3f);
		}
		if(TN == "Hand"){
			anim.SetLayerWeight(2, 1f);
		}
		float NewHandID = (float)Random.Range(0,1);
		anim.SetFloat("AAID", AnimID);
		anim.SetFloat("HandNumber", NewHandID);
		anim.SetBool (TN,true);

		CurrSpellName = SpellName;
		StartCoroutine(DisableAttac(TN));


	}
	IEnumerator DisableAttac( string TN)
	{
		yield return new WaitForSeconds(0.001f);
		anim.SetBool (TN,false);
	}
	public 	void GetHit(){
		anim.SetTrigger ("GetHit");

	}
	public void DisableMovement(int M){
		if(M >= 1){
			dontMove = true;
		}else{
			dontMove = false;
		}
		if(M == 0)
			dontMove = false;
		
		//dontMove = M;
	}
	void SpawnEffect(){
		SS.FindEffect(CurrSpellName);

	}
	void FinishSpell(){
		SS.SpellPlaying =false;
	}
	public void DisapeLayer(){
		anim.SetLayerWeight(2, 0f);

	}
	void DisableAim(){
		anim.SetBool("Aim",false);

	}
	/*
	void SpawnEffect2(){
		ES.SpawnEffect2 ();
	}
	*/
}
