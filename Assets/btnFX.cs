using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
	public AudioSource myFx;
	public AudioClip hoverFx;
	public AudioClip clickFx;
	public AudioClip BackSound;
	public AudioClip DropSound;
	public void HoverSound()
	{
		myFx.PlayOneShot (hoverFx);
	}
	public void ClickSound()
	{
		myFx.PlayOneShot (clickFx);
	}
	public void backSound (){
		myFx.PlayOneShot (BackSound);	
	}
	public void dropSound (){
		myFx.PlayOneShot (DropSound);
	}

}
