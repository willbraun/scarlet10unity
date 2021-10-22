using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetValueSuit : MonoBehaviour
{
    public cardSO thisCardSO;

    public int returnValue()
    {
        return thisCardSO.value;
    }

    public string returnSuit()
    {
        return thisCardSO.suit;
    }
}
