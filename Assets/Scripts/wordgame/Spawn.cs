using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject ballPrefab;

    void Start()
    {
        InvokeRepeating("SpawnBall", 2.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnBall()
    {
        GameObject instance = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = instance.GetComponent<Rigidbody>();

        rb.velocity = new Vector3(0, Random.Range(0, 15), Random.Range(25,40));

    }
}
