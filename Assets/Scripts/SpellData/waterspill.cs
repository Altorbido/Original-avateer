using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterspill : MonoBehaviour
{

    public List<Vector3> Points;
    public float range;
    public float pointRange;

    public LayerMask Mask;
    public int MaxPoint;

    public Vector3 CurrentPoint;
    public float Space = 2f;

    public LayerMask PointMask;
    public GameObject Effect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentPoint = CLosestPoint();
            CheckPoint();
            StartMovment();
        }
     
    }

    
   
    void StartMovment()
    {
     
        GameObject E = PhotonNetwork.Instantiate(Effect.name, CurrentPoint, Quaternion.identity, 0);

        //GameObject E = Instantiate(Effect, FixedPoints[FixedPoints.Count - 1], Quaternion.identity) as GameObject;
        E.GetComponent<move>().target = this.transform;
    }
    void CheckPoint()
    {

        while (CurrentPoint == Vector3.zero )
        {
            CurrentPoint = CLosestPoint();
            Debug.Log("CheckPoint");

        }
    }
    public Vector3 CLosestPoint()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider C in colliders)
        {
            if (C.gameObject.CompareTag("Water"))
            {
                Debug.Log("Found Water : " + C.gameObject.name);
                Vector3 closest;
                float distance3 = Mathf.Infinity;
                Vector3 position = transform.position;
                for (int i = 0; i < MaxPoint; i++)
                {

                    Vector2 Rend = UnityEngine.Random.insideUnitCircle * pointRange;
                    Vector3 OffSet = new Vector3(Rend.x, C.transform.position.y, Rend.y);
                    Vector3 P = C.ClosestPointOnBounds(transform.position + OffSet);
                    if (!Points.Contains(P))
                        Points.Add(P);
                }
                foreach (var Point in Points)
                {     
                    var diff = (Point - position);
                    var curDistance = diff.sqrMagnitude;
                    Vector3 targetDir = Point - position;
                    float angle = Vector3.Angle(targetDir, transform.forward);
                    if (curDistance < distance3)
                    {
                        closest = Point;
                        return closest;
                    }
                }
            }
        }
        return Vector3.zero;

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        //   Vector3 Point = CLosestPoint();
        if (CurrentPoint != Vector3.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, CurrentPoint);
        }
        else
        {
            Debug.Log("No Points Found");
        }
        

    }

 

}

