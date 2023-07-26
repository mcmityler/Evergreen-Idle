/*
Created by:  Tyler McMillan
Description: This script deals with destroying whatever object its attatched too after a delay
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    [SerializeField] float _destroyTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, _destroyTime);
    }
}
