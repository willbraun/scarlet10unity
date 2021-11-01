using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorHandler : MonoBehaviour
{

    public void displayError(string message)
    {
        this.gameObject.GetComponent<UnityEngine.UI.Text>().text = message;
    }

}
