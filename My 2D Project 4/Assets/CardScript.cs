using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{

    class card
    {
        public string val { get; set; }
        public string suit { get; set; }
        public int index { get; set; }
        public string GameObjectName { get; set; }
    }

    List<card> selectedCards = new List<card>();

    void OnMouseDown()
    {
        Debug.Log("hello");
        // Access deck from other script - watch videos
        // Find card in deck with matching gameobject name 
        // Add card object matching gameObject to the selectedCards List if not selected
        // If card is already in list, remove from list
        // Attach this script to all cards
    }

}
