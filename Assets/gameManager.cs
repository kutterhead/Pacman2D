using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.SceneManagement;
public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int pills;
    public GameObject[] pillsPantalla;
    public GameObject[] fantasmasPantalla;

    public TMPro.TextMeshProUGUI textoPills;
    public TMPro.TextMeshProUGUI textoVidas;

    public int vidas = 3;

    public Transform player;

    Vector3 InitialPosition;

    public GameObject panelGameOver;
    void Start()
    {
        InitialPosition = player.transform.position;
        vidas = 2;
        //este array solo recopila las referencias
        pillsPantalla = GameObject.FindGameObjectsWithTag("Pill");
        fantasmasPantalla = GameObject.FindGameObjectsWithTag("Enemy");

        pills = pillsPantalla.Length;
        Debug.Log("Pills pantalla:" + pills);
        panelGameOver.SetActive(false);
        textoVidas.text = (vidas + 1).ToString();
        textoPills.text = 0.ToString();
    }

    // Update is called once per frame
    public void restaVida()
    {
        Debug.Log("Pierde vida");
        vidas--;
        textoVidas.text = (vidas + 1).ToString();
        


        if (vidas<0)
        {
            Debug.Log("Game over");
            //
            player.gameObject.GetComponent<pacmanController>().enabled = false;
            player.gameObject.GetComponent<CircleCollider2D>().enabled = false;

            for (int i = 0; i<fantasmasPantalla.Length;i++)
            {
                fantasmasPantalla[i].GetComponent<enemigo>().enabled = false;
                fantasmasPantalla[i].GetComponent<enemigo>().StopAllCoroutines();

            }

            panelGameOver.SetActive(true);

        }
        else
        {
            player.position = InitialPosition;

        }

    }

    public void pillConseguida()
    {
        pills--;
        textoPills.text = (pillsPantalla.Length - pills).ToString();
        if (pills<1)
        {
            Debug.Log("Siguiente pantalla.");
            for (int i = 0; i < fantasmasPantalla.Length; i++)
            {
                fantasmasPantalla[i].GetComponent<enemigo>().enabled = false;
                fantasmasPantalla[i].GetComponent<enemigo>().StopAllCoroutines();

            }
            player.gameObject.GetComponent<pacmanController>().enabled = false;
            player.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //llamaremos función de carga

            Invoke("loadScene",1f);
        }
        //Debug.Log("Pill conseguida: " + pills);
    }

    public void loadScene()
    {

        SceneManager.LoadScene("2");

    }

    public void modoCazafantasma()
    {

        Debug.Log("Modo cazafantasmas activado");

    }



}
