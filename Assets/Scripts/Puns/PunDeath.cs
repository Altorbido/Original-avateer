using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Linq;

//Show if he dead or not

public class PunDeath : Photon.MonoBehaviour 
{
	
	public const string Death ="0";


   // public  int DeathCount;

	
	// Update is called once per frame
	void Start () {

		PhotonNetwork.player.SetDeath (0);

	}


}
static class CountDeathExtensions
{
  
	public static void SetDeath (this PhotonPlayer player, int newcount)
	{
		Hashtable Counter = new Hashtable() { { "Death", PunDeath.Death}};
	//	Counter [PunDeath.DeathCount] = newcount;
		Counter ["Death"] = newcount;

		player.SetCustomProperties (Counter);  
		}
public static int GetDeath (this PhotonPlayer player)
	{
		
		object teamId;
		if (player.customProperties.TryGetValue ( "Death", out teamId)) {
			return (int)teamId;
		}
			
			return 0;
	}

		public static void AddDeath (this PhotonPlayer player, int CounterToAddToCurrent)
	{
		int current = player.GetDeath ();
		current = current + CounterToAddToCurrent;

		Hashtable Counter = new Hashtable() { { "Death", PunDeath.Death}};
		Counter ["Death"] = current;
							
		player.SetCustomProperties (Counter); 
                
		}



		}
