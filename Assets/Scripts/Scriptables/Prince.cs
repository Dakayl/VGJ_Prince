using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Deck", menuName = "ScriptableObjects/Prince", order = 3)]

public class Prince:ScriptableObject {

    public String princeName;
    public Card startingCard;
    public Deck starterDeck; 
    public Deck eventDeck; 
}