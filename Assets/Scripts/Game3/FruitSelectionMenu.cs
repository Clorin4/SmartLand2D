using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FruitSelectionMenu : MonoBehaviour
{
    public Button[] fruitButtons; // Botones de frutas (en orden: manzanas, plátanos, naranjas)
    public TextMeshProUGUI[] fruitQuantities; // Textos para las cantidades de frutas
    private int selectedButtonIndex = 0;
    private string[] fruitNames = { "Manzanas", "Platanos", "Naranjas", "Cancelar" }; // Nombres de las frutas en orden
    private int playerIndex; // Índice del jugador actual (1 o 2)

    private void Start()
    {
        HideMenu();
    }

    public void ShowMenu(bool isPlayer1)
    {
        playerIndex = isPlayer1 ? 1 : 2; // Establecer el índice del jugador
        Debug.Log(playerIndex);
        gameObject.SetActive(true);
        UpdateFruitQuantities(isPlayer1);
        selectedButtonIndex = 0;
        MoveSelection(0);
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }

    public void MoveSelection(int direction)
    {
        selectedButtonIndex = (selectedButtonIndex + direction) % fruitButtons.Length;
        if (selectedButtonIndex < 0)
        {
            selectedButtonIndex += fruitButtons.Length;
        }

        for (int i = 0; i < fruitButtons.Length; i++)
        {
            fruitButtons[i].image.color = (i == selectedButtonIndex) ? Color.yellow : Color.white;
        }
    }

    public void SelectFruit()
    {
        string fruitName = fruitNames[selectedButtonIndex];
        Debug.Log("Fruit selected: " + fruitName);

        // Si se selecciona "Cancelar", oculta el menú
        if (selectedButtonIndex == fruitButtons.Length - 1)
        {
            HideMenu();
            return;
        }

        // Reducir en 1 la cantidad de frutas seleccionadas del jugador activo
        PlayerPrefs.SetInt(fruitName + "Player" + playerIndex, Mathf.Max(0, PlayerPrefs.GetInt(fruitName + "Player" + playerIndex) - 1));

        // Actualizar las cantidades de frutas en el menú
        UpdateFruitQuantities(playerIndex == 1);
    }

    public void UpdateFruitQuantities(bool isPlayer1)
    {
        for (int i = 0; i < fruitQuantities.Length; i++)
        {
            int fruitQuantity = PlayerPrefs.GetInt(fruitNames[i] + "Player" + (isPlayer1 ? 1 : 2));
            fruitQuantities[i].text = fruitQuantity.ToString();
        }
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            // Controles del jugador 1 (teclas A, D y S)
            if (playerIndex == 1) // Jugador 1 activo
            {
                //Debug.Log("PLAYER 111111111");
                if (Input.GetKeyDown(KeyCode.A))
                {
                    MoveSelection(-1); // Mueve la selección a la izquierda
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    MoveSelection(1); // Mueve la selección a la derecha
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    //PlayerPrefs.SetInt("CanMovePlayer1", 1);
                    SelectFruit(); // Selecciona la fruta
                }
            }
            // Controles del jugador 2 (flechas)
            else // Jugador 2 activo
            {
                //Debug.Log("PLAYER 222222222");
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveSelection(-1); // Mueve la selección a la izquierda
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveSelection(1); // Mueve la selección a la derecha
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    //PlayerPrefs.SetInt("CanMovePlayer2", 1);
                    SelectFruit(); // Selecciona la fruta
                }
            }
        }
    }

}
