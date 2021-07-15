using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour {
	public Image ManaSlider;
	public Image HealthSlider;
	public PlayerVitals HealthScript;
	public SpellSystem ManaScript;
	public Image Spell1SLider;
	public Image Spell2SLider;
	public Image Spell3SLider;

	public float CoolDownTimerFirstSpell; //CDTFS
	// Update is called once per frame
	void Update () {
		if (ManaSlider) {
			ManaSlider.fillAmount = ManaScript.Mana* 0.01f;
		}
		CoolDownTimerFirstSpell  = (ManaScript.MYSpellsList[0].timer* 0.1f) + 0.3f;
		if(Spell1SLider){
		//	Spell1SLider.maxValue = ManaScript.MYSpellsList[0].CoolDown;

			Spell1SLider.fillAmount = CoolDownTimerFirstSpell ;
		}
		if(Spell1SLider){
			//	Spell1SLider.maxValue = ManaScript.MYSpellsList[0].CoolDown;

			Spell2SLider.fillAmount = (ManaScript.MYSpellsList[1].timer* 0.1f)+ 0.3f ;
		}
		if(Spell3SLider){
			//	Spell1SLider.maxValue = ManaScript.MYSpellsList[0].CoolDown;

			Spell3SLider.fillAmount = (ManaScript.MYSpellsList[2].timer* 0.1f) + 0.3f ;
		}
		if(HealthSlider){
			HealthSlider.fillAmount = HealthScript.hitPoints* 0.01f;

		}
	}
}
