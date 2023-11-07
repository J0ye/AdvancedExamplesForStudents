using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    // Explosion prefab for when the asteroid is destroyed
    public GameObject explosionPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the player collides with the asteroid, destroy the asteroid and create an explosion
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
    }
}
