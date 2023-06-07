using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro : MonoBehaviour
{
    [Header("Rigid Body")]
    public Rigidbody2D rb;

    [Header("Velocidade e aceleração")]
    [SerializeField] private float maxSpeed, acceleration;

    [Header("Direção")]
    public float hori;

    [Header("Combustível")]
    public float fuel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.transform.rotation);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (fuel > 0)
        {
            rb.velocity = rb.velocity * 1f;

            Debug.Log("velocity " + rb.velocity);

            hori = Input.GetAxis("Horizontal");

            float impulse = -hori * Mathf.Deg2Rad * rb.inertia;

            Debug.Log("torque " + impulse);


            rb.AddRelativeForce(Vector2.up * acceleration);

            rb.AddTorque(impulse, ForceMode2D.Impulse);

            fuel -= Time.deltaTime;

            if (fuel < 0)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                rb.angularDrag = 0;
            }
        }
    }
}
