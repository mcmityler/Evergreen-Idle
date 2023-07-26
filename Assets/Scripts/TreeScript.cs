/*
Created by:  Tyler McMillan
Description: This script deals with the trees and changing their states / holding their info.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    GameObject _axe; //gameobj with axe script aka player
    [SerializeField] int _treehealth = 100; //how much health a tree has
    [SerializeField] Sprite[] _treeSprites; //different sprites for tree being cut down
    int _maxHealth = 0; //how much health it had when it spawned
    [SerializeField] int _woodPerHit = 1; //how much wood this tree is worth
    int _plotNum = -1; //what plot this tree is grown on
    [SerializeField] GameObject _hitParticles; //particles that spawn when it is hit
    [SerializeField] GameObject _treeFall; //treefall Animation
    bool _treeFell = false; //has the top fallen (used so when hitting stump it doesnt fall more then one time)
    bool _isAutoTree = false; //is this an auto tree farm if so dont allow player to help
    void Start()
    {
        _axe = GameObject.FindGameObjectWithTag("Axe"); //get reference to axe obj 
        _maxHealth = _treehealth; //set max health to what the tree spawned with
    }
    private void OnMouseOver() //check if you are hovering over a tree
    {
        if (!_isAutoTree)
        {

            _axe.GetComponent<AxeScript>().ChangeSwing(true); //tell axe it can swing
            _axe.GetComponent<AxeScript>().SetCurrentTree(this.gameObject); //tell axe what tree you are on
        }
    }
    private void OnMouseExit() //when your cursor leaves tree..
    {
        if (!_isAutoTree)
        {
            _axe.GetComponent<AxeScript>().ChangeSwing(false); //no longer allowed to swing
            _axe.GetComponent<AxeScript>().SetCurrentTree(null); //forget what tree you were on
        }
    }
    public void SetPlotNum(int m_plotNum) //set what plot this tree is grown on when spawned in game manager
    {
        _plotNum = m_plotNum;
    }
    public void DamageTree(int m_dmg) //how much damage this tree takes
    {
        _treehealth -= m_dmg;
        _hitParticles.GetComponent<ParticleSystem>().Play();
        ChangeTreeState(); //change image of tree depending on health amount
    }

    void ChangeTreeState() //change image of tree depending on health amount
    {
        if (_treehealth <= 0) //destroy tree and stop swinging axe 
        {
            _axe.GetComponent<AxeScript>().ChangeSwing(false);
            _axe.GetComponent<AxeScript>().SetCurrentTree(null);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().WoodGathered(_woodPerHit, true, _plotNum); //gather wood
            Destroy(this.gameObject); //destroy tree
            return; //end func
        }
        else if (_treehealth <= _maxHealth * 0.2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = _treeSprites[3];

            if (_treeFell == false) //make sure timber anim & sfx only plays once
            {
                //make tree timber here
                _treeFall.GetComponent<Animator>().SetTrigger("TreeFall");
                //play tree fall sfx here
                FindObjectOfType<AudioManager>().Play("TreeFall");
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
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().WoodGathered(_woodPerHit, false, _plotNum); //collect wood 
    }
    public void SetAutoTree(bool m_isAuto)
    {
        _isAutoTree = m_isAuto;
    }

}
