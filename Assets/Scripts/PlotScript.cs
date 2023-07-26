/*
Created by:  Tyler McMillan
Description: This script deals with the plots and purchasing them
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlotScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().PurchasePlot(this.gameObject);
    }
}
