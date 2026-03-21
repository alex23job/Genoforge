using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    private BattleLand _battleLand = null;
    private TroopObraz _obraz = null;
    private List<GameObject> _troopWarriors = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetObraz(TroopObraz to, BattleLand bl)
    {
        _obraz = to;
        _battleLand = bl;
        int i;
        WarPersonObraz[] warriors =  _obraz.GetWarriors();
        if (_troopWarriors.Count > 0)
        {
            for (i = _troopWarriors.Count; i > 0; i--) Destroy(_troopWarriors[i - 1]);
        }
        _troopWarriors.Clear();
        for(i = 0; i < warriors.Length; i++)
        {
            GameObject prefab = _battleLand.GetWarriorsPrefabByTypeID(warriors[i].Type);
            if (prefab != null)
            {
                GameObject warrior = Instantiate(prefab);
                warrior.transform.parent = transform;
                warrior.transform.localPosition = new Vector3(0.16f - 0.32f * (i % 2), 0.035f, 0.25f - 0.25f * (i / 2));
                _troopWarriors.Add(warrior);
            }
        }
    }
}
