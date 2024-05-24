using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioJugadorr : MonoBehaviour
{
    public Transform[] spawnPoints; // Un arreglo para almacenar los puntos de spawn
    GameObject jugador2;


    private void Start()
    {
        int indexJugador1 = PlayerPrefs.GetInt("Jugador1Index");
        int indexJugador2 = PlayerPrefs.GetInt("Jugador2Index");

        if (indexJugador1 >= 0 && indexJugador1 < GameManager.Instance.personajes.Count &&
            indexJugador2 >= 0 && indexJugador2 < GameManager.Instance.personajes.Count &&
            spawnPoints.Length >= 2)
        {
            GameObject jugador1 = Instantiate(GameManager.Instance.personajes[indexJugador1].personajeJugable, spawnPoints[0].position, Quaternion.identity);
            PlayerAnimatorController player1Controller = jugador1.GetComponent<PlayerAnimatorController>();
            if (player1Controller != null)
            {
                player1Controller.SetPlayerIndexAndTag(indexJugador1, "Player1");
            }

            if(PlayerPrefs.GetInt("GameMode") == 2)
            {
                jugador2 = Instantiate(GameManager.Instance.personajes[indexJugador2].personajeJugable, spawnPoints[1].position, Quaternion.identity);
                PlayerAnimatorController player2Controller = jugador2.GetComponent<PlayerAnimatorController>();
                if (player2Controller != null)
                {
                    player2Controller.SetPlayerIndexAndTag(indexJugador2, "Player2");
                }
            }
            

            int gameChoice = PlayerPrefs.GetInt("gameIndex");
            Debug.Log(gameChoice);
            // Cambiar la orientación del personaje del jugador 2 hacia la izquierda
            if (jugador2 != null && gameChoice == 1)
            {
                // Obtener el componente de escala (scale) del Transform
                Vector3 scale = jugador2.transform.localScale;
                // Voltear el personaje hacia la izquierda
                scale.x = -Mathf.Abs(scale.x);
                // Aplicar la nueva escala al Transform
                jugador2.transform.localScale = scale;
            }
            else if (jugador2 != null && gameChoice == 2)
            {
                float escala = 0.5f;

                Transform transformJugador1 = jugador1.transform;
                transformJugador1.localScale = new Vector2(escala, escala);

                Transform transformJugador2 = jugador2.transform;
                transformJugador2.localScale = new Vector2(escala, escala);

            }
            else if(jugador2 != null && gameChoice == 3){

                //POSICIONES PARA JUGADORES EN EL DE RECOGER DIAMANTES
                float escala = 0.2f;

                Transform transformJugador1 = jugador1.transform;
                transformJugador1.localScale = new Vector2(escala, escala);

                Transform transformJugador2 = jugador2.transform;
                transformJugador2.localScale = new Vector2(escala, escala);

                Vector3 scale = jugador2.transform.localScale;
                scale.x = -Mathf.Abs(scale.x);
                jugador2.transform.localScale = scale;
            }
            else  if (jugador2 != null && gameChoice == 4)
            {
                float escala = 0.65f;

                Transform transformJugador1 = jugador1.transform;
                transformJugador1.localScale = new Vector2(escala, escala);

                Transform transformJugador2 = jugador2.transform;
                transformJugador2.localScale = new Vector2(escala, escala);

                // Obtener el componente de escala (scale) del Transform
                Vector3 scale = jugador2.transform.localScale;
                // Voltear el personaje hacia la izquierda
                scale.x = -Mathf.Abs(scale.x);
                // Aplicar la nueva escala al Transform
                jugador2.transform.localScale = scale;
            }

        }
    }

}
