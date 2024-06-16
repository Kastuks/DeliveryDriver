using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{

    [SerializeField] float steerSpeed = 250f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float fastSpeed = 40f;
    [SerializeField] float regularSpeed = 20f;

    List<Coroutine> restoreSpeedCoroutines = new();


    // Start is called before the first frame update
    void Start()
    {
        // transform.Rotate(0, 0, 45);
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("SpeedUp")) {
            moveSpeed = fastSpeed;
            Destroy(other.gameObject);
            restoreSpeedCoroutines.Add(StartCoroutine(ReturnToRegularSpeed()));
        } else if (other.CompareTag("SlowDown")) {
            moveSpeed  = slowSpeed;
            Destroy(other.gameObject);
            restoreSpeedCoroutines.Add(StartCoroutine(ReturnToRegularSpeed()));
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boundary")) {
            return;
        } else {
            moveSpeed = slowSpeed;
            restoreSpeedCoroutines.Add(StartCoroutine(ReturnToRegularSpeed()));
        }
    }

    IEnumerator ReturnToRegularSpeed() {
        var index = restoreSpeedCoroutines.Count;
        while (index != 0) {
            StopCoroutine(restoreSpeedCoroutines[0]);
            restoreSpeedCoroutines.RemoveAt(0);
            index = restoreSpeedCoroutines.Count;    
        }
        yield return new WaitForSeconds(5f);
        moveSpeed = regularSpeed;
        
    }
}
