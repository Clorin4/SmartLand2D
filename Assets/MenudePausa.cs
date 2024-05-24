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
    public Toggle muteToggle; // Add a reference to the Toggle UI component
    public string selectedButton;

    void Start()
    {
        menuConfirmar.SetActive(false);
        menuopciones.SetActive(false);
        menuPausa.SetActive(false);

        sliderValue = PlayerPrefs.GetFloat("volumenAudio", 1f);
        slider.value = sliderValue;
        AudioListener.volume = sliderValue;

        // Set the mute toggle state based on the initial volume
        muteToggle.isOn = (AudioListener.volume == 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausa();
        }
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;

        // If the slider value is greater than 0, uncheck the mute toggle
        if (sliderValue > 0)
        {
            muteToggle.isOn = false;
        }
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

        PlayerPrefs.GetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;

        if (muted)
        {
            PlayerPrefs.SetFloat("volumenAudio", AudioListener.volume);
            AudioListener.volume = 0;
        }

        else if (sliderValue > 0)
        {
            muteToggle.isOn = false;
            AudioListener.volume = PlayerPrefs.GetFloat("volumenAudio", 1f);
            slider.value = AudioListener.volume;
        }

        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volumenAudio", 1f);
            slider.value = AudioListener.volume;
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

        switch (selectedButton) // decenas indican juego, unidades tipo de juego
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

            case "mainmenu":
                SceneManager.LoadScene(0);
                Time.timeScale = 1f;
                break;

            case "exitgame":
                ExitGame();
                Time.timeScale = 1f;
                break;
        }
    }
    public void ExitGame()
    {

        Application.Quit();

        // Esto solo se ejecutará en el Editor de Unity para simular la salida del juego
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}