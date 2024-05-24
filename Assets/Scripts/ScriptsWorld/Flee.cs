using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float fleeDistance = 2.5f;

    private void Update()
    {
        Fleee();
    }
    private void Fleee()
    {
        //calcular direction
        Vector3 fleeVector = transform.position - target.position;

        if (fleeVector.magnitude < fleeDistance)
        {
            fleeVector = fleeVector.normalized * fleeDistance;
            Vector3 fleePosition = target.position + fleeVector;

            transform.position = fleePosition;
        }
    }
}
