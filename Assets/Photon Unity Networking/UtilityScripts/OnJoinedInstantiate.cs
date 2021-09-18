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
    public GameObject SpawndPlayer;
    public GameObject SpawnButton;
    public bool Conected;
    private void Update()
    {
        if (!SpawndPlayer && Conected) {
            Screen.lockCursor = false;
            if (SceneCame)
            {
                SceneCame.gameObject.SetActive(true);
            }
            SpawnButton.SetActive(true);
        }
    }
    public void SpawnPlayer() {
        SpawnButton.SetActive(false);

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

                SpawndPlayer = PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
                if (SceneCame)
                {
                    SceneCame.gameObject.SetActive(false);
                }
                if (SpawndPlayer.GetComponent<HUD>())
                {
                    SpawndPlayer.GetComponent<HUD>().ManaSlider = ManaSlider;
                    SpawndPlayer.GetComponent<HUD>().HealthSlider = HealthSlider;
                    SpawndPlayer.GetComponent<HUD>().Spell1SLider = Spell1SLider;
                    SpawndPlayer.GetComponent<HUD>().Spell2SLider = Spell2SLider;
                    SpawndPlayer.GetComponent<HUD>().Spell3SLider = Spell3SLider;
                }
                HudCanves.SetActive(true);

            }
        }
    }
    public void OnJoinedRoom()
    {
        Conected = true;
        SpawnPlayer();
    }
}
