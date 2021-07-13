using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Killes : Photon.MonoBehaviour {
	public const string Kill ="0";
	void Start () {
		PhotonNetwork.player.SetKilles (0);
	}
	
	// Update is called once per frame
	

 public void Update()
 {
	 
     
}
}
	static class KillesExtensions
{

	public static void SetKilles (this PhotonPlayer player, int newKilles)
	{
	
  Hashtable kill = new Hashtable() { { "Kill", Killes.Kill }};
		kill ["Kill"] = newKilles;
		player.SetCustomProperties(kill);  
		}
public static int GetKilles (this PhotonPlayer player)
	{
		
		object teamId;
		if (player.CustomProperties.TryGetValue ("Kill", out teamId)) {
			return (int)teamId;
		}
			
			return 0;
	}

		public static void AddKilles (this PhotonPlayer player, int KillesToAddToCurrent)
	{

			            int current = player.GetKilles();
		current = current + KillesToAddToCurrent;
  Hashtable kill = new Hashtable() { { "Kill", Killes.Kill }};
		kill ["Kill"] = current;
		player.SetCustomProperties(kill); 
                
		}
		}

