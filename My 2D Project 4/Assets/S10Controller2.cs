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
        card[] deck = {
            new card {val = "2", suit = "clubs", index = 0, GameObjectName = "C2"},
            new card {val = "3", suit = "clubs", index = 1, GameObjectName = "C3"},
            new card {val = "4", suit = "clubs", index = 2, GameObjectName = "C4"},
            new card {val = "5", suit = "clubs", index = 3, GameObjectName = "C5"},
            new card {val = "6", suit = "clubs", index = 4, GameObjectName = "C6"},
            new card {val = "7", suit = "clubs", index = 5, GameObjectName = "C7"},
            new card {val = "8", suit = "clubs", index = 6, GameObjectName = "C8"},
            new card {val = "9", suit = "clubs", index = 7, GameObjectName = "C9"},
            new card {val = "10", suit = "clubs", index = 8, GameObjectName = "C10"},
            new card {val = "J", suit = "clubs", index = 9, GameObjectName = "CJ"},
            new card {val = "Q", suit = "clubs", index = 10, GameObjectName = "CQ"},
            new card {val = "K", suit = "clubs", index = 11, GameObjectName = "CK"},
            new card {val = "A", suit = "clubs", index = 12, GameObjectName = "CA"},

            new card {val = "2", suit = "diamonds", index = 13, GameObjectName = "D2"},
            new card {val = "3", suit = "diamonds", index = 14, GameObjectName = "D3"},
            new card {val = "4", suit = "diamonds", index = 15, GameObjectName = "D4"},
            new card {val = "5", suit = "diamonds", index = 16, GameObjectName = "D5"},
            new card {val = "6", suit = "diamonds", index = 17, GameObjectName = "6ofDiamonds"},
            new card {val = "7", suit = "diamonds", index = 18, GameObjectName = "7ofDiamonds"},
            new card {val = "8", suit = "diamonds", index = 19, GameObjectName = "8ofDiamonds"},
            new card {val = "9", suit = "diamonds", index = 20, GameObjectName = "9ofDiamonds"},
            new card {val = "10", suit = "diamonds", index = 21, GameObjectName = "10ofDiamonds"},
            new card {val = "J", suit = "diamonds", index = 22, GameObjectName = "JackofDiamonds"},
            new card {val = "Q", suit = "diamonds", index = 23, GameObjectName = "QueenofDiamonds"},
            new card {val = "K", suit = "diamonds", index = 24, GameObjectName = "KingofDiamonds"},
            new card {val = "A", suit = "diamonds", index = 25, GameObjectName = "AceofDiamonds"},

            new card {val = "2", suit = "hearts", index = 26, GameObjectName = "2ofHearts"},
            new card {val = "3", suit = "hearts", index = 27, GameObjectName = "3ofHearts"},
            new card {val = "4", suit = "hearts", index = 28, GameObjectName = "4ofHearts"},
            new card {val = "5", suit = "hearts", index = 29, GameObjectName = "5ofHearts"},
            new card {val = "6", suit = "hearts", index = 30, GameObjectName = "6ofHearts"},
            new card {val = "7", suit = "hearts", index = 31, GameObjectName = "7ofHearts"},
            new card {val = "8", suit = "hearts", index = 32, GameObjectName = "8ofHearts"},
            new card {val = "9", suit = "hearts", index = 33, GameObjectName = "9ofHearts"},
            new card {val = "10", suit = "hearts", index = 34, GameObjectName = "10ofHearts"},
            new card {val = "J", suit = "hearts", index = 35, GameObjectName = "JackofHearts"},
            new card {val = "Q", suit = "hearts", index = 36, GameObjectName = "QueenofHearts"},
            new card {val = "K", suit = "hearts", index = 37, GameObjectName = "KingofHearts"},
            new card {val = "A", suit = "hearts", index = 38, GameObjectName = "AceofHearts"},

            new card {val = "2", suit = "spades", index = 39, GameObjectName = "2ofSpades"},
            new card {val = "3", suit = "spades", index = 40, GameObjectName = "3ofSpades"},
            new card {val = "4", suit = "spades", index = 41, GameObjectName = "4ofSpades"},
            new card {val = "5", suit = "spades", index = 42, GameObjectName = "5ofSpades"},
            new card {val = "6", suit = "spades", index = 43, GameObjectName = "6ofSpades"},
            new card {val = "7", suit = "spades", index = 44, GameObjectName = "7ofSpades"},
            new card {val = "8", suit = "spades", index = 45, GameObjectName = "8ofSpades"},
            new card {val = "9", suit = "spades", index = 46, GameObjectName = "9ofSpades"},
            new card {val = "10", suit = "spades", index = 47, GameObjectName = "10ofSpades"},
            new card {val = "J", suit = "spades", index = 48, GameObjectName = "JackofSpades"},
            new card {val = "Q", suit = "spades", index = 49, GameObjectName = "QueenofSpades"},
            new card {val = "K", suit = "spades", index = 50, GameObjectName = "KingofSpades"},
            new card {val = "A", suit = "spades", index = 51, GameObjectName = "AceofSpades"}
        };

        // Shuffle deck - for 1000 turns, switch the values of two random cards
        var rand = new System.Random();
        
        for (int i = 0; i < 1000; i++)
        {
            int location1 = rand.Next(deck.Length);
            int location2 = rand.Next(deck.Length);
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
   
        for (int i = 0; i < deck.Length; i++) // Adds cards to each player's empty list one by one
        {
            hands[n].Add(deck[i]);
            n++;
            if (n == numPlayers)
            {
                n = 0;
            }
        }

        // Display my cards

        // Set each matching game object to a specific position on the board

        // use GameObject.Find(concatenate first letter of suit, uppercase, with card.val to find game object). 
        // If that works, delete GameObjectName from cards and class




        

    }
    
    

    public void ExitGame()
    {
        StartPage.SetActive(true);
        // Potentially more stuff here to reset things if quit mid game
    }

}
