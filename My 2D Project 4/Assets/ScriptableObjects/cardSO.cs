using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]

public class cardSO : ScriptableObject
{
    public int value;
    public string suit;
}
