using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador.

    private void Update()
    {
        if (player != null)
        {
            // Calcula la dirección hacia el jugador en el plano horizontal.
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0;

            // Cambia la escala en el eje X para voltear al NPC según la dirección del jugador.
            if (directionToPlayer.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Mirando a la derecha
            }
            else if (directionToPlayer.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Mirando a la izquierda (escala -1 en X invierte la dirección)
            }
        }
    }
}
//
