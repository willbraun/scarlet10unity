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

    public float yposition = -95.0f;
    public static float increment = 47.0f;
    public float cardSpread = 0.0f;
    public float xposition = 0.0f;

    void OnMouseDown()
    {

        if (this.transform.position.y != yposition)
        {
            // On Select, save previous position, add card object to "selected" dictionary and list
            previousPosition = this.transform.position;
            selectedCardPrevPos.Add(this,previousPosition);
            selectedCardObjects.Add(this);
        }
        else
        {
            // On deselect, set card back to original position, and remove from dictionary and list
            this.transform.position = selectedCardPrevPos[this];  
            selectedCardPrevPos.Remove(this);
            selectedCardObjects.Remove(this);
        }
        
        // Sort and reposition selected cards
        char[] sortArray = new char[15] {'3','4','5','6','7','8','9','1','J','Q','K','A','2','B','R'}; // Used char instead of string since name[0] returns a char
        selectedCardObjects.Sort((x,y) => Array.IndexOf(sortArray,x.name[0]).CompareTo(Array.IndexOf(sortArray,y.name[0])));

        cardSpread = increment*(selectedCardObjects.Count - 1);
        xposition = cardSpread*(-0.5f); // The -1 is because there are one less spaces than cards
        
        foreach (CardScript card in selectedCardObjects)
        {
            card.transform.position = new Vector2(xposition,yposition);
            xposition += increment;
        }


        // for (int i = 0; i < selectedCardObjects.Count; i++)
        // {
        //     GameObject.Find(selectedCardObjects[i].name).transform.position = new Vector2(xposition,yposition);
        //     xposition += increment;
        // }

    }

    public void ClearSelection()
    {
        foreach (KeyValuePair<CardScript,Vector2> entry in selectedCardPrevPos)
        {
            entry.Key.transform.position = entry.Value;
        }
        selectedCardPrevPos.Clear();
        selectedCardObjects.Clear();
    }

    // public void PlayCards();
    // {




    // }



    // void Update()
    // {
    //     float speed = 0.1f;
    //     float step = speed * Time.deltaTime;
    //     transform.position = Vector2.MoveTowards(new Vector2(transform.position.x,transform.position.y), new Vector2(xposition,yposition), step);
    // }

}
