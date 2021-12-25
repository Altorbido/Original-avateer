using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public Transform target;
    public float MoveSpeed = 0.2f;



    // Update is called once per frame
    void Update()
    {
       

        float Distance = Vector3.Distance(transform.position, target.position);

        transform.position += (target.position - transform.position).normalized * MoveSpeed * Time.deltaTime;
        /*
        if (Distance <= 0.5)
        {
            if (CP > 0)
            {
                CP--;
            }
            else
            {
                CP = 0;
            }
            CurrentPoint = path[CP];
        }*/
        RaycastHit R;
        if (Physics.Raycast(transform.position, (target.position - transform.position).normalized, out R, 10))
        {
            transform.position += transform.up.normalized * MoveSpeed * Time.deltaTime;
        }
    }
}
