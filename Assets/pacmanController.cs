using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pacmanController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direccion;
    public float velocidad = 10;
    //[SerializeField]
    //int pills = 0;

    public gameManager manager;


    public GameObject visualObject;//objeto que vemos, no contiene colliders

    void Start()
    {
        direccion = transform.right;
        
    }

    // Update is called once per frame
    void Update()
    {
        //direccion = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("vertical"),0);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direccion = -transform.up;
            visualObject.transform.eulerAngles = new Vector3(0,0,-90);


        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            direccion = transform.up;
            visualObject.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direccion = -transform.right;
            visualObject.transform.eulerAngles = new Vector3(0, 0, 180);
            //visualObject.GetComponent<SpriteRenderer>().flipX = true;
           
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            direccion = transform.right;
            visualObject.transform.eulerAngles = new Vector3(0, 0, 0);
            //visualObject.GetComponent<SpriteRenderer>().flipX = false;
        }




        transform.Translate(direccion * Time.deltaTime * velocidad);
        //transform.Translate(transform.up * Input.GetAxis("Vertical") * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pill")
        {
            print("Conseguida pill trigger");
            //pills++;
            manager.pillConseguida();

            Destroy(collision.gameObject);
        }else if (collision.gameObject.tag == "SuperPill")
        {
            print("Conseguida Super Pill");
            //pills++;
            //manager.pillConseguida();
            manager.modoCazafantasma();



            Destroy(collision.gameObject);
        }


    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<enemigo>().isScapping==true)
            {
                Destroy(collision.gameObject);

                manager.sumaPuntos(100);
            }
            else
            {

            print("Colisión con Enemy");
            //llamada a manager para restar vida
            manager.restaVida();

            }
           
        }
    }*/




}
