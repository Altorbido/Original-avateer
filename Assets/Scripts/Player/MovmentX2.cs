using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentX2 : MonoBehaviour
{

	public Animator anim;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0F;
	public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		ApplyAnimation();

    }
	void ApplyAnimation(){
		
		 float ver = Input.GetAxis ("Vertical");
		float hor = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Horizontal", hor);
		anim.SetFloat ("Vertical",ver );
	}
	public void OnAnimatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		rigidbody.rotation = anim.rootRotation;
		if ( Time.deltaTime > 0) {
			Vector3 v = (anim.deltaPosition * 1) / Time.deltaTime;

			// we preserve the existing y part of the current velocity.
			v.y = rigidbody.velocity.y;
			rigidbody.velocity = v;
		}
	}

}
