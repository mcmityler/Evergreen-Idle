/*
Created by:  Tyler McMillan
Description: This script deals with the games score and general things
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] TMP_Text _woodCollectedText; //text that displays how much wood the player has currently
    [SerializeField] int _collectedWoodAmount = 0; //how much wood the player has currently
    [SerializeField] int _stumpMultiplier = 2; //how much stumps are worth
    [SerializeField] int _woodMultiplier = 1; //how much the trees wood is worth
    int _totalPlotsOwned = 0; //how many plots do you own
    int _currentTrees = 0; //how many trees are currently gorwn
    [SerializeField] float _treeSpawnTime = 10f; //how often to spawn a new tree
    float _treeCTR = 0; //counter for when to spawn a new tree
    [SerializeField] TMP_Text _timeToTextTreeSpawn; //text that displays next spawn time
    [SerializeField] List<GameObject> _trees = new List<GameObject>(); //list of trees prefabs 
    [SerializeField] GameObject _woodGatheredText; //text that displays how much wood was collected on that swing (ie the +1)
    [SerializeField] List<TreePlotClass> _plots = new List<TreePlotClass>(); //list of plots the tree can grow in & whether it is full or empty

    // Start is called before the first frame update
    void Start()
    {
        _woodCollectedText.text = ":" + _collectedWoodAmount;

        InitializePlots();
        SpawnTree();
    }



    // Update is called once per frame
    void Update()
    {
        if (_currentTrees < _totalPlotsOwned)
        {
            _treeCTR += Time.deltaTime;
            if (_treeCTR >= _treeSpawnTime)
            {
                SpawnTree();
                _treeCTR = 0;
            }
            _timeToTextTreeSpawn.text = "Time to Next Spawn: " + (_treeSpawnTime - _treeCTR).ToString("F2");

        }
        else if (_currentTrees >= _totalPlotsOwned)
        {
            _timeToTextTreeSpawn.text = "Time to Next Spawn: Full";
        }
    }
    private void InitializePlots() //get total plots owned and also make sure unowned plots are correct colour and showing their price
    {
        foreach (TreePlotClass m_plot in _plots)
        {
            if (m_plot.plotOwned == true)
            {
                _totalPlotsOwned++;
            }
            else if (m_plot.plotOwned == false)
            {
                m_plot.plot.GetComponent<SpriteRenderer>().color = m_plot.unownedColour;
                m_plot.plot.transform.GetChild(0).gameObject.SetActive(true);
                m_plot.plot.GetComponentInChildren<TMP_Text>().text = m_plot.plotCost.ToString();
            }
        }
    }
    void SpawnTree()
    {
        bool m_canSpawn = false;
        do
        {

            int m_randomPlotNum = Random.Range(0, _plots.Count);
            if (_plots[m_randomPlotNum].isEmpty == true && _plots[m_randomPlotNum].plotOwned)
            {
                GameObject m_tree = Instantiate(_trees[0], _plots[m_randomPlotNum].plot.transform.position, Quaternion.identity);
                m_canSpawn = true;
                _plots[m_randomPlotNum].isEmpty = false;
                _currentTrees++;
                m_tree.GetComponent<TreeScript>().SetPlotNum(m_randomPlotNum);
            }

        } while (m_canSpawn == false);
    }
    public void WoodGathered(int m_woodCost, bool m_isStump, int m_plotNum)
    {
        int m_amount = m_woodCost * _woodMultiplier;
        if (m_isStump)
        {
            m_amount *= _stumpMultiplier;
            if (m_plotNum != -1)
            {
                _plots[m_plotNum].isEmpty = true;
                _currentTrees--;
            }
        }
        _collectedWoodAmount += m_amount;
        _woodCollectedText.text = ":" + _collectedWoodAmount;
        if (m_plotNum != -1)
        {
            GameObject m_gatherText = Instantiate(_woodGatheredText, _plots[m_plotNum].plot.transform.position, Quaternion.identity);
            m_gatherText.GetComponentInChildren<TMP_Text>().text = "+" + m_amount.ToString();
        }

    }
    public void PurchasePlot(GameObject m_newPlot)
    {

        foreach (TreePlotClass m_plot in _plots)
        {
            if (m_plot.plot == m_newPlot)
            {
                if (m_plot.plotCost <= _collectedWoodAmount)
                {

                    SpendWood(m_plot.plotCost);
                    m_plot.plotOwned = true;
                    m_plot.plot.GetComponent<SpriteRenderer>().color = m_plot.ownedColour;
                    m_plot.plot.transform.GetChild(0).gameObject.SetActive(false);

                    _totalPlotsOwned++;
                    return;
                }
            }
        }

    }
    public void SpendWood(int m_amountSpent)
    {
        _collectedWoodAmount -= m_amountSpent;
        _woodCollectedText.text = ":" + _collectedWoodAmount;

    }
    public int GetWoodCollected()
    {
        return _collectedWoodAmount;
    }
    public void UpgradeWoodMultiplier(int m_increaseAmount)
    {
        _woodMultiplier += m_increaseAmount;
    }
    public void ToggleAutoSwing(Toggle m_autoSwingToggle)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AxeScript>().ChangeAutoSwing(m_autoSwingToggle.isOn);
    }
}
