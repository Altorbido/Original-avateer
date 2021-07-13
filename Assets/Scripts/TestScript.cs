using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	public GameObject Effect;
	public Transform SPawnPoint;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
		if(Input.GetButtonDown("Fire1")){
			GameObject Ef = Instantiate (Effect, SPawnPoint.position,Quaternion.Euler(transform.forward));
			Ef.transform.SetParent (SPawnPoint);
		}
    }
}
