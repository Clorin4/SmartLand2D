using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frutas : MonoBehaviour
{
    public string fruitType;
    private FruitSpawner fruitSpawner;

    private void Start()
    {
        fruitSpawner = FindObjectOfType<FruitSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            // Obtener el tag del jugador
            string playerTag = collision.gameObject.tag;

            // Incrementar la cantidad de frutas recolectadas según el tipo de fruta y el jugador
            int numFruits = PlayerPrefs.GetInt(fruitType + playerTag, 0);
            numFruits++;
            PlayerPrefs.SetInt(fruitType + playerTag, numFruits);

            Debug.Log(playerTag + " " +fruitType + ": " + numFruits);

            // Llamar al método FruitCollected del FruitSpawner
            if (fruitSpawner != null)
            {
                fruitSpawner.FruitCollected(fruitType);
            }

            // Destruir la fruta
            Destroy(gameObject);
        }
    }
}
