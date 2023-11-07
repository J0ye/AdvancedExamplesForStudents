using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DoorTrigger : MonoBehaviour
{
    public PlayableDirector pd;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            pd.Play();
            Invoke("Pause", (float)pd.duration / 2);
        }
    }

    public void Pause()
    {
        pd.Pause();
    }
}
