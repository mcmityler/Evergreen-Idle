using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{
    [SerializeField] GameObject _shopScreen;
    [SerializeField] List<GameObject> _dmgTokenObjs = new List<GameObject>();
    [SerializeField] List<int> _dmgUpgradesCost = new List<int>();
    [SerializeField] TMP_Text _damageUpgradeCostText;
    [SerializeField] int _dmgUpgrades = 0;

    [SerializeField] List<GameObject> _playerSwingSpeedpeedTokenObjs = new List<GameObject>();
    [SerializeField] List<int> _playerSwingSpeedUpgradesCost = new List<int>();
    [SerializeField] List<float> _playerSwingSpeedIncrements = new List<float>();
    [SerializeField] TMP_Text _playerSwingSpeedUpgradeCostText;
    [SerializeField] int _playerSwingSpeedUpgrades = 0;

    GameManagerScript _gmScript;
    [SerializeField] bool _loadSave = false;
    bool _isShopOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        _gmScript = this.GetComponent<GameManagerScript>();
        _damageUpgradeCostText.text = "Cost: " + _dmgUpgradesCost[_dmgUpgrades].ToString();
        _playerSwingSpeedUpgradeCostText.text = "Cost: " + _playerSwingSpeedUpgradesCost[_playerSwingSpeedUpgrades].ToString();

        if (_dmgUpgrades > 0 && _loadSave == true)
        {
            LoadDamageUpgrades(_dmgUpgrades);
        }
        if (_playerSwingSpeedUpgrades > 0 && _loadSave == true)
        {
            LoadSpeedUpgrades(_playerSwingSpeedUpgrades);
        }
    }

    public void UpgradePlayerSwingSpeed()
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
    public void UpgradePlayerDamage()
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
    void LoadDamageUpgrades(int m_dmgUpgrades)
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
    void LoadSpeedUpgrades(int m_speedUpgrades)
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

    public void SetShopOpen(bool m_isShopOpen)
    {
        _isShopOpen = m_isShopOpen;
    }
    public bool IsShopOpen()
    {
        return _isShopOpen;
    }
}
