using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GussTheWord : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer sprite3Renderer;
    public SpriteRenderer sprite2Renderer;
    public SpriteRenderer sprite1Renderer;
    public SpriteRenderer spriteAdelanteRenderer;
    public SpriteRenderer figuras;
    public SpriteRenderer orca;
    public SpriteRenderer orca1;

    void Start()
    {
        TurnOffVariables();
        SaberDificultad();

        StartCoroutine(Countdown());
    }

    public void TurnOffVariables()
    {
        sprite1Renderer.gameObject.SetActive(false);
        sprite2Renderer.gameObject.SetActive(false);
        sprite3Renderer.gameObject.SetActive(false);
        spriteAdelanteRenderer.gameObject.SetActive(false);
        figuras.gameObject.SetActive(false);
        orca.gameObject.SetActive(false);
        orca1.gameObject.SetActive(false);
    }

    public void SaberDificultad()
    {
        string selectedDifficulty = PlayerPrefs.GetString("SelectedDifficulty");
        switch (selectedDifficulty)
        {
            case "dif1":
                //questionManager.questions = questionManager.Easyquestions.ConvertAll(q => (Question)q);
                break;

            case "dif2":
                //questionManager.questions = questionManager.Normalquestions.ConvertAll(q => (Question)q);
                break;

            case "dif3":
                //questionManager.questions = questionManager.Hardquestions.ConvertAll(q => (Question)q);
                break;

            case "dif4":
                //questionManager.questions = questionManager.Insanequestions.ConvertAll(q => (Question)q);
                break;

            case "dif5":
                //questionManager.questions = questionManager.Demonquestions.ConvertAll(q => (Question)q);
                break;

            case "dif6":
                //questionManager.questions = questionManager.SuperDemonquestions.ConvertAll(q => (Question)q);
                break;

            default:
                // Manejar una dificultad inesperada
                break;
        }
    }


    IEnumerator Countdown()
    {
        //yield return new WaitForSeconds(.3f);

        orca.gameObject.SetActive(true);
        orca1.gameObject.SetActive(true);

        sprite3Renderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(sprite3Renderer, Vector3.zero, Vector3.one * 1f, .9f); // Escalar de 0 a un tamaño específico
        sprite3Renderer.gameObject.SetActive(false);

        yield return new WaitForSeconds(.1f);

        sprite2Renderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(sprite2Renderer, Vector3.zero, Vector3.one * 1f, .9f); // Escalar de 0 a un tamaño específico
        sprite2Renderer.gameObject.SetActive(false);

        yield return new WaitForSeconds(.1f);

        sprite1Renderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(sprite1Renderer, Vector3.zero, Vector3.one * 1f, .9f); // Escalar de 0 a un tamaño específico
        sprite1Renderer.gameObject.SetActive(false);

        yield return new WaitForSeconds(.1f);

        spriteAdelanteRenderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(spriteAdelanteRenderer, Vector3.zero, Vector3.one * .7f, .9f); // Escalar de 0 a un tamaño específico
        spriteAdelanteRenderer.gameObject.SetActive(false);

        // Muestra el nuevo sprite después de la cuenta regresiva
        figuras.gameObject.SetActive(true);
        

        //StartGame();
    }

    IEnumerator ScaleSpriteTo(SpriteRenderer spriteRenderer, Vector3 startScale, Vector3 endScale, float duration)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            spriteRenderer.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        spriteRenderer.transform.localScale = endScale;
    }

}