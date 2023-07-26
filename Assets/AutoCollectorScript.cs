using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollectorScript : MonoBehaviour
{
    [SerializeField] GameObject _treeSpawnLocation;
    [SerializeField] List<GameObject> _treeSpawn = new List<GameObject>();
    int _treeAutoLevel = 0;
    [SerializeField] float _treeReplenishTime = 10f;
    float _treeCTR = 0f;
    bool _treeFull = false;
    GameObject _currentTree = null;
    [SerializeField] int _autoChopperDmg = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_treeFull == false)
        {
            _treeCTR += Time.deltaTime;
            if(_treeCTR >= _treeReplenishTime)
            {
                _treeCTR = 0;
                _currentTree = Instantiate(_treeSpawn[_treeAutoLevel], _treeSpawnLocation.transform.position, Quaternion.identity);
                _currentTree.GetComponent<TreeScript>().SetAutoTree(true);
                _treeFull = true;
                this.gameObject.GetComponent<Animator>().SetTrigger("Chop");
            }
        }
    }
    public void AutoCollectorFinished()
    {
        Debug.Log("Finished Anim");
        if (_currentTree == null)
        {
            _treeFull = false;
            return;
        }
        _currentTree.GetComponent<TreeScript>().DamageTree(_autoChopperDmg);
        if (_treeFull) //keep chopping if there is still a tree there
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Chop");

        }
    }
    public void CheckIsFull()
    {
        if (_currentTree == null)
        {
            _treeFull = false;
            this.gameObject.GetComponent<Animator>().SetTrigger("Idle");
            return;
        }
    }
}
