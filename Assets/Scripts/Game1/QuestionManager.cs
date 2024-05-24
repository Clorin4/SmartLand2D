using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Questionn
{
    public string questionText; // El texto de la pregunta
    public string[] options; // Las opciones de respuesta
    public int correctAnswerIndex; // El índice de la respuesta correcta
}

[System.Serializable]
public class EasyQuestion : Question
{
    
}

[System.Serializable]
public class NormalQuestion : Question
{
    
}

[System.Serializable]
public class HardQuestion : Question 
{
    
}

[System.Serializable]
public class InsaneQuestion : Question 
{
    
}

[System.Serializable]
public class DemonQuestion : Question 
{
    
}

[System.Serializable]
public class SuperDemonQuestion : Question 
{
    
}

public class QuestionManager : MonoBehaviour
{
    public List<Question> questions;

    public List<EasyQuestion> Easyquestions;
    public List<NormalQuestion> Normalquestions;
    public List<HardQuestion> Hardquestions;
    public List<InsaneQuestion> Insanequestions;
    public List<DemonQuestion> Demonquestions;
    public List<SuperDemonQuestion> SuperDemonquestions;
}
