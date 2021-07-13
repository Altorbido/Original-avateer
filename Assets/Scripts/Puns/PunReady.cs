using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PunReady : Photon.MonoBehaviour {
	public const bool Ready =false;

	void Start () {
		PhotonNetwork.player.SetReady (false);
	}
	
	// Update is called once per frame
	

 public void Update()
 {
	 
     
}
}
	static class ReadyExtensions
{

	public static void SetReady (this PhotonPlayer player, bool NewState)
	{
	
  Hashtable R = new Hashtable() { { "Ready", PunReady.Ready }};
		R ["Ready"] = NewState;
							Debug.Log("Ready" + PunReady.Ready);
		player.SetCustomProperties(R);  
		}
public static bool GetReady (this PhotonPlayer player)
	{
		
		object teamId;
		if (player.CustomProperties.TryGetValue ("Ready", out teamId)) {
			return (bool)teamId;
		}
			
			return false;
	}

	
		}

