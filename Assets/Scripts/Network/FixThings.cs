using UnityEngine;
using System.Collections;

public class FixThings : Photon.MonoBehaviour
{
    public Camera cam;
	public GameObject TPSCam;
       // public Camera Tps;

    public AudioListener listener;
    //public GameObject fp;

    public GameObject tp;
  
    public GameObject Player;

	public Movement controllerScript;
    public float layerNumber = 16f;
	public int l = 16;
	public CharacterController CC;
    // Use this for initialization
    void Start()
    {
		controllerScript = GetComponent<Movement>();
         if (photonView.isMine)
        {
           // Tps.enabled = true;
            cam.enabled = true;
            listener.enabled = true;
			controllerScript.enabled = true;
			TPSCam.SetActive (true);
        }
        else
        {
            cam.enabled = false;
           // Tps.enabled = false;
            listener.enabled = false;
			controllerScript.isControllable = false;
			TPSCam.SetActive(false);

        }
		SetEveryThing ();
		TeamSpawn ();

    }

   
	void SetEveryThing(){
		if (photonView.isMine)
		{
			//fp.SetActive(true);
			tp.SetActive(true);
		//	Tps.enabled = true;
			cam.enabled = true;
			listener.enabled = true;
			controllerScript.enabled = true;
			if(Player.gameObject.layer != 10){
				Player.gameObject.layer = 10;
				SetLayerRecursivelyMine(Player , 10);
			}
		}
		else
		{
		//	Tps.enabled = false;
			cam.enabled = false;
			listener.enabled = false;
		//	fp.SetActive(false);
			tp.SetActive(true);
		//	bool WaveMode = (bool)PhotonNetwork.room.CustomProperties["WaveMode"];
			if(Player.gameObject.layer != 2){
				Player.gameObject.layer = 2;
				SetLayerRecursivelyMine(Player , 2);
			}
			controllerScript.isControllable = false;
		}
	}

	public void TeamSpawn(){
		if (PhotonNetwork.room.CustomProperties ["WaveMode"] != null) {
			bool WaveMode = (bool)PhotonNetwork.room.CustomProperties ["WaveMode"];
			if (WaveMode) {
				if (!photonView.isMine) {
					SetLayerRecursively (Player, 13);

				}
			}
		}
	}
    public static void SetLayerRecursively(GameObject tp, int layerNumber) {
		bool WaveMode = (bool)PhotonNetwork.room.CustomProperties["WaveMode"];

        if (tp == null) return;
        foreach (Transform trans in tp.GetComponentsInChildren<Transform>(true)) {
			if(!trans.GetComponent<Collider>()){
			trans.gameObject.layer = layerNumber;
			}else{
				trans.gameObject.layer = 13;

			}
        }
    }
	public static void SetLayerRecursivelyMine(GameObject tp, int layerNumber) {
	//	bool WaveMode = (bool)PhotonNetwork.room.CustomProperties["WaveMode"];
		
		if (tp == null) return;
		foreach (Transform trans in tp.GetComponentsInChildren<Transform>(true)) {
				trans.gameObject.layer = layerNumber;

		}
	}
}
