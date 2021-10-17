using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{

    // Dictionary of 'CardScript' objects and their position before moving
    public static Vector2 previousPosition;
    public static Dictionary<GameObject,Vector2> selectedCardPrevPos = new Dictionary<GameObject,Vector2>(); 

    // List of selected cards for positioning
    public static List<GameObject> selectedCardObjects = new List<GameObject>();

    // List of cards on table
    public static List<GameObject> tableCardObjects = new List<GameObject>();

    public static char[] sortArray = new char[15] {'3','4','5','6','7','8','9','1','J','Q','K','A','2','B','R'};
    public static float selectedYposition = -95.0f;
    public static List<List<GameObject>> handsCS = S10Controller2.hands;

    void OnMouseDown()
    {

        if (this.gameObject.transform.position.y != selectedYposition)
        {
            // On Select, save previous position, add card object to "selected" dictionary and list
            previousPosition = this.gameObject.transform.position;
            selectedCardPrevPos.Add(this.gameObject,previousPosition);
            selectedCardObjects.Add(this.gameObject);
        }
        else
        {
            // On deselect, set card back to original position, and remove from dictionary and list
            this.gameObject.transform.position = selectedCardPrevPos[this.gameObject];  
            selectedCardPrevPos.Remove(this.gameObject);
            selectedCardObjects.Remove(this.gameObject);
        }
        
        Reposition(selectedCardObjects,47.0f,selectedYposition);

    }

    public void ClearSelection()
    {

        foreach (KeyValuePair<GameObject,Vector2> entry in selectedCardPrevPos)
        {
            entry.Key.transform.position = entry.Value;
        }

        selectedCardPrevPos.Clear();
        selectedCardObjects.Clear();

    }

    public void PlayCards()
    {
        // Validation of if selectedCardObjects is valid, with error if not

        // Validation of if selectedCardObjects beats tableCardObjects, with error if not

        // If hand is valid
        foreach (GameObject card1 in tableCardObjects)
        {
            Destroy(card1);
        }

        Reposition(selectedCardObjects,-1.0f,92.0f);

        foreach (GameObject card2 in selectedCardObjects)
        {
            handsCS[0].Remove(card2);
        }

        tableCardObjects.Clear();
        tableCardObjects.AddRange(selectedCardObjects);
        selectedCardPrevPos.Clear();
        selectedCardObjects.Clear();

        Reposition(handsCS[0],47.0f,-205.0f);

    }

    public void Reposition(List<GameObject> listOfCards, float increment, float yposition)
    {
        char[] sortArray = new char[15] {'3','4','5','6','7','8','9','1','J','Q','K','A','2','B','R'};
        listOfCards.Sort((x,y) => Array.IndexOf(sortArray,x.name[0]).CompareTo(Array.IndexOf(sortArray,y.name[0])));
        float cardSpread = 0.0f;

        if (increment == -1.0f)
        {
            cardSpread = 450.0f;
            increment = cardSpread/(listOfCards.Count -1);

            if (increment > 47.0f)
            {
                increment = 47.0f;
                cardSpread = increment*(listOfCards.Count - 1);
            }
        }
        else
        {
            cardSpread = increment*(listOfCards.Count - 1);
        }

        float xposition = cardSpread*(-0.5f);

        foreach (GameObject card in listOfCards)
        {
            card.transform.position = new Vector2(xposition,yposition);
            xposition += increment;
        }

    }




    // void Update()
    // {
    //     float speed = 0.1f;
    //     float step = speed * Time.deltaTime;
    //     transform.position = Vector2.MoveTowards(new Vector2(transform.position.x,transform.position.y), new Vector2(xposition,yposition), step);
    // }

}
