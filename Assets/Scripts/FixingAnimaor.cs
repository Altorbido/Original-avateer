using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingAnimaor : MonoBehaviour
{
	public Animator m_Animator;
	public string m_ClipName;
	AnimatorClipInfo[] m_CurrentClipInfo;

	float m_CurrentClipLength;
	public AnimationClip[] AttackClips;
	public bool attacing;
	void Update()
	{
		//	m_Animator = gameObject.GetComponent<Animator>();

		//Get them_Animator, which you attach to the GameObject you intend to animate.
		//Fetch the current Animation clip information for the base layer
		m_CurrentClipInfo = this.m_Animator.GetCurrentAnimatorClipInfo(1);
		//Access the current length of the clip
		if(m_CurrentClipInfo[0].clip ){
		m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
		//Access the Animation clip name
		m_ClipName = m_CurrentClipInfo[0].clip.name;
		}
		/*
		if(attacing ){
			m_Animator.SetLayerWeight(1, 1f);

		}else{
			m_Animator.SetLayerWeight(1, 0f);
		}
		*/
		Fixlayer();
	}
	void Fixlayer(){
		if(AttackClips.Length == 0)
		return;
		foreach (AnimationClip Clip in AttackClips) {

			if( Clip.name ==  m_ClipName){
				//Debug.Log(Clip.name);
				attacing = true;
			m_Animator.SetLayerWeight(1, 1f);

				return;

			}else{
				attacing = false;
			m_Animator.SetLayerWeight(1, 0f);

			}
		}
	
	}
	
}
