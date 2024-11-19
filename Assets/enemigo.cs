using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 10f;
    public float rayDistance = 1f;

    public bool[] direccionesObstruidas;

    void Start()
    {
        detectaColisiones();
    }

    // Update is called once per frame
    void Update()
    {
        //permite detectar como iuun colider
        //detectaColisiones();
        //solo para visualizar en vista de escena
        
       // transform.Translate(transform.right * speed * Time.deltaTime);
    }

    public void detectaColisiones()
    {
        
        Debug.Log("Detectando colisiones");
        //disparamnos arriba
        
        Vector3 direccion;

        //direccion = transform.right;
        // Debug.DrawRay(transform.position, direccion * rayDistance, Color.green, .1f); Debug.DrawRay(transform.position, transform.right * rayDistance, Color.green, .1f);
        //direccion = transform.up;
        //Debug.DrawRay(transform.position, direccion * rayDistance, Color.green, .1f); Debug.DrawRay(transform.position, transform.right * rayDistance, Color.green, .1f);

        direccion = transform.up;
        Debug.DrawRay(transform.position, direccion * rayDistance, Color.green, .1f); Debug.DrawRay(transform.position, transform.right * rayDistance, Color.green, .1f);

        int contador = 4;

        //while (contador>0)
        //{

        //    print("Iteración: " +contador);

        //    contador--;
        //    direccion = transform.up;
        //    Debug.DrawRay(transform.position, direccion * rayDistance, Color.green, .1f); Debug.DrawRay(transform.position, transform.right * rayDistance, Color.green, .1f);

        //}



        for (int i = 0; i < 4; i++)
        {


            print("Iteración: " + i);

           
            direccion = transform.up;
            Debug.DrawRay(transform.position, direccion * rayDistance, Color.green, .1f); Debug.DrawRay(transform.position, transform.right * rayDistance, Color.green, .1f);



        }




            return;


        for (int i = 0; i < direccionesObstruidas.Length;i++)
        {

            
            //switch (i)
            //{
            //    case 0:
            //        direccion = transform.up;
            //        break;
            //    case 1:
            //        direccion = -transform.up;
            //        break;
            //    case 2:
            //        direccion = -transform.right;
            //        break;
            //    default:

            //        direccion = transform.right;
            //        break;

            //}



            direccion = transform.right;


            Debug.DrawRay(transform.position, direccion * rayDistance, Color.green, .1f); Debug.DrawRay(transform.position, transform.right * rayDistance, Color.green, .1f);
            direccion = transform.right;

            RaycastHit2D hit = Physics2D.Raycast(direccion, transform.up, rayDistance);

            if (hit.collider != null)
            {
                Debug.Log(direccion);
                Debug.DrawRay(transform.position, direccion * rayDistance, Color.red, .1f);
                Debug.Log(hit.collider.tag);
                speed = -speed;
                direccionesObstruidas[i] = true;
            }

            else
            {
                direccionesObstruidas[i] = false;
                Debug.DrawRay(transform.position, direccion * rayDistance, Color.green, .1f);
            }

        }


        

        

    }

}
