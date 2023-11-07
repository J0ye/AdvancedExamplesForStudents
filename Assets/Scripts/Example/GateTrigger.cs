using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GateTrigger : MonoBehaviour
{
    private PlayableDirector pd;

    public void Start()
    {
        if (!transform.parent.gameObject.TryGetComponent<PlayableDirector>(out pd))
            Destroy(this);
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ControlGate();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ControlGate();
        }
    }

    public void OnMouseDown()
    {
        ControlGate();
    }

    public void ControlGate()
    {
        if(pd.time == 0f)
        {
            pd.Play();
            Invoke("Pause", (float)pd.duration / 2);
        } else
        {
            pd.Play();
            Invoke("Reset", (float)pd.duration / 2);
        }
    }

    protected void Pause()
    {
        pd.Pause();
    }

    protected void Reset()
    {
        pd.Stop();
    }
}
