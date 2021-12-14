using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{

    public static Vector2 previousPosition;
    public static Dictionary<GameObject,Vector2> selectedCardPrevPos = new Dictionary<GameObject,Vector2>(); 
    public static List<GameObject> selectedCardObjectsCS = S10Controller2.selectedCardObjects;
    public static float selectedYposition = -95.0f;

    void OnMouseDown()
    {
        // "this" refers to the CardScript object

        if (this.gameObject.transform.position.y != selectedYposition)
        {
            // On Select, save previous position, add card object to "selected" dictionary and list
            previousPosition = this.gameObject.transform.position;
            selectedCardPrevPos.Add(this.gameObject,previousPosition);
            selectedCardObjectsCS.Add(this.gameObject);
        }
        else
        {
            // On deselect, set card back to original position, and remove from dictionary and list
            this.gameObject.transform.position = selectedCardPrevPos[this.gameObject];  
            selectedCardPrevPos.Remove(this.gameObject);
            selectedCardObjectsCS.Remove(this.gameObject);
        }
        
        S10Controller2.Reposition(selectedCardObjectsCS,47.0f,selectedYposition);

    }

    public static void ClearSelection()
    {

        foreach (KeyValuePair<GameObject,Vector2> entry in selectedCardPrevPos)
        {
            entry.Key.transform.position = entry.Value;
        }

        selectedCardPrevPos.Clear();
        selectedCardObjectsCS.Clear();

    }
}