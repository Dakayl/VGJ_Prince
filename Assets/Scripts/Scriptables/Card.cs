using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]

public class Card:ScriptableObject
{
    [Serializable] public enum addition {immediately, later23Cards, atRandom, endOfDeck};
    [Serializable] public struct choice
    {
        public string text; 
        public int reactionaryValue;
        public int progressiveValue;
        public int masqueradeValue;
        public int boonValue;
        public int anarchValue;
        public Card addCard;
        public addition howToAddCard;
    } 
    public string title;
    public string description;
    public Sprite characterImage;
    [SerializeField] public choice leftChoice;
    [SerializeField] public choice rightChoice;
    public bool isDeathCard = false;
}


