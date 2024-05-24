using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad;


    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);

    }

}
