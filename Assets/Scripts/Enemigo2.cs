using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2 : MonoBehaviour
{
    // Start is called before the first frame update
    public gameManager manager;
    public float speed = 10f;


    public float factorDistancePlayer;
    public float rayDistance = 1f;

    public bool[] direccionesObstruidas;
    [SerializeField]
    byte numDirLibres = 0;



    private int direccionMovActual = 0;//indice para saber la direccion actual
    private Vector3 vectorMovActual;

    int indiceNuevaDireccion = 0;

    Vector3[] direccionesMovimiento;

    int direccionActual;


    IEnumerator movimiento;
    IEnumerator deteccion;

    bool cambiandoCruce = false;


    private Animator anim;
    public bool isScapping = false;//usado para poner al fantasma en modo escape
   

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>();
        manager.eventoModoEscape.AddListener(modoEscape);
        anim = GetComponent<Animator>();
        direccionActual = escogerDireccionLibre();
        deteccion = detecta();

        //inicializacion del vector
        System.Array.Resize(ref direccionesMovimiento, 4);
        for (int i = 0; i < direccionesMovimiento.Length; i++)
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

        movimiento = mueveEnemigo();


        StartCoroutine(movimiento);

        StartCoroutine(deteccion);//escanea todos los frames
        //StartCoroutine(escanea3Libres());//escanea solo 3 libres cada x tiempo
    }

    public void modoEscape()
    {
        isScapping = true;
    }




    //esta corrutina cambia la dirección posible
    IEnumerator escanea3Libres()
    {
        while (true)

        {

            //implementar prioridad de player ???


            //si las direcciones obstruíidas mayor o igual a 3
            if (numDirLibres >= 3)
            {
                cambiandoCruce = true;
                Debug.Log("Cruce indice actual: " + direccionActual + "vector:" + vectorMovActual);
                //tiempo de espera antes de reaccionar
                yield return new WaitForSeconds(0.35f);//tiempo de reaccion
                detectaColisiones();
                int nuevoIndice = escogerDireccionLibre();
                vectorMovActual = devuelveDireccionVector3(nuevoIndice);
                ajustaAnimacion(nuevoIndice);
                indiceNuevaDireccion = nuevoIndice;
                direccionActual = indiceNuevaDireccion;
                direccionMovActual = direccionActual;
                cambiandoCruce = false;
                yield return new WaitForSeconds(0.4f);//tiempo de ejecucion de nueva dirección
                Debug.Log("Cruce indice reasignado!" + nuevoIndice + "vector" + vectorMovActual);

            }

            yield return null;
        }
    }


    IEnumerator mueveEnemigo()
    {

        // Debug.Log("iniciado loop corrutina de movimiento");
        while (true)
        {

            transform.Translate(vectorMovActual * speed * Time.deltaTime);
            //Debug.Log("Direccion movimiento: " + vectorMovActual);
            yield return null;

        }

    }


    IEnumerator detecta()
    {

        Debug.Log("iniciado loop corrutina de movimiento");
        while (true)
        {
            if (!cambiandoCruce)
            {
                detectaColisiones();
                if (direccionesObstruidas[direccionActual] == false)
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
                ajustaAnimacion(direccionActual);
            }

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

        if (numeroObstruidas > 2)
        {

            Debug.Log("hacer cambio de direccion");

        }

    }

    //escoger direccion libre random
    int escogerDireccionLibre()//la usamos para que nos devuelva una direccion disponible random
    {
        indiceNuevaDireccion = Random.Range(0, direccionesObstruidas.Length);

        for (int j = 0; j < direccionesObstruidas.Length; j++)
        {


            if (direccionesObstruidas[j] == false && indiceNuevaDireccion == j)
            {
                //este caso es bueno Bingo es random y es libre

                indiceNuevaDireccion = j;

            }
            indiceNuevaDireccion++;
            if (indiceNuevaDireccion >= direccionesObstruidas.Length)
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
                direccion = new Vector3(0, 1, 0);
                anim.SetInteger("action", 3);
                break;
            case 1:
                direccion = new Vector3(0, -1, 0);
                anim.SetInteger("action", 2);
                break;
            case 2:
                direccion = new Vector3(-1, 0, 0);

                anim.SetInteger("action", 0);
                break;
            default:

                direccion = new Vector3(1, 0, 0);
                anim.SetInteger("action", 1);
                break;
        }
        return direccion;
    }

    void ajustaAnimacion(int indice)
    {

        if (isScapping)
        {
            anim.SetInteger("action", 4);
            return;
        }

        Vector3 direccion;
        switch (indice)
        {
            case 0:

                anim.SetInteger("action", 3);
                break;
            case 1:

                anim.SetInteger("action", 2);
                break;
            case 2:

                anim.SetInteger("action", 0);
                break;
            default:

                anim.SetInteger("action", 1);
                break;

        }
    }

    #region función ded detección

    public void detectaColisiones()//almacena en el array de colisiones
    {

        Vector3 direccion;
        numDirLibres = 0;//sólo para contar huecos libres

        for (int i = 0; i < direccionesObstruidas.Length; i++)//dispara en las 4 direcciones
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

            if ((hit.collider != null && hit.collider.CompareTag("Escenario")))
            {
                //Debug.Log(direccion);
                Debug.DrawRay(transform.position, direccion * rayDistance, Color.red);

                direccionesObstruidas[i] = true;
            }

            else
            {
                direccionesObstruidas[i] = false;
                numDirLibres++;
                Debug.DrawRay(transform.position, direccion * rayDistance, Color.green);
            }

            //se usa para detectar player
            hit = Physics2D.Raycast(transform.position, direccion, rayDistance * factorDistancePlayer);
            Debug.DrawRay(transform.position, direccion * rayDistance * factorDistancePlayer, Color.yellow);
            if ((hit.collider != null && hit.collider.CompareTag("Player")))
            {

                if (isScapping)
                {

                Debug.Log("detectado player en modo escape");
                Debug.DrawRay(transform.position, direccion * rayDistance, Color.red);

                direccionesObstruidas[i] = true;
                }
                else
                {
                    //aquí no se corresponde
                    Debug.Log("detectado player en modo normal");
                    direccionesObstruidas = new bool[] { true, true, true, true };

                   /* for (int j = 0; j < direccionesObstruidas.Length; j++)
                    {
                        direccionesObstruidas[j] = true;
                    }
                   */

                    //Debug.LogError("detectado player en modo normal");
                    Debug.Break();
                    direccionesObstruidas[i] = false;

                }
            }


        }//------------------------------final del for

    }

    #endregion


    //manager llama a esta funcion de todos los fanttatsmas cuando se coge superpill
    public void pasaModoEscape()
    {
        isScapping = true;
    }
    public void desactivaEscape()
    {


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isScapping)
            {
                Destroy(gameObject);
                manager.sumaPuntos(100);
            }
            else
            {
                print("Colisión con Enemy");
                //llamada a manager para restar vida
                manager.restaVida();
            }

        }
    }
}
