using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    GameObject _axe;
    // Start is called before the first frame update
    void Start()
    {
        _axe = GameObject.FindGameObjectWithTag("Axe");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseOver()
    {
        _axe.GetComponent<AxeScript>().ChangeSwing(true);
    }
    private void OnMouseExit()
    {
        _axe.GetComponent<AxeScript>().ChangeSwing(false);
    }
}
