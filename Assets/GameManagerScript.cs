using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManagerScript : MonoBehaviour
{

    [SerializeField] TMP_Text _woodCollectedText;
    [SerializeField] int _collectedWoodAmount = 0;
    [SerializeField] int _stumpMultiplier = 2;
    [SerializeField] int _woodMultiplier = 1;
    [SerializeField] List<GameObject> _treeSpawnPlots = new List<GameObject>();
    [SerializeField] List<bool> _treeFull = new List<bool>();
    int _totalTrees = 0;
    int _currentTrees = 0;
    [SerializeField] float _treeSpawnTime = 10f;
    float _treeCTR = 0;
    [SerializeField] TMP_Text _timeToTextTreeSpawn;
    [SerializeField] List<GameObject> _trees = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _woodCollectedText.text = ":" + _collectedWoodAmount;
        _totalTrees = _treeSpawnPlots.Count;
        SpawnTree();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTrees < _totalTrees)
        {
            _treeCTR += Time.deltaTime;
            if (_treeCTR >= _treeSpawnTime)
            {
                SpawnTree();
                _treeCTR = 0;
            }
            _timeToTextTreeSpawn.text = "Time to Next Spawn: " + (_treeSpawnTime - _treeCTR).ToString("F2");

        }
        else if (_currentTrees >= _totalTrees)
        {
            _timeToTextTreeSpawn.text = "Time to Next Spawn: Full";
        }
    }
    void SpawnTree()
    {
        bool m_canSpawn = false;
        do
        {

            int m_randomPlotNum = Random.Range(0, _treeSpawnPlots.Count);
            if (_treeFull[m_randomPlotNum] == false)
            {
                GameObject m_tree = Instantiate(_trees[0], _treeSpawnPlots[m_randomPlotNum].transform.position, Quaternion.identity);
                m_canSpawn = true;
                _treeFull[m_randomPlotNum] = true;
                _currentTrees++;
                m_tree.GetComponent<TreeScript>().SetPlotNum(m_randomPlotNum);
            }

        } while (m_canSpawn == false);
    }
    public void AddPlot(GameObject m_newPlot) 
    {
        _treeSpawnPlots.Add(m_newPlot);
        _treeFull.Add(false);
        _totalTrees++;
    }
    public void WoodGathered(int m_woodCost, bool m_isStump, int m_plotNum)
    {
        int m_amount = m_woodCost * _woodMultiplier;
        if (m_isStump)
        {
            m_amount *= _stumpMultiplier;
            _treeFull[m_plotNum] = false;
            _currentTrees--;
        }
        _collectedWoodAmount += m_amount;
        _woodCollectedText.text = ":" + _collectedWoodAmount;

    }
}