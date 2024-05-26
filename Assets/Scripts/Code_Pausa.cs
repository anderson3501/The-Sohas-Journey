using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Code_Pausa : MonoBehaviour
{

    public GameObject ObjetoMenuPausa;
    public bool Pausa;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Pausa == false)
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;

                Time.timeScale = 0;
                Cursor.visible = true;
            }
            else if(Pausa == true)
            {
                Resumir();
            }
        }
    }

    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;
        // Cursor.visible = false;   
    }

    public void MenuPrincipal(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);
    }

    public void SalieJuego(){
        Application.Quit();
    }
}
