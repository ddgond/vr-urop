using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private Renderer renderer;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            Color oldCol = renderer.material.color;
            if (oldCol.a <= 0)
            {
                Destroy(gameObject);
                return;
            }

            Color newCol = new Color(oldCol.r, oldCol.g, oldCol.b, oldCol.a - 1.5f*Time.deltaTime);
            renderer.material.color = newCol;
            Debug.Log(newCol);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Weapon"))
        {
            dead = true;
            //Instantiate(explode, location.position, transform.rotation);

        }

    }
}
