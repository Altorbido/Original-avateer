using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

	public class SyncZone : Photon.MonoBehaviour {
		Hashtable Zone = new Hashtable() { { "ZoneScale", TheScale }};
	public static Vector3 TheScale = new Vector3 (0,0,0);
	public Vector3 RealScale = new Vector3 (0,0,0);
	public  Vector3 ShowScale = new Vector3 (0,0,0);

	public Transform _Zone;
	public bool Sync;

	// Use this for initialization
	void Start () {
		/*
		Zone ["ZoneScale"] = _Zone.localScale;
				PhotonNetwork.room.SetCustomProperties (Zone); 
*/
	}
	
	// Update is called once per frame
	void Update () {
		if (Sync && PhotonNetwork.connected) {
			if (PhotonNetwork.player.isMasterClient) {
				Zone ["ZoneScale"] = _Zone.localScale;
				if(ShowScale != _Zone.localScale){
				PhotonNetwork.room.SetCustomProperties (Zone); 
				}
			}
			/*
			object teamId;
			if (PhotonNetwork.room.customProperties.TryGetValue ("ZoneScale", out teamId)) {
				ShowScale =  (Vector3)teamId;
				RealScale=  (Vector3)teamId;
			}
*/
		}
	}
	public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
	{
		if (propertiesThatChanged.ContainsKey("ZoneScale"))
		{
			ShowScale = (Vector3)propertiesThatChanged["ZoneScale"];

		}
	}
}
