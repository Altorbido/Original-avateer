using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	public static class CharacterAnimatorParamId
	{
		public static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
		public static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");
		public static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
	}

	public class CharacterAnimator : MonoBehaviour
	{
		public Animator _animator;
         public NetowrkMang NM;
		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

       private void FixedUpdate()
		{
			if(!NM.isMine){
			UpdateState();
						_animator.applyRootMotion = false;

			}
		}
		public void UpdateState()
		{
			_animator.applyRootMotion = NM.IsGrounded;
			_animator.SetFloat(CharacterAnimatorParamId.HorizontalSpeed, NM.normHorizontalSpeed);
			_animator.SetFloat(CharacterAnimatorParamId.VerticalSpeed, NM.normVerticalSpeed);
			_animator.SetBool(CharacterAnimatorParamId.IsGrounded, NM.IsGrounded);
		}
		public void GetHit (){
			_animator.SetTrigger("Hit");
		}
	}

