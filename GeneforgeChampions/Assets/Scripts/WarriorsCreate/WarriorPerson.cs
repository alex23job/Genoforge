using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorPerson : MonoBehaviour
{
    [SerializeField] private int _typeID;
    private WarPersonObraz _personObraz;
    private BattleWarrior _battleWarrior;

    public int WarriorType { get { return _typeID; } }
    public WarPersonObraz ObrazWarrior { get { return _personObraz; } }

    private void Awake()
    {
        _battleWarrior = GetComponent<BattleWarrior>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetObraz(WarPersonObraz wpo, int nTr, int nPos)
    {
        _personObraz = wpo;
        _battleWarrior.SetParams(wpo, nTr, nPos);
    }
}
