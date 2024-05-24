using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    
    public CharacterHealt characterHealt;
    public int damageAmount = 1;

    //public PlayerController movimientojugador;
    //[SerializeField] private float tiempoPerdidaControl;
    

    public Vector2 ola; 

    private void Start()
    {
        //movimientojugador = GetComponent<PlayerController>();
    }
    //<>

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            
            characterHealt.TakeDamage(damageAmount, ola);
        }
    }


}
