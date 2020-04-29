using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameManager game;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        InvokeRepeating("SpawnBall", 2.0f, 0.35f);
    }

    void SpawnBall()
    {
        string text = game.RequestText();
        if (text == "") return; // out of balls to spawn

        GameObject instance = Instantiate(ballPrefab, nextPosition(), Quaternion.identity);
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        TextMeshPro textMesh = instance.GetComponentInChildren<TextMeshPro>();

        textMesh.text = text;
        rb.velocity = new Vector3(0, Random.Range(4, 8), Random.Range(25,35));
    }

    Vector3 nextPosition()
    {
        return transform.GetChild(Random.Range(0, transform.childCount)).position; 
    }
}
