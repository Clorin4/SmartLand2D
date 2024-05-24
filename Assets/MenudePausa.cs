using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenudePausa : MonoBehaviour
{
    public GameObject botonPausa;
    public GameObject menuPausa;
    public GameObject menuopciones;
    public GameObject menuConfirmar;

    public Slider slider;
    public float sliderValue;
    public string selectedButton;
    
 

   void Start()
    {
        menuConfirmar.SetActive(false);
        menuopciones.SetActive(false);
        menuPausa.SetActive(false);
    }

    public void ChangeSlider(float valor)
    {
        
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
       
    }


    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        menuopciones.SetActive(false);
        menuConfirmar.SetActive(false);
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Opciones()
    {
        menuPausa.SetActive(false);
        menuopciones.SetActive(true);
    }

    public void RegresaralmenuPrincipal()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }


    public void MuteToggle(bool muted)
    {
        if (!muted)
        {
            
            AudioListener.volume = 0;
        }
        else
        {
            
            AudioListener.volume = 1;
        } 


    }
    public void MenuButton(string button)
    {
        menuConfirmar.SetActive(true);
        menuPausa.SetActive(false);
        selectedButton = button;
        
    }
    public void SelectedButton()
    {
        menuPausa.SetActive(false);
        menuConfirmar.SetActive(false);

        switch (selectedButton) // decenas indican juego , unidades tipo de juego
        {
            case "reiniciar11":
                SceneManager.LoadScene(3);
                Time.timeScale = 1f;
                break;

            case "reiniciar12":
                SceneManager.LoadScene(7);
                Time.timeScale = 1f;
                break;

            case "reiniciar21":
                SceneManager.LoadScene(4);
                Time.timeScale = 1f;
                break;

            case "reiniciar31":
                SceneManager.LoadScene(5);
                Time.timeScale = 1f;
                break;

            case "isla":
                SceneManager.LoadScene(1);
                Time.timeScale = 1f;
                break;

            case "personajes":
                SceneManager.LoadScene(2);
                Time.timeScale = 1f;
                break;
        }

            
    }

    
}
