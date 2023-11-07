using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour
{
    public GameObject prefab;

    public void OnMouseDown()
    {
        Instantiate(prefab, transform);
    }
}
