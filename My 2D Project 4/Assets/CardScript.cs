using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{

    // Dictionary of 'CardScript' objects and their position before moving
    private Vector2 previousPosition;
    private Dictionary<CardScript,Vector2> selectedCards = new Dictionary<CardScript,Vector2>(); 

    void OnMouseDown()
    {

        float xNew = 0f;
        float yNew = -95.0f;

        if (this.transform.position.y != -95.0f)
        {
            // On Select
            previousPosition = this.transform.position;
            this.transform.position = new Vector2(xNew,yNew);
            selectedCards.Add(this,previousPosition);

            Debug.Log("Selected "+ this.name);
        }
        else
        {
            // On Deselect, set position to original position and remove from selected list
            this.transform.position = selectedCards[this];  
            selectedCards.Remove(this);

            Debug.Log("Deselected "+ this.name);
        }

    }


    // class card
    // {
    //     public string val { get; set; }
    //     public string suit { get; set; }
    //     public int index { get; set; }
    //     public string GameObjectName { get; set; }
    // }

    //List<card> selectedCards = new List<card>();



}
