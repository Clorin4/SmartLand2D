using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText; // El texto de la pregunta
    public string[] options; // Las opciones de respuesta
    public int correctAnswerIndex; // El índice de la respuesta correcta
}

[System.Serializable]
public class Materia
{
    public string name; // Nombre de la materia
    public List<Question> preguntas; // Lista de preguntas de la materia
}

[System.Serializable]
public class DifficultyLeveln //n
{
    public string name; // Nombre del grado de dificultad (por ejemplo: "Primer grado")
    public List<Materia> materias; // Lista de materias para este grado
}

public class NewBehaviourScript : MonoBehaviour
{
    public List<DifficultyLeveln> difficultyLevels; // Lista de grados de dificultad
}
