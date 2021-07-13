using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class BotKilles : Photon.MonoBehaviour {
	public const string Kill ="0";

	void Start () {
		PhotonNetwork.player.SetKilles (0);
	}
	
	// Update is called once per frame
	

 public void Update()
 {
	 
     
}
}
	static class BotKillesExtensions
{

	public static void SetBotKilles (this PhotonPlayer player, int newKilles)
	{
	
  Hashtable kill = new Hashtable() { { "Kill", Killes.Kill }};
		kill ["Kill"] = newKilles;
							Debug.Log(kill);
		player.SetCustomProperties(kill);  
		}
	public static int GetBotKilles (this PhotonPlayer player)
	{
		
		object teamId;
		if (player.CustomProperties.TryGetValue ("Kill", out teamId)) {
			return (int)teamId;
		}
			
			return 0;
	}

	public static void AddBotKilles (this PhotonPlayer player, int KillesToAddToCurrent)
	{

			            int current = player.GetKilles();
		current = current + KillesToAddToCurrent;
  Hashtable kill = new Hashtable() { { "Kill", Killes.Kill }};
		kill ["Kill"] = current;
							Debug.Log(kill);

		player.SetCustomProperties(kill); 
                
		}
		}

