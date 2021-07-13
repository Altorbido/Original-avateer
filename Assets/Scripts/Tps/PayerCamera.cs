using UnityEngine;
using System.Collections;

public class PayerCamera : Photon.MonoBehaviour {
	public bool canControl = true;
	public GameVIEW gameView;
	public Vector3 normalOffset_FPS;
	public Vector3 normalOffset_TPS;

	public Camera weaponRenderCamera;
		public Camera TpsCamera;

	public float reticuleSize = 15.0f;

	 public Transform target;
	public Texture2D crosshair;


	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

	public bool tps = false;

	Vector3 offset;
	public Rect position ;

	public enum GameVIEW{
		FirstPersone,
		ThirdPersone,
	}
	

	
	public void Update(){

		 if (photonView.isMine)
        {
			//sensitivityY = SG.sensitivity;
			//CurrWeap = S.CurrentWeapon.GetComponent<Weapon>();
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
		
	}
}
	void LateUpdate () {
		 if (photonView.isMine)
        {
		if(tps){
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

		}else{
						//rotationY += lookAngle;

		}
		
	   if(!canControl)
			return;
		
		if (target) {
			switch(gameView){
				case GameVIEW.FirstPersone :
								tps =false;
				
					TpsCamera.enabled = false;
					break;
				
				case GameVIEW.ThirdPersone :
				tps =true;
					offset = normalOffset_TPS;
	weaponRenderCamera.enabled = false;
					TpsCamera.enabled = true;
					break;
			}
			//lookAngle
	     	Quaternion rotation = Quaternion.Euler(-rotationY,  target.eulerAngles.y, 0);
			transform.rotation = rotation;
			transform.position = calcPOS(offset, rotation);
			Debug.DrawRay(calcPOS(normalOffset_FPS, rotation), transform.TransformDirection(Vector3.forward) * 1000 , Color.green, 0, true);
		}
	}
	
}
public	Vector3 calcPOS(Vector3 _offset, Quaternion _rotation){
		return _rotation * new Vector3(_offset.x, 0, -_offset.z) + target.position + new Vector3(0, _offset.y, 0);
	}
	
	void OnPlayerSpawn(GameObject player){
		target = player.transform;
	}
	
	void OnGUI(){
		 if (photonView.isMine)
        {
		switch(gameView){
				case GameVIEW.ThirdPersone :
			
					position = new Rect((Screen.width - reticuleSize) * 0.5f, (Screen.height - reticuleSize) * 0.5f, reticuleSize, reticuleSize);

				break;
		}
		GUI.DrawTexture(position, crosshair);
		}
	}
	
	
}
