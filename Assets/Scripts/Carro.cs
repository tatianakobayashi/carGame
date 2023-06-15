using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Carro : MonoBehaviour
{
    [Header("Rigid Body")]
    public Rigidbody2D rb;

    [Header("Velocidade e aceleração")]
    [SerializeField] private float maxSpeed, acceleration, target, currentSpeed;

    [Header("Direção")]
    public float hori;

    [Header("Combustível")]
    public float fuel;

    private float drift;
    private float aux;

    private Vector2 speed;
    private Vector2 relativeForce;

    private List<GameObject> marcas = new List<GameObject>();
    [SerializeField] private Transform rd, re;

    void AddMarcas()
    {
        GameObject marcaRD = GameObject.Instantiate(Resources.Load("Marcas")) as GameObject;
        GameObject marcaRE = GameObject.Instantiate(Resources.Load("Marcas")) as GameObject;

        marcaRD.transform.position = rd.position;
        marcaRD.transform.rotation = rd.rotation;

        marcaRE.transform.position = re.position;
        marcaRE.transform.rotation = re.rotation;

        marcas.Add(marcaRD);
        marcas.Add(marcaRE);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //if (Mathf.Abs(transform.position.x) > 10.8f) Debug.Log("Saiu da tela");

        if (fuel > 0)
        {
            hori = -Input.GetAxis("Horizontal");

            speed = transform.up * acceleration;
            rb.AddForce(speed);

            //Debug.Log("velocity " + rb.velocity);

            aux = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));

            if (aux >= 0.0f)
            {
                rb.rotation += hori * target * (rb.velocity.magnitude / maxSpeed * 0.8f);
            }
            else
            {
                rb.rotation -= hori * target * (rb.velocity.magnitude / maxSpeed * 0.8f);
            }

            drift = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;
            relativeForce = Vector2.right * drift;
            rb.AddForce(rb.GetRelativeVector(relativeForce));

            if(rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            currentSpeed = rb.velocity.magnitude;


            //Debug.Log(rb.velocity.magnitude);   

            if(rb.velocity.magnitude <= 4.9f)
            {
                AddMarcas();
            }

            //fuel -= Time.deltaTime;
            if (fuel < 0)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                rb.angularDrag = 0;
            }
        }
    }
}
