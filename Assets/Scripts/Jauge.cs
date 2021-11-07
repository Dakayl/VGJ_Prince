using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jauge : MonoBehaviour
{
    [SerializeField] private Image color;

    void Start()
    {
      resetColor();
    }
    

    public float value
    {
        set {
            float newWidth = Mathf.Lerp(0, 280, value/100);
            color.rectTransform.sizeDelta = new Vector2(newWidth, 22);
        }
    }

    public void triggerColor(){
        color.color = new Color32(7,149,167,255);
    }

    public void resetColor(){
        color.color = new Color32(160,104,6,255);
    }


}
