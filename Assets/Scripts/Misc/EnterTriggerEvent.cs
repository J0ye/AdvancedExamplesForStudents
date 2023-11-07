using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerEvent : MonoBehaviour
{
    public string targetTag = "Untagged";
    public UnityEvent OnEnter = new UnityEvent();

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(targetTag)) OnEnter.Invoke();
    }
}
