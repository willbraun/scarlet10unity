using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class S10Controller2 : MonoBehaviour
{

    [SerializeField] private GameObject StartPage; 
    [SerializeField] private GameObject StartGameButton;
    [SerializeField] private GameObject ExitGameButton;

    public static List<List<GameObject>> hands = new List<List<GameObject>>();
    public static List<GameObject> tableCardObjects = new List<GameObject>();
    public static List<GameObject> selectedCardObjects = new List<GameObject>();   
    public static float selectedYposition = -95.0f;
    public static int numPlayers = 4;
    public static int playerTurn = 0;
    public static int passCount = 0;
    public static string tableType = "empty";
    public static string playedType = "empty";

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

    public class player 
    {  
        public List<GameObject> cards {get; set;}
        public int seat {get; set;}
    }

    public static player[] players = new player[numPlayers];

    public void StartGame()
    {
        StartPage.SetActive(false);
        
        // Define deck
        var deckArray = GameObject.FindGameObjectsWithTag("Card");
        List<GameObject> deck = new List<GameObject>(deckArray);
        deck.Remove(GameObject.Find("Redjoker"));
        deck.Remove(GameObject.Find("Blackjoker"));

        // Shuffle deck - for 1000 turns, switch the values of two random cards
        int deckSize = deck.Count;
        var rand = new System.Random();
        
        for (int i = 0; i < 1000; i++)
        {
            int location1 = rand.Next(deckSize);
            int location2 = rand.Next(deckSize);
            var temp = deck[location1];

            deck[location1] = deck[location2];
            deck[location2] = temp;
        }

        // Deal cards evenly
        for (int i = 0; i < numPlayers; i++) // Creates a list of empty lists, one for each player
        {
            hands.Add(new List<GameObject> {});
        }

        int n = 0;
   
        for (int i = 0; i < deckSize; i++) // Adds cards to each player's empty list one by one
        {
            hands[n].Add(deck[i]);
            n++;
            if (n == numPlayers)
            {
                n = 0;
            }
        }

        // Display all player cards (players 2-4 outside viewing area)
        int[] yPosArray = new int[4] {-205,475,400,325};
        int yPosArrayIndex = 0;

        foreach (List<GameObject> hand in hands)
        {
            Reposition(hand,47.0f,yPosArray[yPosArrayIndex]);
            yPosArrayIndex += 1;
        }

        // Create player objects
        for(int i = 0; i < numPlayers; i++)
        {
            players[i] = new player();
            players[i].cards = hands[i];
            players[i].seat = i;
        }
     
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

    public static void PlayCards()
    {
        string comparisonResult = Compare(selectedCardObjects,tableCardObjects);

        if (comparisonResult == "win")
        {
            foreach (GameObject card in tableCardObjects)
            {
                Destroy(card);
            }

            foreach (GameObject card in selectedCardObjects)
            {
                players[0].cards.Remove(card);
                Destroy(card.GetComponent<Collider2D>());
            }

            Reposition(selectedCardObjects,-1.0f,92.0f);
            Reposition(players[0].cards,47.0f,-205.0f); 

            tableType = playedType;
            tableCardObjects.Clear();
            tableCardObjects.AddRange(selectedCardObjects);
            CardScript.selectedCardPrevPos.Clear();
            selectedCardObjects.Clear();
            passCount = 0;
            DisplayError("");
            Rotate();
        }
        else 
        {
            DisplayError(comparisonResult);
        }
        // You can't select Pass object, will never appear here. Human must pass with button.
    }

    public static void Pass()
    {
        passCount++;
        Rotate();
        // Add call to Rotate function
    }

    public static void PlayCardsComputer(player thisPlayer)
    {
        // Update hands that are legal since the table has changed, choose play
        List<List<GameObject>> legalHands = GetLegalHands(thisPlayer.cards);
        
        // list legal hands in debug
        // foreach (List<GameObject> LH in legalHands)
        // {
        //     Debug.Log(intlist2string(GetCardValues(LH)));
        // }

        var rand = new System.Random();
        List<GameObject> computerPlay = legalHands[rand.Next(legalHands.Count-1)];
        string computerPlayType = Characterize(computerPlay);

        if (computerPlayType != "passType")
        {
            foreach (GameObject card in tableCardObjects)
            {
                Destroy(card);
            }

            foreach (GameObject card in computerPlay)
            {
                thisPlayer.cards.Remove(card);
                Destroy(card.GetComponent<Collider2D>());
            }

            Reposition(computerPlay,-1.0f,92.0f);
            tableType = computerPlayType;
            tableCardObjects.Clear();
            tableCardObjects.AddRange(computerPlay);
            passCount = 0;
            DisplayError("");
        }
        else
        {
            passCount++;
        }
    }

    public static void Rotate()
    {
        
        playerTurn++;

        if (playerTurn == 4)
        {
            playerTurn = 0;
            return;
        }
        else
        {
            //Thread.sleep? Wait some time?

            Debug.Log("before "+ tableType);
            PlayCardsComputer(players[playerTurn]);
            Debug.Log("after "+ tableType);

            if (passCount == 3)
            {
                tableType = "empty";
                tableCardObjects.Clear();
                passCount = 0;
            }

            Debug.Log("pass "+ passCount.ToString());
            Debug.Log("turn "+ playerTurn.ToString());
            Rotate();
        }
    }

    public static List<List<GameObject>> GetLegalHands(List<GameObject> listOfCards)
    {

        GameObject[] temporaryComboArray = new GameObject[18];
        List<GameObject> temporaryCombo = new List<GameObject>();
        List<List<GameObject>> comboList = new List<List<GameObject>>();

        void GetLegalHandsInner(List<GameObject> listOfCardsInner, int k)
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

                if (Compare(temporaryCombo,tableCardObjects) == "win")
                {
                    comboList.Add(new List<GameObject>());
                    comboList[comboList.Count-1].AddRange(temporaryCombo);
                }

                return;
            }

            for (int i = 0; i < 2; i++)
            {
                temporaryComboArray[k] = (i == 1 ? listOfCardsInner[k] : null);
                GetLegalHandsInner(listOfCardsInner,k+1);
            }

        }

        GetLegalHandsInner(listOfCards,0);
        comboList.Add(new List<GameObject>(){GameObject.Find("PassPlaceholder")});
        return comboList;

    }

    public static string Compare(List<GameObject> playedCards,List<GameObject> tableCards)
    {
        playedType = Characterize(playedCards);
        List<int> playedCardValues = GetCardValues(playedCards);
        List<int> tableCardValues = GetCardValues(tableCards);

        if (playedType == "Invalid")
        {
            return "Invalid hand";
        }

        if (playedType == "empty")
        {
            return "No cards selected";
        }      

        if (playedType == "passType")
        {
            return "pass";
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

    public static string Characterize(List<GameObject> listOfCards)
    {
        List<int> theseCardValues = GetCardValues(listOfCards);
        List<string> theseCardSuits = GetCardSuits(listOfCards);

        string result = "";

        if (listOfCards.Count == 1 && theseCardSuits[0] == "passSuit")
            {result = "passType";}
        else if (listOfCards.Count == 0) 
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

    public static List<int> GetCardValues(List<GameObject> listOfCards)
    {
        List<int> cardValues = new List<int>();

        foreach (GameObject card in listOfCards)
        {
            cardValues.Add(card.GetComponent<GetValueSuit>().returnValue());
        }

        return cardValues;
    }

    public static List<string> GetCardSuits(List<GameObject> listOfCards)
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

    public static void DisplayError(string message)
    {
        GameObject.Find("ErrorText").GetComponent<UnityEngine.UI.Text>().text = message;
    }


    
    public static void testMethod()
    {
        PlayCardsComputer(players[1]);
        // foreach (List<GameObject> LH in players[1].legalHands)
        // {
        //     Debug.Log(intlist2string(GetCardValues(LH)));
        // }
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

    public void Start()
    {
        StartPage.SetActive(true);
    }

    public void ExitGame()
    {
        StartPage.SetActive(true);
        // Potentially more stuff here to reset things if quit mid game
    }

}
