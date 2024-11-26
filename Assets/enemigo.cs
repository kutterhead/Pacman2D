using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 10f;
    public float rayDistance = 1f;

    public bool[] direccionesObstruidas;

    private int direccionMovActual = 0;//indice para saber la direccion actual
    private Vector3 vectorMovActual;

    int indiceNuevaDireccion = 0;

    Vector3[] direccionesMovimiento;

    int direccionActual;
    void Start()
    {
        direccionActual = escogerDireccionLibre();


        //inicializacion del vector
        System.Array.Resize(ref direccionesMovimiento,4);
        for (int i = 0; i< direccionesMovimiento.Length;i++)
        {
            switch (i)
            {
                case 0:
                    direccionesMovimiento[i] = transform.up;
                    break;
                case 1:
                    direccionesMovimiento[i] = -transform.up;
                    break;
                case 2:
                    direccionesMovimiento[i] = -transform.right;
                    break;
                default:

                    direccionesMovimiento[i] = transform.right;
                    break;

            }

        }
        vectorMovActual = devuelveDireccionVector3(direccionActual);
        StartCoroutine(mueveEnemigo());
        StartCoroutine(detecta());

    }

    // Update is called once per frame
    void Update()
    {
        //permite detectar como iuun colider
        
        //solo para visualizar en vista de escena     
       
    }

    IEnumerator mueveEnemigo()
    {

        Debug.Log("iniciado loop corrutina de movimiento");
        while (true)
        {
            
            
            transform.Translate(vectorMovActual * speed * Time.deltaTime);
            Debug.Log("Direccion movimiento: " + vectorMovActual);
            yield return null;


        }
       
    }
    IEnumerator detecta()
    {

        Debug.Log("iniciado loop corrutina de movimiento");
        while (true)
        {
            detectaColisiones();
            if (direccionesObstruidas[direccionActual]==false)
            {

                direccionMovActual = direccionActual;
            }
            else
            {
                indiceNuevaDireccion = escogerDireccionLibre();
                direccionActual = indiceNuevaDireccion;
                direccionMovActual = direccionActual;
            }

            vectorMovActual = direccionesMovimiento[direccionMovActual];


            yield return null;


        }

    }

    public void detectMomentoCambio()
    {

        int numeroObstruidas = 0;
        for (int i = 0; i < direccionesMovimiento.Length; i++)
        {
            if (direccionesObstruidas[i] == false)
            {
                numeroObstruidas++;

            }

        }

        if (numeroObstruidas>2)
        {

            Debug.Log("hacer cambio de direccion");

        }


    }

    //escoger direccion libre random
    int  escogerDireccionLibre()//la usamos para que nos devuelva una direccion disponible random
    {
        indiceNuevaDireccion = Random.Range(0, direccionesObstruidas.Length);
        
        for (int j = 0; j < direccionesObstruidas.Length; j++)
        {


            if (direccionesObstruidas[j] == false && indiceNuevaDireccion == j)
            {
                //este caso es bueno Bingo es random y es libre


                indiceNuevaDireccion = j;
                Debug.Log("Direccion Libre: " + j);
                //return j;

            }
            indiceNuevaDireccion++;
            if (indiceNuevaDireccion>= direccionesObstruidas.Length)
            {
                indiceNuevaDireccion = 0;
            }



        }
   
        return indiceNuevaDireccion;//por defecto


    }

    Vector3 devuelveDireccionVector3(int indice)
    {

        Vector3 direccion;
        switch (indice)
        {
            case 0:
                direccion = new Vector3(0,1,0);
                break;
            case 1:
                direccion = -new Vector3(0, 1, 0);
                break;
            case 2:
                direccion = -new Vector3(1, 0, 0);
                break;
            default:

                direccion = new Vector3(1, 0, 0); 
                break;


        }
                return direccion;
    }



public void detectaColisiones()//almacena en el array de colisiones
    {
        
        Debug.Log("Detectando colisiones");
        //disparamnos arriba
        
        Vector3 direccion;

        //direccion = transform.up;       
        //int contador = 4;

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
