using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int pills;
    public GameObject[] pillsPantalla;

    void Start()
    {
        pillsPantalla = GameObject.FindGameObjectsWithTag("Pill");

       
        pills = pillsPantalla.Length;
        Debug.Log("Pills pantalla:" + pills);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pillConseguida()
    {
        pills++;
        Debug.Log("Pill conseguida: " + pills);
    }


}
