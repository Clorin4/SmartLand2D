using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyLevel
{
    Easy,
    Normal,
    Hard,
    Insane,
    Demon,
    SuperDemon
}

[CreateAssetMenu(fileName = "PhraseData", menuName = "Custom/Phrase Data")]
public class PhraseData : ScriptableObject
{
    public DifficultyLevel difficultyLevel;
    public float phraseTime;
    public string[] phrases;
}


public class PhraseManager : MonoBehaviour
{
    public PhraseData[] phraseData;

    public PhraseData GetPhraseDataByDifficulty(DifficultyLevel difficulty)
    {
        foreach (PhraseData data in phraseData)
        {
            if (data.difficultyLevel == difficulty)
            {
                return data;
            }
        }

        return null;
    }
}
