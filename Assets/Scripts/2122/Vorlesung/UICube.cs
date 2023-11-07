using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICube : MonoBehaviour
{
    public GameObject prefab;
    public void OnMouseDown()
    {
        Instantiate(prefab, transform);
    }
}
