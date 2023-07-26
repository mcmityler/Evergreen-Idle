using UnityEngine;

//tree plot class

[System.Serializable]
public class TreePlotClass
{
    public string plotName;
    public GameObject plot;
   
    public bool plotOwned = false;
    public int plotCost = 10;
    public Color32 ownedColour;
    public Color32 unownedColour;

    [HideInInspector]
    public bool isEmpty = true;
}
