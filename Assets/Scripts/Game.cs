using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    private int currentTurn = 1;
    private int reactionaryValue;
    private int progressiveValue;
    private int masqueradeValue;
    private int boonsValue;
    private int anarchsValue;
    private Card currentCard;
    [SerializeField] private int startReactionnaryValue = 50;
    [SerializeField] private int startProgressiveValue = 50;
    [SerializeField] private int startMasqueradeValue = 50;
    [SerializeField] private int startBoonsValue = 50;
    [SerializeField] private int startAnarchsValue = 50;
    private int notEnoughValue = 1;
    private int tooMuchValue = 99;
    private int notEnoughWarningValue = 11;
    private int tooMuchWarningValue = 89;
    [SerializeField] private TextMeshProUGUI turn;
    [SerializeField] private PanelUI mainPanel;
    [SerializeField] private PlayerDeck deck;
    [SerializeField] private Prince prince;

    public JaugeUI jauges;

    // Start is called before the first frame update
    void Start()
    {
        reactionaryValue = startReactionnaryValue;
        progressiveValue = startProgressiveValue;
        masqueradeValue = startMasqueradeValue;
        boonsValue = startBoonsValue;        
        anarchsValue = startAnarchsValue;
        StartCoroutine(this.Instantiate());
    }
    
    public IEnumerator Instantiate(){
        while( jauges == null) yield return null;
        while( turn == null) yield return null;
        while( mainPanel == null) yield return null;
        while( deck == null) yield return null;
        RefreshData();
        mainPanel.OnLeft += OnLeftChangeValues;
        mainPanel.OnRight += OnRightChangeValues;
        mainPanel.OnNewCard += NewCard;
        getStarted();
        yield break;

    }

    public void getStarted(){
        deck.CreateDeck(prince);
        NewCard();
    }

    public void RefreshData() {
        jauges.reactionary = reactionaryValue;
        jauges.progressive = progressiveValue;
        jauges.masquerade = masqueradeValue;
        jauges.boon = boonsValue;
        jauges.anarch = anarchsValue;
        turn.text = "Turn " + currentTurn.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewCard () {
        if(currentCard != null 
        && currentCard.isDeathCard) {
            
            return;
        }
        card = deck.getTopOfDeck();
    }


    public Card card {
        set {
            currentCard = value;
            mainPanel.card = value;
        }
    }

    public void  OnLeftChangeValues(){
        reactionaryValue += currentCard.leftChoice.reactionaryValue;
        progressiveValue += currentCard.leftChoice.progressiveValue;
        masqueradeValue += currentCard.leftChoice.masqueradeValue;
        boonsValue += currentCard.leftChoice.boonValue;
        anarchsValue += currentCard.leftChoice.anarchValue;
        currentTurn++;
        if(currentCard.leftChoice.addCard != null) {
            deck.addCard(currentCard.leftChoice.addCard, currentCard.leftChoice.howToAddCard);
        }
        checkValues();
        RefreshData();
    }
    public void  OnRightChangeValues(){
        reactionaryValue += currentCard.rightChoice.reactionaryValue;
        progressiveValue += currentCard.rightChoice.progressiveValue;
        masqueradeValue += currentCard.rightChoice.masqueradeValue;
        boonsValue += currentCard.rightChoice.boonValue;
        anarchsValue += currentCard.rightChoice.anarchValue;
        currentTurn++;
         if(currentCard.leftChoice.addCard != null) {
            deck.addCard(currentCard.rightChoice.addCard, currentCard.rightChoice.howToAddCard);
        }
        checkValues();
        RefreshData();
    }

    public void checkValues(){
        
        Debug.Log("Boons :  "+boonsValue);
        if(boonsValue < notEnoughValue) {
            deck.deathNotEnoughBoons();
            return;
        }
        if(boonsValue > tooMuchValue) {
            deck.deathTooManyBoons();
            return;
        }
        if(boonsValue < notEnoughWarningValue) {
            deck.addNotEnoughBoonsWarning();
        }
        if(boonsValue > tooMuchWarningValue) {
            deck.addTooManyBoonsWarning();
        }

        Debug.Log("Reactionaries :  "+reactionaryValue);
        if(reactionaryValue < notEnoughValue) {
            deck.deathNotEnoughReacs();
            return;
        }
        if(reactionaryValue > tooMuchValue) {
            deck.deathTooManyReacs();
            return;
        }
        if(reactionaryValue < notEnoughWarningValue) {
            deck.addNotEnoughReacsWarning();
        }
        if(reactionaryValue > tooMuchWarningValue) {
            deck.addTooManyReacsWarning();
        }

        Debug.Log("Progressives :  "+progressiveValue);
        if(progressiveValue < notEnoughValue) {
            deck.deathNotEnoughProgs();
            return;
        }
        if(progressiveValue > tooMuchValue) {
            deck.deathTooManyProgs();
            return;
        }
        if(progressiveValue < notEnoughWarningValue) {
            deck.addNotEnoughProgsWarning();
        }
        if(progressiveValue > tooMuchWarningValue) {
            deck.addTooManyProgsWarning();
        }

        Debug.Log("Masquerade :  "+masqueradeValue);
        if(masqueradeValue < notEnoughValue) {
            deck.deathNotEnoughMasquerade();
            return;
        }
        if(masqueradeValue > tooMuchValue) {
            deck.deathTooManyMasquerade();
            return;
        }
        if(masqueradeValue < notEnoughWarningValue) {
            deck.addNotEnoughMasqueradeWarning();
        }
        if(masqueradeValue > tooMuchWarningValue) {
            deck.addTooManyMasqueradeWarning();
        }

        Debug.Log("Anarchs :  "+anarchsValue);
        if(anarchsValue < notEnoughValue) {
            deck.deathNotEnoughAnarchs();
            return;
        }
        if(anarchsValue > tooMuchValue) {
            deck.deathTooManyAnarchs();
            return;
        }
        if(anarchsValue < notEnoughWarningValue) {
            deck.addNotEnoughAnarchsWarning();
        }
        if(anarchsValue > tooMuchWarningValue) {
            deck.addTooManyAnarchsWarning();
        }
    }
}
