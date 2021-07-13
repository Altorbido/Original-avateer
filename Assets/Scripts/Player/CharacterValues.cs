using UnityEngine;
using System.Collections;

public class CharacterValues : Photon.MonoBehaviour
{
    public float velMag;
    public bool grounded;
    public float height;
    public Vector3 center;
    public float hor;
	public float Rothor;

    public float ver;
	public float JumpY;
    public float mouseX;
    public float mouseY;
    public int state;
    public bool running;
    public bool aiming;
	//public bool ROLW;
	//public bool RORW;
    public float speed;
    public float velPercent;
	 
    public Mouse ml;
    public Movement m;
	public Transform fps;
	 public float fpsAim;
	public bool isSliding;
	public  bool FrontFlip;

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		
		// Send data to server
		if (stream.isWriting)
		{
			stream.SendNext(velMag);
			stream.SendNext(grounded);
			stream.SendNext(height);
			stream.SendNext(center);
			stream.SendNext(hor);
			stream.SendNext(Rothor);

			stream.SendNext(ver);
			stream.SendNext(mouseX);
			stream.SendNext(mouseY);
			stream.SendNext(state);
			stream.SendNext(running);
			stream.SendNext(aiming);
			stream.SendNext(speed);
			stream.SendNext(isSliding );
			//stream.SendNext(ROLW );
			//stream.SendNext(RORW );
			stream.SendNext(JumpY );

			stream.SendNext(FrontFlip );

		}
		else
		{
			velMag = (float)stream.ReceiveNext();
			grounded = (bool)stream.ReceiveNext();
			height = (float)stream.ReceiveNext();
			center = (Vector3)stream.ReceiveNext();
			hor = (float)stream.ReceiveNext();
			Rothor = (float)stream.ReceiveNext();

			ver = (float)stream.ReceiveNext();
			mouseX = (float)stream.ReceiveNext();
			mouseY = (float)stream.ReceiveNext();
			state = (int)stream.ReceiveNext();
			running = (bool)stream.ReceiveNext();
			aiming = (bool)stream.ReceiveNext();
			speed = (float)stream.ReceiveNext();
			isSliding = (bool)stream.ReceiveNext();
		//	ROLW = (bool)stream.ReceiveNext();
			JumpY = (float)stream.ReceiveNext();
			FrontFlip = (bool)stream.ReceiveNext();

	
		}
	}

    void Update()
    {
         if (photonView.isMine)
        {
 
            mouseX = ml.mouseX;
            mouseY = ml.mouseY;
            hor = m.hor;
			hor = m.Rothor;

            ver = m.ver;
            state = m.state;
            running = m.running;
            grounded = m.controller.isGrounded;
            velMag = m.controller.velocity.magnitude;
				speed = m.speed;
            velPercent = velMag / speed;
			isSliding =m.isSliding;
			JumpY = m.jumpY;
			//RORW = m.RORW;
			FrontFlip = m.ForntFlip;
        }
        else
        {
            ml.mouseX = mouseX;
            ml.mouseY = mouseY;
            m.hor = Rothor;
            m.ver = ver;
            m.state = state;
            m.running = running;
			m.jumpY =JumpY;
			m.ForntFlip  = FrontFlip;

            velPercent = velMag / speed;
        }
    }
}
