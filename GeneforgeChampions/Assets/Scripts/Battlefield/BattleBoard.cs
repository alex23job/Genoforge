using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBoard : MonoBehaviour
{
    [SerializeField] private GameObject _prefabField;
    [SerializeField] private GameObject _prefabGrass;
    [SerializeField] private float _ofsX = -20f;
    [SerializeField] private float _ofsY = 16f;

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateBoard()
    {
        int x, y;
        Vector3 pos = Vector3.zero;
        GameObject prefab = _prefabGrass;
        for (y = 0; y < 33; y++)
        {
            for (x = 0; x < 41; x++)
            {
                pos.x = _ofsX + x;
                pos.z = _ofsY - y;
                if (((x > 18) && (x < 23)) && ((y > 15) && (y < 19))) prefab = _prefabField;
                else prefab = _prefabGrass;
                GameObject ceil = Instantiate(prefab, pos, Quaternion.identity);
                ceil.transform.parent = transform;
            }
        }
    }
}
