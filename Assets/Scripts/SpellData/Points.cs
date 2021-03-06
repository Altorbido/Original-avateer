using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : Photon.MonoBehaviour
{
    public List<Vector3> bluePositions;
    public GameObject Effect;
    public float spawnRate = 0.2f;
    public float Space;
    public LayerMask Mask;
    public Quaternion Rotation;
    public float offset;
    private void Start()
    {
        if (!photonView.isMine)
            return;

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd, Color.green);
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, fwd, out objectHit, Mask))
        {
            //do something if hit object ie
            if (objectHit.transform)
            {
                Test(transform.position, objectHit.point);
            }
        }
        else {
           

            Test(transform.position, transform.TransformDirection(transform.forward * offset));
            Debug.Log(transform.TransformDirection(transform.forward * offset));

        }
        StartCoroutine(SpawnRate());


    }
    private void Update()
    {
        /*  if(bluePositions.Count > 0)
          {

              foreach (Vector3 point in bluePositions) {

                  Instantiate(Effect, point, Quaternion.identity);
              }
          }*/
    }
    void Test(Vector3 V1, Vector3 V2)
    {
        Vector3 directionVector = (V2 - V1);
        float Distance = Vector3.Distance(V1, V2);
        Vector3 normalVector = directionVector.normalized;

        float vectorMagnitude = directionVector.magnitude;
        Space = Distance;

        float spacing = vectorMagnitude / Space;
        
        bluePositions = new List<Vector3>();
       float  NOO = Distance / spacing;
        for (var i = 0; i < NOO; i++)
        {

            Vector3 bluePos = V1 + (normalVector * spacing * i);

            bluePositions.Add(bluePos);
            //  Instantiate(Effect, bluePos, Quaternion.identity);

        }
    }
    IEnumerator SpawnRate() {
        foreach (Vector3 Point in bluePositions)
        {
            int index = bluePositions.IndexOf(Point);
            if (index == 0 || index == 1 || index == 2)
            {
                continue;
            }
            yield return new WaitForSeconds(spawnRate);

           // PhotonNetwork.Instantiate(Effect, Point, Quaternion.identity);
          
            GameObject Eff = PhotonNetwork.Instantiate(Effect.name, Point, transform.rotation, 0) as GameObject;
            Eff.transform.SetParent(this.transform);
            Eff.GetComponent<FireReng>().Owner = this.transform;
        }
    }

}
   
