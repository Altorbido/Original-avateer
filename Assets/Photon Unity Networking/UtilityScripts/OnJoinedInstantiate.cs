using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnJoinedInstantiate : MonoBehaviour
{
    public Transform SpawnPosition;
    public float PositionOffset = 2.0f;
    public GameObject[] PrefabsToInstantiate;   // set in inspector
	public Camera SceneCame;

	public Image HealthSlider;
	public Image ManaSlider;
	public Image Spell1SLider;
	public Image Spell2SLider;
	public Image Spell3SLider;

	public GameObject HudCanves;
    public void OnJoinedRoom()
    {
        if (this.PrefabsToInstantiate != null)
        {
            foreach (GameObject o in this.PrefabsToInstantiate)
            {
                Debug.Log("Instantiating: " + o.name);

                Vector3 spawnPos = Vector3.up;
                if (this.SpawnPosition != null)
                {
                    spawnPos = this.SpawnPosition.position;
                }

                Vector3 random = Random.insideUnitSphere;
                random.y = 0;
                random = random.normalized;
                Vector3 itempos = spawnPos + this.PositionOffset * random;

				GameObject Player   =  PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
            	if (SceneCame) {
					SceneCame.gameObject.SetActive (false);
				}
                if(Player.GetComponent<HUD> ()){
            	Player.GetComponent<HUD> ().ManaSlider = ManaSlider;
				Player.GetComponent<HUD> ().HealthSlider = HealthSlider;
				Player.GetComponent<HUD> ().Spell1SLider = Spell1SLider;
				Player.GetComponent<HUD> ().Spell2SLider = Spell2SLider;
				Player.GetComponent<HUD> ().Spell3SLider = Spell3SLider;
                }
				HudCanves.SetActive(true);
			
            }
        }
    }
}
