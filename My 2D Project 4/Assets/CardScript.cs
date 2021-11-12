using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class CardScript : MonoBehaviour
{

    // Dictionary of 'CardScript' objects and their position before moving
    public static Vector2 previousPosition;
    public static Dictionary<GameObject,Vector2> selectedCardPrevPos = new Dictionary<GameObject,Vector2>(); 

    // Lists of cards
    public static List<GameObject> tableCardObjects = new List<GameObject>();
    public static List<GameObject> selectedCardObjects = new List<GameObject>();   

    public static char[] sortArray = new char[15] {'3','4','5','6','7','8','9','1','J','Q','K','A','2','B','R'};
    public static float selectedYposition = -95.0f;
    public static List<List<GameObject>> handsCS = S10Controller2.hands;

    public static Dictionary<string,List<string>> typesThatBeatMe = new Dictionary<string,List<string>>()
    {
        {"empty",new List<string>() {"single","double","S3","S4","S5","S6","S7","S8","S9","S10","S11","S12","D6","D8","D10","D12","D14","D16","D18","triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"single",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"double",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"S3",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"S4",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"S5",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"S6",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"S7",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"S8",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"S9",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"S10",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"S11",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"S12",new List<string>() {"triple","4-4-Ace","doublejoker","quad","red10s"}},
        {"D6",new List<string>() {"quad","red10s"}},
        {"D8",new List<string>() {"quad","red10s"}},
        {"D10",new List<string>() {"quad","red10s"}},
        {"D12",new List<string>() {"quad","red10s"}},
        {"D14",new List<string>() {"quad","red10s"}},
        {"D16",new List<string>() {"quad","red10s"}},
        {"D18",new List<string>() {"quad","red10s"}},
        {"triple",new List<string>() {"4-4-Ace","doublejoker","quad","red10s"}},
        {"4-4-Ace",new List<string>() {"doublejoker","quad","red10s"}},
        {"doublejoker",new List<string>() {"quad","red10s"}},
        {"quad",new List<string>() {"red10s"}},
        {"red10s",new List<string>() {"black10sONred10s"}},
        {"black10sONred10s",new List<string>() {"4-4-Ace","quad"}}
    };

    public static string tableType = "empty";
    public static string playedType = "empty";

    public class player 
    {
        public List<GameObject> cards { get; set; }
        public List<List<GameObject>> legalHands { get; set; }
        public int seat { get; set; }
    }

    public static player player1 = new player(){cards = handsCS[0],seat = 1};
    //public static player player2 = new player(){cards = handsCS[1],legalHands = getLegalHands(handsCS[1]),seat = 2};
    //public static player player3 = new player(){cards = handsCS[2],legalHands = getLegalHands(handsCS[2]),seat = 3};
    //public static player player4 = new player(){cards = handsCS[3],legalHands = getLegalHands(handsCS[3]),seat = 4};


    //public static int playerTurn = 1;

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

    public static void ClearSelection()
    {

        foreach (KeyValuePair<GameObject,Vector2> entry in selectedCardPrevPos)
        {
            entry.Key.transform.position = entry.Value;
        }

        selectedCardPrevPos.Clear();
        selectedCardObjects.Clear();

    }

    public static void Reposition(List<GameObject> listOfCards, float increment, float yposition)
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

    public static void PlayCards() // Add inputs for tableCardObjects and selectedCardObjects
    {
        
        string comparisonResult = compare(selectedCardObjects,tableCardObjects);

        if (comparisonResult == "win")
        {
            foreach (GameObject card in tableCardObjects)
            {
                Destroy(card);
            }

            foreach (GameObject card in selectedCardObjects)
            {
                handsCS[0].Remove(card);
                Destroy(card.GetComponent<Collider2D>());
            }

            Reposition(selectedCardObjects,-1.0f,92.0f);
            Reposition(handsCS[0],47.0f,-205.0f); 

            tableType = playedType;
            tableCardObjects.Clear();
            tableCardObjects.AddRange(selectedCardObjects);
            selectedCardPrevPos.Clear();
            selectedCardObjects.Clear();
            displayError("");
                  
            // Wait for 5 seconds

            // getLegalHands(handsCS[1], 0);
            // var rand = new System.Random();
            // player2CardObjects = comboList[rand.Next(comboList.Count)];
            // Reposition(player2CardObjects,-1.0f,92.0f);
            // reset comboList, temporarycomboarray, temporarycombo

            // turn repeatable computer player moves into a loop

        }
        else
        {
            displayError(comparisonResult);
        }


    }

    public static void PlayCardsComputer(player thisPlayer)
    {
        System.Threading.Thread.Sleep(2000);
        var rand = new System.Random();
        Reposition(thisPlayer.legalHands[rand.Next(thisPlayer.legalHands.Count)],-1.0f,92.0f);
    }

    public static string compare(List<GameObject> playedCards,List<GameObject> tableCards)
    {
        playedType = characterize(playedCards);
        List<int> playedCardValues = getCardValues(playedCards);
        List<int> tableCardValues = getCardValues(tableCards);

        if (playedType == "Invalid")
        {
            return "Invalid hand";
        }

        if (playedType == "empty")
        {
            return "No cards selected";
        }

        if (playedType == "black10s")
        {
            if (tableType == "red10s")
            {
                playedType = "black10sONred10s";
            }
            else
            {
                playedType = "double";
            }
        }

        if (playedType == tableType)
        {
            if (playedCardValues[0] > tableCardValues[0])
            {
                return "win";
            }
            else
            {
                return "Not strong enough";
            }
        }
        else if (typesThatBeatMe[tableType].Contains(playedType))
        {
            return "win";
        }
        else if (typesThatBeatMe[playedType].Contains(tableType))
        {
            return "Not strong enough";
        }
        else
        {
            return "Incompatible hand";
        }

    }

    public static string characterize(List<GameObject> listOfCards)
    {

        List<int> theseCardValues = getCardValues(listOfCards);
        List<string> theseCardSuits = getCardSuits(listOfCards);

        string result = "";

        if (listOfCards.Count == 0) 
            {result = "empty";}
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
        else if (listOfCards.Count == 1) 
            {result = "single";}
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
            {result = "D6";}
        else if (listOfCards.Count == 8 && isStraightOfDoubles(theseCardValues))
            {result = "D8";}
        else if (listOfCards.Count == 10 && isStraightOfDoubles(theseCardValues))
            {result = "D10";}
        else if (listOfCards.Count == 12 && isStraightOfDoubles(theseCardValues))
            {result = "D12";}
        else if (listOfCards.Count == 14 && isStraightOfDoubles(theseCardValues))
            {result = "D14";}
        else if (listOfCards.Count == 16 && isStraightOfDoubles(theseCardValues))
            {result = "D16";}
        else if (listOfCards.Count == 18 && isStraightOfDoubles(theseCardValues))
            {result = "D18";}

        else
        {
            result = "Invalid";
        }

        return result;

    }

    public static List<int> getCardValues(List<GameObject> listOfCards)
    {
        
        List<int> cardValues = new List<int>();

        foreach (GameObject card in listOfCards)
        {
            cardValues.Add(card.GetComponent<GetValueSuit>().returnValue());
        }

        return cardValues;

    }

    public static List<string> getCardSuits(List<GameObject> listOfCards)
    {
        
        List<string> cardSuits = new List<string>();

        foreach (GameObject card in listOfCards)
        {
            cardSuits.Add(card.GetComponent<GetValueSuit>().returnSuit());
        }

        return cardSuits;

    }   

    public static bool isConsecutive(List<int> listOfCardValues)
    {
        for (int i = 0; i < listOfCardValues.Count - 1; i++)
        {
            if (listOfCardValues[i] + 1 != listOfCardValues[i+1])
            {
                return false;
            }
        }

        return true;

    }

    public static bool isAllSame(List<int> listOfCardValues)
    {
        for (int i = 0; i < listOfCardValues.Count - 1; i++)
        {
            if (listOfCardValues[i] != listOfCardValues[i+1])
            {
                return false;
            }
        }

        return true;
    }

    public static bool isStraight(List<int> listOfCardValues)
    {
        return isConsecutive(listOfCardValues) && !listOfCardValues.Contains(15);
    }

    public static bool isStraightOfDoubles(List<int> listOfCardValues)
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
               oddSDcards[0] == evenSDcards[0];
    }

    public static void displayError(string message)
    {
        GameObject.Find("ErrorText").GetComponent<UnityEngine.UI.Text>().text = message;
    }

    public static List<List<GameObject>> getLegalHands(List<GameObject> listOfCards)
    {

        GameObject[] temporaryComboArray = new GameObject[18];
        List<GameObject> temporaryCombo = new List<GameObject>();
        List<List<GameObject>> comboList = new List<List<GameObject>>();

        void getLegalHandsInner(List<GameObject> listOfCardsInner, int k)
        {
            // K should start at 0, the position in the list of cards. 
            // For each K, either change temporaryCardArray[k] to null (i=0) or listOfCardsInner[k] (i=1)
            // Call the function again for the next item in the list and repeat
            // the function will stop once k reaches the max, then go back through each combination

            if (k == listOfCardsInner.Count)
            {   
                temporaryCombo.Clear();
                temporaryCombo.AddRange(temporaryComboArray);
                temporaryCombo.RemoveAll(x => x == null);

                if (compare(temporaryCombo,tableCardObjects) == "win")
                {
                    comboList.Add(new List<GameObject>());
                    comboList[comboList.Count-1].AddRange(temporaryCombo);
                }

                return;
            }

            for (int i = 0; i < 2; i++)
            {
                temporaryComboArray[k] = (i == 1 ? listOfCardsInner[k] : null);
                getLegalHandsInner(listOfCardsInner,k+1);
            }

        }

        getLegalHandsInner(listOfCards,0);
        return comboList;

    }
    
    public static void testMethod()
    {

        Debug.Log(intlist2string(getCardValues(handsCS[0])));
        //PlayCardsComputer(player1);

    }

    public static string intlist2string(List<int> listOfIntegers)
    {
        string result = "";
        foreach(int num in listOfIntegers)
        {
            result += (num.ToString() + " ");
        }
        return result;
    }

        // foreach(List<GameObject> play in getLegalHands(handsCS[1]))
        // {
        //     Debug.Log(intlist2string(getCardValues(play)));
        // }    

    // foreach(List<GameObject> play in comboList)
    // {
    //     Debug.Log(intlist2string(getCardValues(play)));
    // }

    // Debug.Log(intlist2string(getCardValues(temporaryCombo)));

    // foreach (GameObject card in temporaryComboArray )
    // {
    //     if (card != null)
    //     {
    //         Debug.Log(card.GetComponent<GetValueSuit>().returnValue()+" Combo Number:"+numberOfCombos+" Index:"+Array.IndexOf(temporaryComboArray, card)+" k="+k);
    //     }
            
    // }

    // numberOfCombos++;    

    // printcombos(new int[3]{3,4,5},0);

    // public static void printcombos(int[] combo, int k) 
    // {

    //     // print each index that stores a nonzero value?
    //     foreach (int num in combo)
    //     {

    //         //Debug.Log(num+" "+k+" "+combo.Length);

    //     }

    //     // Determine the smallest item that can go in slot k.
    //     int start = 0;
    //     if (k > 0) 
    //     {
    //         start = combo[k-1] + 1;
    //     }
    //     // Same as odometer, except a different start value.
    //     for (int i = start; i < combo.Length; i++) 
    //     {
    //     combo[k] = i;
    //     printcombos(combo, k+1);
    //     }
    // }


    // public static void odometer(int[] digits, int numDigits, int k) 
    // {
    //     if (k == digits.Length) 
    //     {
    //         Debug.Log(digits);
    //         return;
    //     }
    //     for (int i = 0; i < numDigits; i++) 
    //     {
    //         digits[k] = i;
    //         odometer(digits, k+1, k);
    //     }
    // }


    // void Update()
    // {
    //     float speed = 0.1f;
    //     float step = speed * Time.deltaTime;
    //     transform.position = Vector2.MoveTowards(new Vector2(transform.position.x,transform.position.y), new Vector2(xposition,yposition), step);
    // }

}
