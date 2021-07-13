using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetowrkMang : Photon.MonoBehaviour
{
    public Character Controller;
    public PlayerInput PI;
    
  public float normVerticalSpeed{ get; private set; }
public float normHorizontalSpeed{ get; private set; }
public float jumpSpeed{ get; private set; }
public bool IsGrounded;
public bool isMine;
		public CharacterController _characterController; // The Unity's CharacterController
 public int localLayer;
    // Start is called before the first frame update

void Start(){
  if (!photonView.isMine) {
            Controller.enabled = false;
            PI.enabled = false;
        }else{
          SetLayerRecursivelyMine(this.gameObject,localLayer);

          isMine = true;
        }
}
         private void FixedUpdate(){
           if(photonView.isMine){
		 normHorizontalSpeed = Controller.HorizontalVelocity.magnitude / Controller.MovementSettings.MaxHorizontalSpeed;
	 jumpSpeed = Controller.MovementSettings.JumpSpeed;
	 normVerticalSpeed = Controller.VerticalVelocity.y.Remap(-jumpSpeed, jumpSpeed, -1.0f, 1.0f);
   IsGrounded = _characterController.isGrounded;
           }
         }


	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		
		// Send data to server
		if (stream.isWriting)
		{
			stream.SendNext(normVerticalSpeed);
			stream.SendNext(normHorizontalSpeed);
			stream.SendNext(jumpSpeed);
			stream.SendNext(IsGrounded);

		}
		else
		{
			normVerticalSpeed = (float)stream.ReceiveNext();
				normHorizontalSpeed = (float)stream.ReceiveNext();
			jumpSpeed = (float)stream.ReceiveNext();
			IsGrounded = (bool)stream.ReceiveNext();

		}
	}
    // Update is called once per frame
    void Update()
    {
		if (!photonView.isMine) {
            Controller.enabled = false;
            PI.enabled = false;
        }else{
          isMine = true;
        }
    }
    public static void SetLayerRecursivelyMine(GameObject Object, int layerNumber) {
	//	bool WaveMode = (bool)PhotonNetwork.room.CustomProperties["WaveMode"];
		
		if (Object == null) return;
		foreach (Transform trans in Object.GetComponentsInChildren<Transform>(true)) {
				trans.gameObject.layer = layerNumber;

		}
	}
}
