using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{

    // Dictionary of 'CardScript' objects and their position before moving
    public static Vector2 previousPosition;
    public static Dictionary<CardScript,Vector2> selectedCardPrevPos = new Dictionary<CardScript,Vector2>(); 

    // List of selected cards for positioning
    public static List<CardScript> selectedCardObjects = new List<CardScript>();

    void OnMouseDown()
    {

        float yposition = -95.0f;

        if (this.transform.position.y != yposition)
        {
            // On Select
            // Save previous position, add card object to "selected" dictionary and list
            previousPosition = this.transform.position;
            selectedCardPrevPos.Add(this,previousPosition);
            selectedCardObjects.Add(this);
        }
        else
        {
            // On Deselect
            // Set card back to original position, and remove from dictionary and list
            this.transform.position = selectedCardPrevPos[this];  
            selectedCardPrevPos.Remove(this);
            selectedCardObjects.Remove(this);
        }
        
        // Reposition selected cards
        float increment = 52.0f;
        float cardSpread = increment*(selectedCardObjects.Count - 1);
        float xposition = cardSpread*(-0.5f); // The -1 is because there are one less spaces than cards
        
        for (int i = 0; i < selectedCardObjects.Count; i++)
        {
            GameObject.Find(selectedCardObjects[i].name).transform.position = new Vector2(xposition,yposition);
            xposition += increment;
        }

    }

}
