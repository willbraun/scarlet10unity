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

    //public GetValueSuit getValueSuitScript;

    void OnMouseDown()
    {
        // "this" refers to the CardScript object

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
        // See what type of hand is on the table
        List<int> tableCardValues = getCardValues(tableCardObjects);
        List<int> selectedCardValues = getCardValues(selectedCardObjects);

        string errorMessage = "";

        // Debug.Log("Triple? " + isTriple(selectedCardValues));
        // if (tableCardValues.Count == 1)
        // {
        //     // single card validation
        // }
        // else if (tableCardValues.Count == 2)
        // {
        //     // double card validation
        // }
        // else if (tableCardValues.Count == 3)
        // {
        //     // 
        // }
        //single - count = 1
        //double - count = 2, same
        //triple - count = 3, same
        //quad - count = 4, same
        //straight - count >= 3, consecutive, doesn't include 2
        //straight of doubles - odds are consecutive, evens are consecutive, doesn't include 2
        //4-4-Ace - values equal 4, 4, 14
        //red10s - count = 2, same, value[0] = 10, suits = hearts and diamonds
        //black10s - count = 2, same, value[0] = 10, suits = clubs and spades
        //2joker - count = 2, same, value[0] = 16





        // Validate that hand is that type or bomb

        // Check values to see if higher


        // Validation of if selectedCardObjects is valid, with error if not





        // Validation of if selectedCardObjects beats tableCardObjects, with error if not

        // If hand is valid
        foreach (GameObject card in tableCardObjects)
        {
            Destroy(card);
        }

        Reposition(selectedCardObjects,-1.0f,92.0f);

        foreach (GameObject card in selectedCardObjects)
        {
            handsCS[0].Remove(card);
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

    
    public List<int> getCardValues(List<GameObject> listOfCards)
    {
        
        List<int> cardValues = new List<int>();

        foreach (GameObject card in listOfCards)
        {
            cardValues.Add(card.GetComponent<GetValueSuit>().returnValue());
        }

        return cardValues;

    }

    // write getCardSuits function

    public bool isConsecutive(List<int> listOfCardValues)
    {
        bool result = false;
        
        for (int i = 0; i < listOfCardValues.Count - 1; i++)
        {
            if (listOfCardValues[i] + 1 != listOfCardValues[i+1])
            {
                break;
            }

            result = true;
        }

        return result;

    }

    public bool isAllSame(List<int> listOfCardValues)
    {
        if (listOfCardValues.Count == 1)
        {
            return true;
        }

        bool result = false;

        for (int i = 0; i < listOfCardValues.Count - 1; i++)
        {
            if (listOfCardValues[i] != listOfCardValues[i+1])
            {
                break;
            }

            result = true;
        }

        return result;

    }

    public bool isTriple(List<int> listOfCardValues)
    {
        bool result = false;

        if (listOfCardValues.Count == 3 && isAllSame(listOfCardValues) == true)
        {
            result = true;
        }

        return result;
    }

    public bool isQuad(List<int> listOfCardValues)
    {
        bool result = false;

        if (listOfCardValues.Count == 4 && isAllSame(listOfCardValues) == true)
        {
            result = true;
        }

        return result;
    }


    // void Update()
    // {
    //     float speed = 0.1f;
    //     float step = speed * Time.deltaTime;
    //     transform.position = Vector2.MoveTowards(new Vector2(transform.position.x,transform.position.y), new Vector2(xposition,yposition), step);
    // }

}
