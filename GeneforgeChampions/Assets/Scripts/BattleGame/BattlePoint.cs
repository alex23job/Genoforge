using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePoint : MonoBehaviour
{
    private int _type;
    private BattleLand _battleLand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParams(int type, BattleLand bl)
    {
        _type = type;
        _battleLand = bl;
    }

    private void OnMouseUp()
    {
        if (_battleLand != null)
        {
            _battleLand.BattlePointSelect(gameObject);
        }
    }
}
