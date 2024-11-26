using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 10f;
    public float rayDistance = 1f;

    public bool[] direccionesObstruidas;

    private int direccionMovActual = 0;
    void Start()
    {
        detectaColisiones();
    }

    // Update is called once per frame
    void Update()
    {
        //permite detectar como iuun colider
        detectaColisiones();
        //solo para visualizar en vista de escena
        
        //transform.Translate(transform.right * speed * Time.deltaTime);
    }

    public void detectaColisiones()
    {
        
        Debug.Log("Detectando colisiones");
        //disparamnos arriba
        
        Vector3 direccion;



        direccion = transform.up;
        

        int contador = 4;


        for (int i = 0; i < direccionesObstruidas.Length;i++)
        {


            switch (i)
            {
                case 0:
                    direccion = transform.up;
                    break;
                case 1:
                    direccion = -transform.up;
                    break;
                case 2:
                    direccion = -transform.right;
                    break;
                default:

                    direccion = transform.right;
                    break;

            }
           


            RaycastHit2D hit = Physics2D.Raycast(transform.position, direccion, rayDistance);

            if (hit.collider != null)
            {
                Debug.Log(direccion);
                Debug.DrawRay(transform.position, direccion * rayDistance, Color.red);
                Debug.Log(hit.collider.tag);
                //speed = -speed;
                direccionesObstruidas[i] = true;
            }

            else
            {
                direccionesObstruidas[i] = false;
                Debug.DrawRay(transform.position, direccion * rayDistance, Color.green);
            }

        }


        

        

    }

}
