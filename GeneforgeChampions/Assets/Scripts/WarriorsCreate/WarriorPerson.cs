using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorPerson : MonoBehaviour
{
    [SerializeField] private int _typeID;
    private WarPersonObraz _personObraz;

    public int WarriorType { get { return _typeID; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetObraz(WarPersonObraz wpo)
    {
        _personObraz = wpo;
    }
}
