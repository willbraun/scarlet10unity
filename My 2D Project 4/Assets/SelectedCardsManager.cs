using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCardsManager : MonoBehaviour
{

    class card
    {
        public string val { get; set; }
        public string suit { get; set; }
        public int index { get; set; }
        public string GameObjectName { get; set; }
    }

    List<card> selectedCards = new List<card>();
   
    //public void AddCard(gameObject here);

    //public void RemoveCard(gameObject here);

}
