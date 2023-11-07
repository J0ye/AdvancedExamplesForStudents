using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    // Prefab for the asteroid
    public GameObject asteroidPrefab;

    // Number of asteroids to create
    public int numAsteroids = 10;

    // Range for the position of the asteroids
    public Vector2 positionRange = new Vector2(10, 10);

    // Range for the size of the asteroids
    public Vector2 sizeRange = new Vector2(1, 3);

    // Range for the rotation of the asteroids
    public Vector2 rotationRange = new Vector2(0, 360);

    // Range for the speed of the asteroids
    public Vector2 speedRange = new Vector2(2, 5);

    void Start()
    {
        // Create the asteroids
        for (int i = 0; i < numAsteroids; i++)
        {
            // Create an asteroid
            GameObject asteroid = Instantiate(asteroidPrefab);

            // Set the position of the asteroid to a random position within the specified range
            asteroid.transform.position = new Vector3(Random.Range(-positionRange.x, positionRange.x), Random.Range(-positionRange.y, positionRange.y), 0);

            // Set the size of the asteroid to a random size within the specified range
            asteroid.transform.localScale = new Vector3(Random.Range(sizeRange.x, sizeRange.y), Random.Range(sizeRange.x, sizeRange.y), 1);

            // Set the rotation of the asteroid to a random rotation within the specified range
            asteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(rotationRange.x, rotationRange.y));

            // Set the speed of the asteroid to a random speed within the specified range
            asteroid.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-speedRange.x, speedRange.x), Random.Range(-speedRange.y, speedRange.y));
        }
    }
}

