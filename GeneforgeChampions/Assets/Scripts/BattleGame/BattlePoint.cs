using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePoint : MonoBehaviour
{
    [SerializeField] private bool _isTroop = true;
    [SerializeField] private int _pointID;
    private int _type;
    private BattleLand _battleLand;

    public bool IsTroop { get { return _isTroop; } }
    public int PointID { get { return _pointID; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TroopObraz GetEnemyTroop()
    {
        if (_battleLand != null) return _battleLand.GetEnemyTroop(_pointID);
        return null;
    }

    public void SetParams(int type, BattleLand bl, int id, bool isTroop = false)
    {
        _type = type;
        _battleLand = bl;
        _pointID = id;
        _isTroop = isTroop;
    }

    private void OnMouseUp()
    {
        if (_battleLand != null)
        {
            _battleLand.BattlePointSelect(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        if (_battleLand != null)
        {
            Vector3 pos = transform.position;
            pos = Camera.main.WorldToScreenPoint(pos);
            pos.y += 150;
            _battleLand.ViewHintPanel(true, gameObject.name, pos);
        }
    }

    private void OnMouseExit()
    {
        if (_battleLand != null)
        {
            Vector3 pos = transform.position;
            pos = Camera.main.WorldToScreenPoint(pos);
            pos.y += 60;
            _battleLand.ViewHintPanel(false, gameObject.name, pos);
        }
    }
}
