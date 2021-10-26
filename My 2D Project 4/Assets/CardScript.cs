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

    public void PlayCards() // Add inputs for tableCardObjects and selectedCardObjects
    {
        
        //characterize table card objects
        //characterize selected card objects
        string selectedType = characterize(selectedCardObjects);
        Debug.Log(selectedType);

        // pass "Error" message from characterize along as error for Play Cards, then skip code to play hand



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

    public string characterize(List<GameObject> listOfCards)
    {

        List<int> theseCardValues = getCardValues(listOfCards);
        List<string> theseCardSuits = getCardSuits(listOfCards);

        string result = "";

        if (listOfCards.Count == 1) 
            {result = "single";}
        else if (listOfCards.Count == 3 && 
            theseCardValues[0] == 4 && 
            theseCardValues[1] == 4 && 
            theseCardValues[2] == 14)
            {result = "4-4-Ace";}
        else if (listOfCards.Count == 2 && 
            theseCardValues[0] == 10 &&
            isAllSame(theseCardValues) && 
            theseCardSuits.Contains("diamonds") &&
            theseCardSuits.Contains("hearts"))
            {result = "red10s";}
        else if (listOfCards.Count == 2 && 
            theseCardValues[0] == 10 &&
            isAllSame(theseCardValues) && 
            theseCardSuits.Contains("clubs") &&
            theseCardSuits.Contains("spades"))
            {result = "black10s";}
        else if (listOfCards.Count == 2 && 
            theseCardValues[0] == 16 &&
            isAllSame(theseCardValues)) 
            {result = "doublejoker";}
        else if (listOfCards.Count == 2 && isAllSame(theseCardValues)) 
            {result = "double";}
        else if (listOfCards.Count == 3 && isAllSame(theseCardValues))
            {result = "triple";}
        else if (listOfCards.Count == 4 && isAllSame(theseCardValues))
            {result = "quad";}
        else if (listOfCards.Count == 3 && isStraight(theseCardValues))
            {result = "S3";}
        else if (listOfCards.Count == 4 && isStraight(theseCardValues))
            {result = "S4";}
        else if (listOfCards.Count == 5 && isStraight(theseCardValues))
            {result = "S5";}
        else if (listOfCards.Count == 6 && isStraight(theseCardValues))
            {result = "S6";}
        else if (listOfCards.Count == 7 && isStraight(theseCardValues))
            {result = "S7";}
        else if (listOfCards.Count == 8 && isStraight(theseCardValues))
            {result = "S8";}
        else if (listOfCards.Count == 9 && isStraight(theseCardValues))
            {result = "S9";}
        else if (listOfCards.Count == 10 && isStraight(theseCardValues))
            {result = "S10";}
        else if (listOfCards.Count == 11 && isStraight(theseCardValues))
            {result = "S11";}
        else if (listOfCards.Count == 12 && isStraight(theseCardValues))
            {result = "S12";}
        else if (listOfCards.Count == 6 && isStraightOfDoubles(theseCardValues))
            {result = "SD6";}
        else if (listOfCards.Count == 8 && isStraightOfDoubles(theseCardValues))
            {result = "SD8";}
        else if (listOfCards.Count == 10 && isStraightOfDoubles(theseCardValues))
            {result = "SD10";}
        else if (listOfCards.Count == 12 && isStraightOfDoubles(theseCardValues))
            {result = "SD12";}
        else if (listOfCards.Count == 14 && isStraightOfDoubles(theseCardValues))
            {result = "SD14";}
        else if (listOfCards.Count == 16 && isStraightOfDoubles(theseCardValues))
            {result = "SD16";}
        else if (listOfCards.Count == 18 && isStraightOfDoubles(theseCardValues))
            {result = "SD18";}

        else
        {
            result = "Error: Invalid Hand";
        }

        return result;

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

    public List<string> getCardSuits(List<GameObject> listOfCards)
    {
        
        List<string> cardSuits = new List<string>();

        foreach (GameObject card in listOfCards)
        {
            cardSuits.Add(card.GetComponent<GetValueSuit>().returnSuit());
        }

        return cardSuits;

    }   

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

    public bool isStraight(List<int> listOfCardValues)
    {
        return isConsecutive(listOfCardValues) && !listOfCardValues.Contains(15) ? true : false;
    }

    public bool isStraightOfDoubles(List<int> listOfCardValues)
    {
        if (listOfCardValues.Count < 6 || listOfCardValues.Count % 2 == 1)
        {
            return false;
        }

        List<int> oddSDcards = new List<int>();
        List<int> evenSDcards = new List<int>();

        for (int i = 0; i < listOfCardValues.Count; i += 2)
        {
            oddSDcards.Add(listOfCardValues[i]);
        }
        for (int i = 1; i < listOfCardValues.Count; i += 2)
        {
            evenSDcards.Add(listOfCardValues[i]);
        }

        return isStraight(oddSDcards) && 
               isStraight(evenSDcards) && 
               oddSDcards[0] == evenSDcards[0]
               ? true : false;
    }


    // void Update()
    // {
    //     float speed = 0.1f;
    //     float step = speed * Time.deltaTime;
    //     transform.position = Vector2.MoveTowards(new Vector2(transform.position.x,transform.position.y), new Vector2(xposition,yposition), step);
    // }

}
