using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new(42, 195, 41, 255);
    [SerializeField] Color32 hasNoPackageColor = new(255, 255, 255, 255);
    bool packagePickedUp = false;
    [SerializeField] float destroyDelay = 0f;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("A collision happened");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package")) {
            if (!packagePickedUp) {
                PickupPackage(other);
                return;
            } else {
                Debug.Log("Package has already been picked up, deliver it!");
            }
        } else if (other.tag == "Customer") {
            if (packagePickedUp) {
                DeliverPackage(other);
            } else {
                Debug.Log("No package has been picked up for delivery!");
            }
        }
    }

    void PickupPackage(Collider2D other) {
        Debug.Log("Package picked up");
        spriteRenderer.color = hasPackageColor;
        packagePickedUp = true;
        Destroy(other.gameObject, destroyDelay);
    }
    void DeliverPackage(Collider2D other) {
        Debug.Log("Delivered package to customer");
        spriteRenderer.color = hasNoPackageColor;
        packagePickedUp = false;
    }
}
