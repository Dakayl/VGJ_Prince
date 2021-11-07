using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Deck", menuName = "ScriptableObjects/Deck", order = 2)]

public class Deck:ScriptableObject {
    public List<Card> cards; 
}

