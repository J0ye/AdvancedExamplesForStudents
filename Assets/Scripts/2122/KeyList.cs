using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyList : MonoBehaviour
{
    [Tooltip("Will be populated automatically")]
    public List<Image> keyImages = new List<Image>();

    public int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            Image newChild;
            if(child.gameObject.TryGetComponent<Image>(out newChild))
            {
                keyImages.Add(newChild);
                newChild.gameObject.SetActive(false);
            }
        }
    }

    public void IncreaseIndex()
    {
        index++;
        ClampIndexToListCount();
        UpdateDisplay();
    }

    public void DecreaseIndex()
    {
        index--;
        ClampIndexToListCount();
        UpdateDisplay();
    }

    protected void UpdateDisplay()
    {
        int counter = 1;
        foreach(Image image in keyImages)
        {
            if(index >= counter)
            {
                image.gameObject.SetActive(true);
            } else
            {
                image.gameObject.SetActive(false);
            }
            counter++;
        }
    }

    protected void ClampIndexToListCount()
    {
        index = Mathf.Clamp(index, 0, keyImages.Count);
    }
}
