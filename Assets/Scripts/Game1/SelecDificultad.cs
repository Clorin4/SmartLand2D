using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecDificultad : MonoBehaviour
{
    private string selectedDifficulty;

    private void Start()
    {
        selectedDifficulty = PlayerPrefs.GetString("SelectedDifficulty");
        // Si se utiliza un gestor de juego en lugar de PlayerPrefs, accede a la dificultad desde all�.

        // Ahora puedes usar la variable selectedDifficulty para ajustar el juego seg�n la dificultad elegida.
        Debug.Log("Dificultad seleccionada: " + selectedDifficulty);
    }
}
