using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAB : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    private Transform targetPoint;


    public Transform target;
    public float speed = 5f;

    public float seekDistance = 10f;

    


    private void Start()
    {
        targetPoint = pointA;
    }

    private void Update()
    {
        MoveToPoints();
        Seek();
    }

    void MoveToPoints()
    {
        if (transform.position == targetPoint.position)
        {
            //enemy A to b and viceversa
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
        }

        //mover enemy a point actual
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);
    }


    void Seek()
    {

        Vector3 seekVector = target.position - transform.position;
        float distanceToTarget = seekVector.magnitude;
        
        if (distanceToTarget < seekDistance)
        {
            Debug.DrawRay(transform.position, seekVector, Color.blue);
            Vector3 seekPosition = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
            transform.position = seekPosition;
        }
    }
}
