using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wtf : MonoBehaviour {
	public UnityEngine.UI.Text _Ammo; 
	public string test; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_Ammo.text = test; 
	}
}
