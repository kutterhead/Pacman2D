using UnityEngine;

public class Enemigo3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public gameManager manager;
    public float speed = 10f;
    public float rayDistance = 1f;
    public bool[] direccionesObstruidas;
    public Vector3 vectorMovActual;
    public int numDirLibres = 0;


    void Start()
    {
        vectorMovActual = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(vectorMovActual * speed * Time.deltaTime);
        detectaColisiones();

    }


    public void detectaColisiones()//almacena en el array de colisiones
    {

        Vector3 direccion;
        numDirLibres = 0;//sólo para contar huecos libres
        Vector3 puntoRay;
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



            if (i>=2){


                puntoRay = transform.position + new Vector3(0, 0.8f, 0);
                RaycastHit2D hit = Physics2D.Raycast(puntoRay, direccion, rayDistance);
               
                if ((hit.collider != null && hit.collider.CompareTag("Escenario")))
                {
                    //Debug.Log(direccion);

                    direccionesObstruidas[i] = true;
                    Debug.DrawRay(puntoRay, direccion * rayDistance, Color.red);
                }
                else
                {
                    Debug.DrawRay(puntoRay, direccion * rayDistance, Color.green);
                    puntoRay = transform.position + new Vector3(0, -0.8f, 0);
                    RaycastHit2D hit2 = Physics2D.Raycast(puntoRay, direccion, rayDistance);
                    Debug.DrawRay(puntoRay, direccion * rayDistance, Color.green);
                    if ((hit2.collider != null && hit2.collider.CompareTag("Escenario")))
                    {

                        direccionesObstruidas[i] = true;
                        Debug.DrawRay(puntoRay, direccion * rayDistance, Color.red);
                    }
                    else
                    {
                        Debug.DrawRay(puntoRay, direccion * rayDistance, Color.green);
                        direccionesObstruidas[i] = false;
                        numDirLibres++;
                    }


                      
                }

            } else
            {
                puntoRay = transform.position + new Vector3(-0.8f, 0, 0);
                RaycastHit2D hit = Physics2D.Raycast(puntoRay, direccion, rayDistance);

                if ((hit.collider != null && hit.collider.CompareTag("Escenario")))
                {
                    //Debug.Log(direccion);

                    direccionesObstruidas[i] = true;
                    Debug.DrawRay(puntoRay, direccion * rayDistance, Color.red);
                }
                else
                {
                    Debug.DrawRay(puntoRay, direccion * rayDistance, Color.green);
                    puntoRay = transform.position + new Vector3(0.8f, 0, 0);
                    RaycastHit2D hit2 = Physics2D.Raycast(puntoRay, direccion, rayDistance);
                    Debug.DrawRay(puntoRay, direccion * rayDistance, Color.green);
                    if ((hit2.collider != null && hit2.collider.CompareTag("Escenario")))
                    {

                        direccionesObstruidas[i] = true;
                        Debug.DrawRay(puntoRay, direccion * rayDistance, Color.red);
                    }
                    else
                    {
                        Debug.DrawRay(puntoRay, direccion * rayDistance, Color.green);
                        direccionesObstruidas[i] = false;
                        numDirLibres++;
                    }



                }

            }

           

           

         /*   //se usa para detectar player
            hit = Physics2D.Raycast(transform.position, direccion, rayDistance);
            Debug.DrawRay(transform.position, direccion * rayDistance, Color.yellow);
            if ((hit.collider != null && hit.collider.CompareTag("Player")))
            {
                Debug.LogError("Error player");
                //Debug.Break();
                //Debug.Log("Este mensaje no debería imprimirse");


               

                    direccionesObstruidas[i] = false;

                }
         */
        }


    }
    int escogerDireccionLibre()//la usamos para que nos devuelva una direccion disponible random
    {
        int indiceNuevaDireccion = Random.Range(0, direccionesObstruidas.Length);

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
                //anim.SetInteger("action", 3);
                break;
            case 1:
                direccion = new Vector3(0, -1, 0);
                //anim.SetInteger("action", 2);
                break;
            case 2:
                direccion = new Vector3(-1, 0, 0);

                //anim.SetInteger("action", 0);
                break;
            default:

                direccion = new Vector3(1, 0, 0);
               // anim.SetInteger("action", 1);
                break;
        }
        return direccion;
    }
}
