using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private Renderer renderer;
    private GameManager game;
    private TextMeshPro text;
    private bool dead = false;
    private float decayRate = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        text = GetComponentInChildren<TextMeshPro>();
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            if (transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);
                
            Color oldCol = renderer.material.color;
            if (oldCol.a <= 0)
            {
                Destroy(gameObject);
                return;
            }

            Color newCol = new Color(oldCol.r, oldCol.g, oldCol.b, oldCol.a - decayRate*Time.deltaTime);
            renderer.material.color = newCol;
        }

        if (rb.velocity.magnitude < 0.01)
        {
            // if ball gets stuck somewhere, despawn it
            dead = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Weapon"))
        {
            dead = true;
            game.SubmitAnswer(text.text);
            //Instantiate(explode, location.position, transform.rotation);

        }

    }
}
