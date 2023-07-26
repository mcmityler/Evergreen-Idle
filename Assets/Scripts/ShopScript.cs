/*
Created by:  Tyler McMillan
Description: This script deals with the shop screen and its upgrades
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{
    [SerializeField] List<GameObject> _dmgTokenObjs = new List<GameObject>(); //list of damage tokens that display your upgrades you are at on the shop
    [SerializeField] List<int> _dmgUpgradesCost = new List<int>(); //list of dmg upgrades cost (goes up as you purchase)
    [SerializeField] TMP_Text _damageUpgradeCostText; //text that displays dmg upgrade cost
    [SerializeField] int _dmgUpgrades = 0; //how many dmg upgrades you currently have 

    [SerializeField] List<GameObject> _playerSwingSpeedpeedTokenObjs = new List<GameObject>(); //list of player swing speed tokens that display your upgrades you are at on the shop
    [SerializeField] List<int> _playerSwingSpeedUpgradesCost = new List<int>(); //list of player ss upgrade costs (goes up as you buy)
    [SerializeField] List<float> _playerSwingSpeedIncrements = new List<float>(); //how fast each upgrade makes the player swing
    [SerializeField] TMP_Text _playerSwingSpeedUpgradeCostText; //text that displayers how much the swing speed upgrade costs
    [SerializeField] int _playerSwingSpeedUpgrades = 0; //how many player swing speed upgrades have currently been obtained

    GameManagerScript _gmScript; //game manager
    [SerializeField] bool _loadSave = false; //are you loading from a save (aka from your inspectors values)
    bool _isShopOpen = false; //is the shop currently open or closed

    void Start()
    {
        _gmScript = this.GetComponent<GameManagerScript>(); //get reference to game manager script
        _damageUpgradeCostText.text = "Cost: " + _dmgUpgradesCost[_dmgUpgrades].ToString(); //make dmg upgrade text upgrade display correct starting cost
        _playerSwingSpeedUpgradeCostText.text = "Cost: " + _playerSwingSpeedUpgradesCost[_playerSwingSpeedUpgrades].ToString(); //make player swing speed upgrade text display correct starting cost
        

        //load from inspector if you have load save on and upgrades
        if (_dmgUpgrades > 0 && _loadSave == true)
        {
            LoadDamageUpgrades(_dmgUpgrades);
        }
        if (_playerSwingSpeedUpgrades > 0 && _loadSave == true)
        {
            LoadSpeedUpgrades(_playerSwingSpeedUpgrades);
        }
    }

    public void UpgradePlayerSwingSpeed() //upgrade player swing speed
    {
        if (_playerSwingSpeedUpgrades < _playerSwingSpeedpeedTokenObjs.Count)
        {
            if (_gmScript.GetWoodCollected() >= _playerSwingSpeedUpgradesCost[_playerSwingSpeedUpgrades])
            {
                _gmScript.SpendWood(_playerSwingSpeedUpgradesCost[_playerSwingSpeedUpgrades]);
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AxeScript>().ChangeSwingSpeed(_playerSwingSpeedIncrements[_playerSwingSpeedUpgrades]);
                _playerSwingSpeedUpgrades++;
                for (int i = 0; i < _playerSwingSpeedUpgrades; i++)
                {
                    _playerSwingSpeedpeedTokenObjs[i].GetComponent<Image>().color = Color.green;
                }
                if (_playerSwingSpeedUpgrades >= _playerSwingSpeedpeedTokenObjs.Count)
                {
                    _playerSwingSpeedUpgradeCostText.text = "No More upgrades";

                    return;
                }
                //update cost
                _playerSwingSpeedUpgradeCostText.text = "Cost: " + _playerSwingSpeedUpgradesCost[_playerSwingSpeedUpgrades].ToString();

            }
        }
    }
    public void UpgradePlayerDamage()//upgrade player DMG
    {
        if (_dmgUpgrades < _dmgTokenObjs.Count)
        {
            if (_gmScript.GetWoodCollected() >= _dmgUpgradesCost[_dmgUpgrades])
            {
                _gmScript.SpendWood(_dmgUpgradesCost[_dmgUpgrades]);
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AxeScript>().UpDMG(10);
                _gmScript.UpgradeWoodMultiplier(1);
                _dmgUpgrades++;
                for (int i = 0; i < _dmgUpgrades; i++)
                {
                    _dmgTokenObjs[i].GetComponent<Image>().color = Color.green;
                }
                if (_dmgUpgrades >= _dmgTokenObjs.Count)
                {
                    _damageUpgradeCostText.text = "No More upgrades";

                    return;
                }
                //update cost
                _damageUpgradeCostText.text = "Cost: " + _dmgUpgradesCost[_dmgUpgrades].ToString();

            }
        }
    }
    void LoadDamageUpgrades(int m_dmgUpgrades) //load player dmg upgrade from save
    {
        _dmgUpgrades = m_dmgUpgrades;
        for (int i = 0; i < _dmgUpgrades; i++)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AxeScript>().UpDMG(10);
            _gmScript.UpgradeWoodMultiplier(1);
            _dmgTokenObjs[i].GetComponent<Image>().color = Color.green;
        }
        _damageUpgradeCostText.text = "Cost: " + _dmgUpgradesCost[_dmgUpgrades].ToString();

    }
    void LoadSpeedUpgrades(int m_speedUpgrades)//load player swing speed upgrade from save
    {
        _playerSwingSpeedUpgrades = m_speedUpgrades;

        for (int i = 0; i < _playerSwingSpeedUpgrades; i++)
        {
            _playerSwingSpeedpeedTokenObjs[i].GetComponent<Image>().color = Color.green;
        }
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AxeScript>().ChangeSwingSpeed(_playerSwingSpeedIncrements[_playerSwingSpeedUpgrades]);

        if (_playerSwingSpeedUpgrades >= _playerSwingSpeedpeedTokenObjs.Count)
        {
            _playerSwingSpeedUpgradeCostText.text = "No More upgrades";

            return;
        }
        //update cost
        _playerSwingSpeedUpgradeCostText.text = "Cost: " + _playerSwingSpeedUpgradesCost[_playerSwingSpeedUpgrades].ToString();

    }

    public void SetShopOpen(bool m_isShopOpen) //set shop open or closed
    {
        _isShopOpen = m_isShopOpen;
    }
    public bool IsShopOpen()//getter for if shop is open or closed
    {
        return _isShopOpen;
    }
}
