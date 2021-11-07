using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDeck : MonoBehaviour
{
   [SerializeField] private List<Card> current;
   [SerializeField] private Deck commonStartList;
   [SerializeField] private Deck commonEventList;
   [SerializeField] private Deck stockList;
   private List<Card> finalStockList;
   private int commonToAdd = 1;
   private int specificToAdd = 1;
   [SerializeField] private Card lackOfCardsDeath;
   [Serializable] public struct narration
   {
       public bool warningSent;
       public Card warning;
       public Card death;
   }
   [Serializable] public struct narrationCards
   {
       public narration tooMany;
       public narration notEnough;
   }
   [SerializeField] private narrationCards Boons;
   [SerializeField] private narrationCards Reacs;
   [SerializeField] private narrationCards Progs;
   [SerializeField] private narrationCards Masquerade;
   [SerializeField] private narrationCards Anarchs;

   public void CreateDeck(Prince prince) {
       current = new List<Card>(commonStartList.cards);
       current.AddRange(prince.starterDeck.cards);
       List<Card> commonList = new List<Card>(commonEventList.cards);
       while(commonToAdd > 0) {
           Card newCard = getOneAtRandom(commonList);
           if(newCard == null) break;
           commonList.Remove(newCard);
           addCardAtEnd(newCard);
           commonToAdd --;
       }
       List<Card> specificList = new List<Card>(prince.eventDeck.cards);
        while(specificToAdd > 0) {
           Card newCard = getOneAtRandom(specificList);
           if(newCard == null) break;
           commonList.Remove(newCard);
           addCardAtEnd(newCard);
           specificToAdd --;
       }
       shuffleDeck();
       addCardAtFirst(prince.startingCard);

       finalStockList = new List<Card>(commonList);
       finalStockList.AddRange(specificList);
       finalStockList.AddRange(stockList.cards);
       Boons.tooMany.warningSent = false;
       Boons.notEnough.warningSent = false;
       Reacs.tooMany.warningSent = false;
       Reacs.notEnough.warningSent = false;
       Progs.tooMany.warningSent = false;
       Progs.notEnough.warningSent = false;
       Masquerade.tooMany.warningSent = false;
       Masquerade.notEnough.warningSent = false;
       Anarchs.tooMany.warningSent = false;
       Anarchs.notEnough.warningSent = false;
   }

    public Card getOneAtRandom(List<Card> list){
        if(list.Count <1) return null;
        int position = UnityEngine.Random.Range(0, list.Count);
        Card chosen = list[position];
        return chosen;
    }

   protected void shuffleDeck(){
        for (int i = 0; i < current.Count; i++) {
            Card temp = current[i];
            int randomIndex = UnityEngine.Random.Range(i, current.Count);
            current[i] = current[randomIndex];
            current[randomIndex] = temp;
        }
    }

    public void addCard(Card card, Card.addition positioning ){
        switch(positioning) {
            case Card.addition.immediately:
                addCardAtFirst(card);
                break;
            case Card.addition.later23Cards:
                addCardAtFirst(card);
                break;
            case Card.addition.atRandom :
                 addCardAtRandom(card);
                 break;
            case Card.addition.endOfDeck:
                addCardAtEnd(card);
                break;
        }
    }

    public Card getTopOfDeck() {
        if(current.Count < 2) {
            refillDeck();
        }
        if(current.Count < 1) {
            return lackOfCardsDeath;
        }
        Card card = current[0];
        current.Remove(card);
        return card;
    }
    public void refillDeck(){
        if(finalStockList.Count < 1) return;
        addCardAtEnd( getOneAtRandom(finalStockList) ) ;
    }
   
    private void addCardAtEnd(Card card) {
       current.Add(card);
    }

    private void addCardAtFirst(Card card) {
        current.Insert(0, card);
    }

    public void addCardAtRandom(Card card) {
         if(current.Count < 2) 
        {
             addCardAtEnd(card); 
             return;
        }
        int addCardAtRandom = UnityEngine.Random.Range(0, current.Count);
        current.Insert(addCardAtRandom, card);
    }

    private void addCard23(Card card) {
        if(current.Count < 4) 
        {
             addCardAtEnd(card); 
             return;
        }
        int addCardAtRandom = UnityEngine.Random.Range(2, 4);
        current.Insert(addCardAtRandom, card);
    }

    private void addWarning(narration narration) {
       if(narration.warningSent) return;
       addCardAtFirst(narration.warning);
       narration.warningSent = true;
    }

    /** BOONS **/

    public void addTooManyBoonsWarning(){
        addWarning(Boons.tooMany);
    }

    public void deathTooManyBoons(){
        addCardAtFirst(Boons.tooMany.death);
    }

    public void addNotEnoughBoonsWarning(){
        addWarning(Boons.notEnough);
    }

    public void deathNotEnoughBoons(){
        addCardAtFirst(Boons.notEnough.death);
    }

    /** REACS **/

    public void addTooManyReacsWarning(){
        addWarning(Reacs.tooMany);
    }

    public void deathTooManyReacs(){
        addCardAtFirst(Reacs.tooMany.death);
    }

    public void addNotEnoughReacsWarning(){
        addWarning(Reacs.notEnough);
    }

    public void deathNotEnoughReacs(){
        addCardAtFirst(Reacs.notEnough.death);
    }

    /** PROGS **/

    public void addTooManyProgsWarning(){
        addWarning(Progs.tooMany);
    }

    public void deathTooManyProgs(){
        addCardAtFirst(Progs.tooMany.death);
    }

    public void addNotEnoughProgsWarning(){
        addWarning(Progs.notEnough);
    }

    public void deathNotEnoughProgs(){
        addCardAtFirst(Progs.notEnough.death);
    }

    /** MASQUERADE **/

    public void addTooManyMasqueradeWarning(){
        addWarning(Masquerade.tooMany);
    }

    public void deathTooManyMasquerade(){
        addCardAtFirst(Masquerade.tooMany.death);
    }

    public void addNotEnoughMasqueradeWarning(){
        addWarning(Masquerade.notEnough);
    }

    public void deathNotEnoughMasquerade(){
        addCardAtFirst(Masquerade.notEnough.death);
    }

    /** ANARCHS **/

    public void addTooManyAnarchsWarning(){
        addWarning(Anarchs.tooMany);
    }

    public void deathTooManyAnarchs(){
        addCardAtFirst(Anarchs.tooMany.death);
    }

    public void addNotEnoughAnarchsWarning(){
        addWarning(Anarchs.notEnough);
    }

    public void deathNotEnoughAnarchs(){
        addCardAtFirst(Anarchs.notEnough.death);
    }
}
