using UnityEngine;
using System.Collections;

[System.Serializable]
public class state
{
    public string name = "Stand";
    public float speed;
    public float height;
    public Vector3 center;
    public Vector3 camPos;
}

public class Movement : Photon.MonoBehaviour
{
	public bool isControllable = true;

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
	public float jumpY;
    public CharacterController controller;
    public PlayerVitals pv;
	public PlayerAnim PA;

    public float hor;
	public float Rothor;

    public float ver;
    public int state;
    public bool running;
	public bool POpen;

    public state[] states = new state[3];
    private state curState;

    //public Transform adjTrans;
	public bool Andriod;
    private float adjvar = 1;
	public bool isSliding;
	private float slideTimer  = 0.0f;
	public float slideTimerMax = 1f; // time while sliding
	private  Vector3 slideForward;
	public float slideSpeed = 15f; // slide speed
	public bool crouch;
	private bool leftwall;
	private bool Rightwall;

	public LayerMask WallMask;
	public float WallDistance = 2f;
	public float wallGravity;
	public bool ROLW; //running on left wall;
	public bool RORW; //running on Right wall;
	public bool WallRanning;

	public float SliedDely = 0.03f;
	private float SlideTime = 0;
	public Vector3 lookPos;
	public Transform cam; // A reference to the main camera in the scenes transform
	public float turnAmount;
	public SpellSystem SS;

	public bool ForntFlip;
	private bool DoupleTap;
	private bool OneTap;
	public float DJT;
	public float DJTTimer;
	public Animator anim;

	public float SinMini = 50f;
	public float SinMax = 230f;
	public float SinVer = -10f;

