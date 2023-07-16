/*
Created by:  Tyler McMillan
Description: This script deals with the trees and changing their states / holding their info.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    GameObject _axe;


    [SerializeField] int _treehealth = 100;
    [SerializeField] Sprite[] _treeSprites;
    int _maxHealth = 0;
    [SerializeField] int _woodPerHit = 1;
    int _plotNum = -1;
    [SerializeField] GameObject _hitParticles;
    [SerializeField] GameObject _treeFallPrefab;
    bool _treeFell = false;

    // Start is called before the first frame update
    void Start()
    {
        _axe = GameObject.FindGameObjectWithTag("Axe");
        _maxHealth = _treehealth;
    }
    private void OnMouseOver()
    {
        _axe.GetComponent<AxeScript>().ChangeSwing(true);
        _axe.GetComponent<AxeScript>().SetCurrentTree(this.gameObject);
    }
    private void OnMouseExit()
    {
        _axe.GetComponent<AxeScript>().ChangeSwing(false);
        _axe.GetComponent<AxeScript>().SetCurrentTree(null);

    }
    public void SetPlotNum(int m_plotNum)
    {
        _plotNum = m_plotNum;
    }
    public GameObject GetTreeHitParticles()
    {
        return _hitParticles;
    }
    public void DamageTree(int m_dmg)
    {
        _treehealth -= m_dmg;
        ChangeTreeState();
    }
    
    void ChangeTreeState() //change image of tree depending on health amount
    {
        if (_treehealth <= 0)
        {
            _axe.GetComponent<AxeScript>().ChangeSwing(false);
            _axe.GetComponent<AxeScript>().SetCurrentTree(null);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().WoodGathered(_woodPerHit, true, _plotNum);
            Destroy(this.gameObject);
            return;
        }
        else if (_treehealth <= _maxHealth * 0.2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = _treeSprites[3];
            
            if(_treeFell == false)//make tree timber here
            {
                Instantiate(_treeFallPrefab, this.gameObject.transform.position, Quaternion.identity);
                //play tree fall sfx here

                _treeFell = true;
            }
        }
        else if (_treehealth <= _maxHealth * 0.4)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = _treeSprites[2];

        }
        else if (_treehealth <= _maxHealth * 0.6)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = _treeSprites[1];

        }
        else if (_treehealth <= _maxHealth * 0.8)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = _treeSprites[0];
        }
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().WoodGathered(_woodPerHit, false, _plotNum);





    }

}
