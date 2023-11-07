using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    public Vector3 direction = Vector3.one;
    public float speed = 1f;

    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position + direction;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
