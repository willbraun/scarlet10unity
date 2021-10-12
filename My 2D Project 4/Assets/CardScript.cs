using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{

    [SerializeField] private SelectedCardsManager selectedCardsManager;

    //List<GameObject> selectedCards = new List<GameObject>();
    //selectedCards.Add GameObjects that are at this Y height

    private Vector2 previousPosition;

    void OnMouseDown()
    {

        float xNew = 0f;
        float yNew = -95.0f;

        if (this.transform.position.y != -95.0f)
        {
            //Saves position, then moves to new position
            previousPosition = this.transform.position;
            this.transform.position = new Vector2(xNew,yNew);

            Debug.Log("Selected "+ this.name);
        }
        else
        {
            // Moves selected card to original position
            this.transform.position = previousPosition;

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
