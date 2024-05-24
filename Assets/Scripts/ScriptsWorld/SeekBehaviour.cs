using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    public float seekDistance = 10f;


    private void Update()
    {
        //SeekForever();
        Seek();
    }

    void SeekForever()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        Vector3 seekVelocity = direction * speed;
        transform.position += seekVelocity * Time.deltaTime;
    }

    void Seek()
    {
        Vector3 seekVector = target.position - transform.position;
        float distanceToTarget = seekVector.magnitude;

        if (distanceToTarget < seekDistance)
        {
            Vector3 seekPosition = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
            transform.position = seekPosition;
        }
    }

}