	void Update()
    {
		if (photonView.isMine) {
			TurnTowardsCameraForward(); // makes the character face the way the camera is looking
			ApplyExtraTurnRotation(); 

			if (isControllable) {	// don't move player at all if not controllable.
				if (!POpen) {
					
					CheckState ();
					CheckInput ();
					if (SlideTime > 0) SlideTime -= Time.deltaTime;
					SlideTime = Mathf.Clamp (SlideTime, 0, 3);
					bool Leg = anim.GetBool("Leg");
					if (controller.isGrounded ) {

						if (Screen.lockCursor) {
							ver = Input.GetAxis ("Vertical");
							hor = Input.GetAxis ("Horizontal");
							Rothor = Input.GetAxis ("Horizontal");

						} else {
							ver = 0;
							hor = 0;
						}
						if(ver ==0 && hor !=0 ){
							hor = 0;
						}
						if(ver < 0){
							Rothor  = 0;
						}
					
						if(Input.GetKeyDown(KeyCode.W)   ){
							if(!OneTap){
							OneTap =true;
								DJT = DJTTimer ;

								Debug.Log("OneTap");
							}else{
								Debug.Log("DoupleTap");
								DoupleTap = true; 
								DJT = DJTTimer ;
							}
						}
						ForntFlip = DoupleTap;
					
					


												if (Mathf.Abs (ver) > 0.1f && Mathf.Abs (hor) > 0.1f)
							adjvar = 0.701f;
						else
							adjvar = 1f;
						// - slide -    
						/*
						if (Input.GetKeyDown ("f") && !isSliding && running && SlideTime == 0 ) { // press F to slide
							slideTimer = 0.0f; // start timer
							isSliding = true;
							slideForward = transform.forward;

						}
						*/

						if (isSliding) {
							//	h = 0.5 * height; // height is crouch height
							controller.height = states [2].height;
							controller.center = states [2].center;

							speed = slideSpeed; // speed is slide speed
							if(ver < 0 ){
							moveDirection = slideForward * speed;
							}else{
								float BackSPeed = speed-4f;
								moveDirection = slideForward * BackSPeed;

							}
							slideTimer += Time.deltaTime;
							if (slideTimer > slideTimerMax) {
								isSliding = false;
								state = 0;
							}
							SlideTime = SliedDely;
						}else if(ForntFlip){

						
							if (state == 0)
								Flip();
							else if (state == 1)
								state = 0;
							else if (state == 2)
								state = 1;
						} else {
							moveDirection = new Vector3 (hor * adjvar, -2f, ver * adjvar);
							moveDirection = transform.TransformDirection (moveDirection);
							moveDirection *= speed;
							if (Input.GetButtonDown ("Jump") && Screen.lockCursor) {
								if (state == 0)
									moveDirection.y = jumpSpeed;
								else if (state == 1)
									state = 0;
								else if (state == 2)
									state = 1;
							}

						}
					
							
					}
					if (!controller.isGrounded && leftwall) {
						gravity = wallGravity;
						ROLW = true;
					} else {
						ROLW = false;
					}
					if (!controller.isGrounded && Rightwall) {
						gravity = wallGravity;
						RORW = true;
					} else {
						RORW = false;
					}
					if (!RORW && !ROLW) {
						gravity = 20;

					}

					lookPos =  cam != null
						? transform.position + cam.forward*100
						: transform.position + transform.forward*100;
					

					moveDirection.y -= gravity * Time.deltaTime;
				//	transform.Rotate ( 100* hor *Vector3.up * Time.deltaTime);
					if(PA.dontMove){
						ver = 0;
						hor = 0;
						moveDirection = Vector3.zero;
					}

					controller.Move (moveDirection * Time.deltaTime);
				} else {
					gravity = 0;
				}
			}
			if (WallRanning) {
				RaycastHit hitleft;
				if (Physics.Raycast (transform.position, -transform.right, out hitleft, WallDistance, WallMask)) {
					leftwall = true;
				} else {
					leftwall = false;
				}
				RaycastHit hitright;
				if (Physics.Raycast (transform.position, transform.right, out hitright, WallDistance, WallMask)) {
					Rightwall = true;
				} else {
					Rightwall = false;
				}
			}
			jumpY = moveDirection.y;		
			singleOrDouble();


		} else {
			this.enabled =false;	
		}

    }
	void Flip(){
		slideForward = transform.forward;
		speed += 3f;
	moveDirection = slideForward * speed ;
		moveDirection.y = jumpSpeed + 1;

		Debug.Log("JUmp");
	}
	private void TurnTowardsCameraForward()
	{
		// automatically turn to face camera direction,
		// when not moving, and beyond the specified angle threshold
		if(SS.Aim && controller.isGrounded){
			Vector3 lookDelta = transform.InverseTransformDirection (lookPos - transform.position);
			float lookAngle = Mathf.Atan2 (lookDelta.x, lookDelta.z) * Mathf.Rad2Deg;

			// are we beyond the threshold of where need to turn to face the camera?
			if (Mathf.Abs (lookAngle) > 10) {
				Rothor += lookAngle *10f * 0.001f;

			}
		}
		if (Mathf.Abs (Rothor) < .01f &&  ver > .01f) {
			Vector3 lookDelta = transform.InverseTransformDirection (lookPos - transform.position);
			float lookAngle = Mathf.Atan2 (lookDelta.x, lookDelta.z) * Mathf.Rad2Deg;

			// are we beyond the threshold of where need to turn to face the camera?
			if (Mathf.Abs (lookAngle) > 30) {
				Rothor += lookAngle * 5f * 0.001f;

			}
		} 
	}
	private void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(SinMini, SinMax,
			ver );
		transform.Rotate(0, Rothor*turnSpeed*Time.deltaTime, 0);
	}

    void CheckInput()
    {
		if (isControllable)	// don't move player at all if not controllable.
		{
			if (Input.GetButtonDown("Crouch") && controller.isGrounded && Screen.lockCursor)
        {
				if (state == 0 && !isSliding) state = 1;
				else if (state == 1 && !isSliding) state = 0;
			}
			if (isSliding && controller.isGrounded && Screen.lockCursor)
			{
				if (state == 0 ) state = 2;
				else if (state == 1 ) state = 2;
			}
			//&& Input.GetAxis ("Vertical") > 0  && !Input.GetKey(KeyCode.S)
			running = (controller.isGrounded && controller.velocity.magnitude > 1 && state == 0 && Input.GetButton("Run")  && Input.GetAxis ("Vertical") > 0  && !Input.GetKey(KeyCode.S)&& Screen.lockCursor);
    }
}

    void CheckState()
    {
        if (running) {
			curState = states [0];
		} else if (state == 0) {
			curState = states [1];
		} else if (state == 1) {
			curState = states [2];
		} else if (state == 2) {
			curState = states [3];

		}

        speed = curState.speed;
        controller.height = curState.height;
        controller.center = curState.center;
        //adjTrans.localPosition = Vector3.Lerp(adjTrans.localPosition, curState.camPos, Time.deltaTime * 10);
    }
	void singleOrDouble(){
		if (DJT > 0) DJT -= Time.deltaTime;
		DJT = Mathf.Clamp (DJT, 0, 4);
		/*
		if(OneTap &&DJT != DJTTimer ){
			DJT = DJTTimer ;
			OneTap = false;

		}
		*/
		if(DJT == 0){
		DoupleTap = false;
		OneTap = false;
		ForntFlip = false;
		}

		}
}
