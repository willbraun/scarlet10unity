using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S10Controller2 : MonoBehaviour
{

    [SerializeField] private GameObject StartPage; 
    [SerializeField] private GameObject StartGameButton;
    [SerializeField] private GameObject MainGame;
    [SerializeField] private GameObject ExitGameButton;

    public static List<List<GameObject>> hands = new List<List<GameObject>>();
    public static char[] sortArray = new char[15] {'3','4','5','6','7','8','9','1','J','Q','K','A','2','B','R'};

    public void Start()
    {
        StartPage.SetActive(true);
    }

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
        int numPlayers = 4;
        
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
            hand.Sort((x,y) => Array.IndexOf(sortArray,x.name[0]).CompareTo(Array.IndexOf(sortArray,y.name[0])));

            float increment = 47.0f;
            float cardSpread = increment*(hand.Count - 1); // The -1 is because there are one less spaces than cards
            float xposition = cardSpread*(-0.5f);
            float yposition = yPosArray[yPosArrayIndex];

            foreach (GameObject card in hand)
            {
                card.transform.position = new Vector2(xposition,yposition);
                xposition += increment;
            }

            yPosArrayIndex += 1;
        }
     
    }

    public void ExitGame()
    {
        StartPage.SetActive(true);
        // Potentially more stuff here to reset things if quit mid game
    }

}
