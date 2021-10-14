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

    [SerializeField] private GameObject SA;
    [SerializeField] private GameObject S2;
    [SerializeField] private GameObject S3;
    [SerializeField] private GameObject S4;
    [SerializeField] private GameObject S5;
    [SerializeField] private GameObject S6;
    [SerializeField] private GameObject S7;
    [SerializeField] private GameObject S8;
    [SerializeField] private GameObject S9;
    [SerializeField] private GameObject S10;
    [SerializeField] private GameObject SJ;
    [SerializeField] private GameObject SQ;
    [SerializeField] private GameObject SK;
    [SerializeField] private GameObject HA;
    [SerializeField] private GameObject H2;
    [SerializeField] private GameObject H3;
    [SerializeField] private GameObject H4;
    [SerializeField] private GameObject H5;
    [SerializeField] private GameObject H6;
    [SerializeField] private GameObject H7;
    [SerializeField] private GameObject H8;
    [SerializeField] private GameObject H9;
    [SerializeField] private GameObject H10;
    [SerializeField] private GameObject HJ;
    [SerializeField] private GameObject HQ;
    [SerializeField] private GameObject HK;
    [SerializeField] private GameObject CA;
    [SerializeField] private GameObject C2;
    [SerializeField] private GameObject C3;
    [SerializeField] private GameObject C4;
    [SerializeField] private GameObject C5;
    [SerializeField] private GameObject C6;
    [SerializeField] private GameObject C7;
    [SerializeField] private GameObject C8;
    [SerializeField] private GameObject C9;
    [SerializeField] private GameObject C10;
    [SerializeField] private GameObject CJ;
    [SerializeField] private GameObject CQ;
    [SerializeField] private GameObject CK;
    [SerializeField] private GameObject DA;
    [SerializeField] private GameObject D2;
    [SerializeField] private GameObject D3;
    [SerializeField] private GameObject D4;
    [SerializeField] private GameObject D5;
    [SerializeField] private GameObject D6;
    [SerializeField] private GameObject D7;
    [SerializeField] private GameObject D8;
    [SerializeField] private GameObject D9;
    [SerializeField] private GameObject D10;
    [SerializeField] private GameObject DJ;
    [SerializeField] private GameObject DQ;
    [SerializeField] private GameObject DK;
    [SerializeField] private GameObject RJ;
    [SerializeField] private GameObject BJ;

    [SerializeField] private GameObject selectedCards;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player3;
    [SerializeField] private GameObject player4;


    class card
    {
        public string val { get; set; }
        public string suit { get; set; }
        public int index { get; set; }
        public string GameObjectName { get; set; }
    }

    public void Start()
    {
        StartPage.SetActive(true);
    }

    public void StartGame()
    {
        StartPage.SetActive(false);
        
        // Define deck
        List<card> deck = new List<card> {
            new card {val = "2", suit = "clubs", index = 0, GameObjectName = "2ofclubs"},
            new card {val = "3", suit = "clubs", index = 1, GameObjectName = "3ofclubs"},
            new card {val = "4", suit = "clubs", index = 2, GameObjectName = "4ofclubs"},
            new card {val = "5", suit = "clubs", index = 3, GameObjectName = "5ofclubs"},
            new card {val = "6", suit = "clubs", index = 4, GameObjectName = "6ofclubs"},
            new card {val = "7", suit = "clubs", index = 5, GameObjectName = "7ofclubs"},
            new card {val = "8", suit = "clubs", index = 6, GameObjectName = "8ofclubs"},
            new card {val = "9", suit = "clubs", index = 7, GameObjectName = "9ofclubs"},
            new card {val = "10", suit = "clubs", index = 8, GameObjectName = "10ofclubs"},
            new card {val = "J", suit = "clubs", index = 9, GameObjectName = "Jackofclubs"},
            new card {val = "Q", suit = "clubs", index = 10, GameObjectName = "Queenofclubs"},
            new card {val = "K", suit = "clubs", index = 11, GameObjectName = "Kingofclubs"},
            new card {val = "A", suit = "clubs", index = 12, GameObjectName = "Aceofclubs"},

            new card {val = "2", suit = "diamonds", index = 13, GameObjectName = "2ofdiamonds"},
            new card {val = "3", suit = "diamonds", index = 14, GameObjectName = "3ofdiamonds"},
            new card {val = "4", suit = "diamonds", index = 15, GameObjectName = "4ofdiamonds"},
            new card {val = "5", suit = "diamonds", index = 16, GameObjectName = "5ofdiamonds"},
            new card {val = "6", suit = "diamonds", index = 17, GameObjectName = "6ofdiamonds"},
            new card {val = "7", suit = "diamonds", index = 18, GameObjectName = "7ofdiamonds"},
            new card {val = "8", suit = "diamonds", index = 19, GameObjectName = "8ofdiamonds"},
            new card {val = "9", suit = "diamonds", index = 20, GameObjectName = "9ofdiamonds"},
            new card {val = "10", suit = "diamonds", index = 21, GameObjectName = "10ofdiamonds"},
            new card {val = "J", suit = "diamonds", index = 22, GameObjectName = "Jackofdiamonds"},
            new card {val = "Q", suit = "diamonds", index = 23, GameObjectName = "Queenofdiamonds"},
            new card {val = "K", suit = "diamonds", index = 24, GameObjectName = "Kingofdiamonds"},
            new card {val = "A", suit = "diamonds", index = 25, GameObjectName = "Aceofdiamonds"},

            new card {val = "2", suit = "hearts", index = 26, GameObjectName = "2ofhearts"},
            new card {val = "3", suit = "hearts", index = 27, GameObjectName = "3ofhearts"},
            new card {val = "4", suit = "hearts", index = 28, GameObjectName = "4ofhearts"},
            new card {val = "5", suit = "hearts", index = 29, GameObjectName = "5ofhearts"},
            new card {val = "6", suit = "hearts", index = 30, GameObjectName = "6ofhearts"},
            new card {val = "7", suit = "hearts", index = 31, GameObjectName = "7ofhearts"},
            new card {val = "8", suit = "hearts", index = 32, GameObjectName = "8ofhearts"},
            new card {val = "9", suit = "hearts", index = 33, GameObjectName = "9ofhearts"},
            new card {val = "10", suit = "hearts", index = 34, GameObjectName = "10ofhearts"},
            new card {val = "J", suit = "hearts", index = 35, GameObjectName = "Jackofhearts"},
            new card {val = "Q", suit = "hearts", index = 36, GameObjectName = "Queenofhearts"},
            new card {val = "K", suit = "hearts", index = 37, GameObjectName = "Kingofhearts"},
            new card {val = "A", suit = "hearts", index = 38, GameObjectName = "Aceofhearts"},

            new card {val = "2", suit = "spades", index = 39, GameObjectName = "2ofspades"},
            new card {val = "3", suit = "spades", index = 40, GameObjectName = "3ofspades"},
            new card {val = "4", suit = "spades", index = 41, GameObjectName = "4ofspades"},
            new card {val = "5", suit = "spades", index = 42, GameObjectName = "5ofspades"},
            new card {val = "6", suit = "spades", index = 43, GameObjectName = "6ofspades"},
            new card {val = "7", suit = "spades", index = 44, GameObjectName = "7ofspades"},
            new card {val = "8", suit = "spades", index = 45, GameObjectName = "8ofspades"},
            new card {val = "9", suit = "spades", index = 46, GameObjectName = "9ofspades"},
            new card {val = "10", suit = "spades", index = 47, GameObjectName = "10ofspades"},
            new card {val = "J", suit = "spades", index = 48, GameObjectName = "Jackofspades"},
            new card {val = "Q", suit = "spades", index = 49, GameObjectName = "Queenofspades"},
            new card {val = "K", suit = "spades", index = 50, GameObjectName = "Kingofspades"},
            new card {val = "A", suit = "spades", index = 51, GameObjectName = "Aceofspades"}
        };

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
        List<List<card>> hands = new List<List<card>>();
        
        for (int i = 0; i < numPlayers; i++) // Creates a list of empty lists, one for each player
        {
            hands.Add(new List<card> {});
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
        string[] sortArray = new string[14] {"3","4","5","6","7","8","9","10","J","Q","K","A","2","Joker"};
        
        int[] xPosArray = new int[4] {0,0,0,0};
        int[] yPosArray = new int[4] {-205,475,400,325};

        for (int h = 0; h < hands.Count; h++)
        {
            hands[h].Sort((x,y) => Array.IndexOf(sortArray,x.val).CompareTo(Array.IndexOf(sortArray,y.val)));

            float increment = 52.0f;
            float cardSpread = increment*(hands[h].Count - 1); // The -1 is because there are one less spaces than cards
            float xposition = cardSpread*(-0.5f) + xPosArray[h];
            float yposition = yPosArray[h];

            for (int i = 0; i < hands[h].Count; i++)
            {
                GameObject.Find(hands[h][i].GameObjectName).transform.position = new Vector2(xposition,yposition);
                xposition += increment;
            }
        }
     
    }

    

    public void ExitGame()
    {
        StartPage.SetActive(true);
        // Potentially more stuff here to reset things if quit mid game
    }

}
