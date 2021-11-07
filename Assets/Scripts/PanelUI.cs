using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable] struct itemStructure
{
    public CardPanel cardPanel;
    public ChoicePanel choiceLeft;
    public ChoicePanel choiceRight;
    public JaugeUI jauges;
} 

public class PanelUI : MonoBehaviour
{
   
    [SerializeField] private itemStructure items;
    private bool isLeftSelected = false;
    private bool isRightSelected = false;
    [SerializeField] private Card currentCard;
     public delegate void OnLeftHandler();
    public event OnLeftHandler OnLeft;
    public delegate void OnRightHandler();
    public event OnRightHandler OnRight;
    public delegate void OnNewCardHandler();
    public event OnNewCardHandler OnNewCard;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(this.Instantiate());
    }
    
    public IEnumerator Instantiate(){
        while( items.choiceLeft == null) yield return null;
        while( items.choiceRight == null) yield return null;
        while( items.cardPanel == null) yield return null;
        items.cardPanel.SwipingLeft += ChoosingLeft;
        items.cardPanel.SwipingRight += ChoosingRight;
        items.cardPanel.NotSwiping += ResetSide;
        items.cardPanel.OnRelease += ReleasingCard;
        items.cardPanel.OnNewCard += NewCard;
        card = currentCard;
        yield break;

    }

    public Card card {
        set {
            title = value.title;
            description = value.description;
            left = value.leftChoice.text;
            right = value.rightChoice.text;
            currentCard = value;
        }
    }

    public string description
    {
        get { return items.cardPanel.description; }
        set { items.cardPanel.description = value; }
    }

    public string title
    {
        get { return items.cardPanel.title; }
        set { items.cardPanel.title = value; }
    }

    public string left
    {
        get { return items.choiceLeft.text; }
        set { items.choiceLeft.text = value; }
    }

    public string right
    {
        get { return items.choiceRight.text; }
        set { items.choiceRight.text = value; }
    }

    public void NewCard(){
        if(OnNewCard != null) OnNewCard();
    }

    public void ChoosingLeft() {
        isLeftSelected = true;
        HighlightChoice(currentCard.leftChoice);
        
    }

    public void ChoosingRight() {
        isRightSelected = true;
        HighlightChoice(currentCard.rightChoice);
    }
  

    public void HighlightChoice( Card.choice choice) {
        items.jauges.resetColors();
        if(choice.reactionaryValue != 0) {
            items.jauges.triggerReactionary();
            
        } 
        if(choice.progressiveValue != 0) {
            items.jauges.triggerProgressive();
        }
        if(choice.masqueradeValue != 0) {
            items.jauges.triggerMasquerade();
        }
        if(choice.boonValue != 0) {
            items.jauges.triggerBoon();
        }
         if(choice.anarchValue != 0) {
            items.jauges.triggerAnarchs();
        }
    }

    public void ReleasingCard(){
        items.jauges.resetColors();
        
        if(isLeftSelected) {
           
            if(OnLeft != null) OnLeft();
             items.cardPanel.Die(true);
            return;
        }
        
        if(isRightSelected) {
            if(OnRight != null) OnRight(); 
            items.cardPanel.Die(false);   
            return;
        }
        
        items.cardPanel.RestartPosition();

    }

    public void ResetSide() {
        isLeftSelected = false;
        isRightSelected = false;
        items.jauges.resetColors();
    }

    
}