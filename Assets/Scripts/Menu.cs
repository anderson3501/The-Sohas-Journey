using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void start()
    {
        PauseGame();
    }
   public void EscenaJuego()
    {
        SceneManager.LoadScene("BasementMain");
        Time.timeScale = 1;
    }

    public void salir()
    {
        Application.Quit();
    }

    void PauseGame()
    {
        // Pausa el juego estableciendo la escala de tiempo en 0
        Time.timeScale = 0f;
    }
}
