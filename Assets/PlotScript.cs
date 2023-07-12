using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlotScript : MonoBehaviour
{
    [SerializeField] bool _plotOwned = true;
    [SerializeField] int _plotCost = 5;
    [SerializeField] GameObject _plotCostCanvas;
    [SerializeField] Color32 _unownedColour;
    // Start is called before the first frame update
    void Start()
    {
        if (_plotOwned == false) //if its not an owned plot make the colour different and make the cost text visable
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = _unownedColour;
            _plotCostCanvas.SetActive(true);
            _plotCostCanvas.GetComponentInChildren<TMP_Text>().text = _plotCost.ToString();

        }
    }
    private void OnMouseDown()
    {
        if (_plotOwned == false) //Purchase Plot
        {
            _plotOwned = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().PurchasePlot(this.gameObject, _plotCost);
            if(_plotOwned == true)
            {
                _plotCostCanvas.SetActive(false);
                //spawn purchase /spent particles
            }
        }
      
    }

    // Update is called once per frame
    void Update()
    {

    }
}
