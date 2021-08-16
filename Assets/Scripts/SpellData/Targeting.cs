using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Be Sure To Include This

public class Targeting : Photon.MonoBehaviour
{
        public bool lockedOn;//Keeps Track Of Lock On Status    
 public Transform target;
   public Image image;//Image Of Crosshair
    public Camera cam; //Main Camera
public LayerMask mask;
public Transform MyCollider;
public Vector3 Offset;
    // Start is called before the first frame update
    void Start()
    {
                cam = Camera.main;       
image = GameObject.FindGameObjectWithTag("Target").GetComponent<Image>();
            image.enabled = false;

    }

    // Update is called once per frame
    void Update()
			{	if (photonView.isMine) {
             if(target && lockedOn){
               image.enabled = true;
             }else{
                                image.enabled = false;

             }
         if (Input.GetKeyDown(KeyCode.E) && !lockedOn)
        {
                lockedOn = true;
                 target = FindTarget();

                if(target)
               image.enabled = true;

            
        }   else if (Input.GetKeyDown(KeyCode.E) && lockedOn)
        {
            lockedOn = false;
            image.enabled = false;
            target = null;
        } 
           if (lockedOn)
        {
            target = FindTarget();
 if(target){
            image.transform.position = cam.WorldToScreenPoint(target.transform.position+Offset);   
            image.transform.Rotate(new Vector3(0, 0, -1));
 }else{
     target = FindTarget();
 }
        } 
    }
            }
    public void LockOn(bool Lock){

           if ( !lockedOn && Lock)
        {
                lockedOn = true;
                 target = FindTarget();

                if(target)
               image.enabled = true;

            
        }   else if (image && !Lock && lockedOn)
        {
            lockedOn = false;
            image.enabled = false;
        } 
    }
public Transform FindTarget (){

  //First Create A Vector3 With Dimensions Based On The Camera's Viewport
     GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        Vector3 here = transform.position; 

     Debug.Log(targets.Length);

     foreach (GameObject T in targets)
        {
            if(T != this.gameObject  && !T.transform.IsChildOf(this.transform)){
    //   Vector3 enemyPosition = cam.WorldToViewportPoint(T.transform.position);

        Vector3 pos = T.transform.position;
       RaycastHit  hit;
    //   if (Physics.Linecast(here, pos, out hit,mask) && hit.transform == T.transform && hit.transform != MyCollider)
   //Ray ray = new Ray(transform.position, transform.position - T.transform.position);
    Debug.DrawRay(transform.position,  T.transform.position - cam.transform.position , Color.red);
       				Vector3 targetDir = T.transform.position - cam.transform.position;
				float angle = Vector3.Angle (targetDir, cam.transform.forward);

             if (Physics.Linecast(here, pos, out hit,mask)  && angle <= 60f)   
        {
            Debug.Log(hit.transform.name);
return T.transform;
Debug.Log("Find one");
        }  
   }
        }
        return null;
   
}
}
