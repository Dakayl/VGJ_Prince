using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;
using TMPro;

public class ChoicePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI TMP;
    public delegate void OverHandler();
    public event OverHandler OnOver;
    public delegate void ExitHandler();
    public event ExitHandler OnExit;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        if(OnOver != null) OnOver();
    }

     public void OnPointerExit(PointerEventData eventData)
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        if(OnExit != null) OnExit();
    }

    public string text
    {
        get { return TMP.text; }
        set {TMP.text = value; }
    }

}
