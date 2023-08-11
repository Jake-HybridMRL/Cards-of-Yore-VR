using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CharacterData : ScriptableObject
{

    public new string name;
    public string description;

    public int mana;
    public int health;
    public int attack;

    public Sprite artwork;
    public GameObject hologram;

}
