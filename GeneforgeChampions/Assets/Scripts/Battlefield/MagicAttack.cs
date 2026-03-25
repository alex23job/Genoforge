using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    private GameObject _magicPrefab;
    private Vector3 _centerPoint;

    private MyMagicAttack[] myMagicAttacks = new MyMagicAttack[4] { new MyMagicAttack(1, 5), new MyMagicAttack(2, 20), new MyMagicAttack(3, 10), new MyMagicAttack(4, 5) };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParams(Vector3 center, GameObject magic, float dopDmg)
    {
        _centerPoint = center;
        _magicPrefab = magic;
        Vector3 pos = _centerPoint;
        pos.y = 5f;
        //print($"center <{_centerPoint}>");
        for (int i = 0; i < 6; i++)
        {
            pos.x = _centerPoint.x - 0.5f + (i / 3);
            pos.z = _centerPoint.z - 1f + (i % 3);
            GameObject mg = Instantiate(_magicPrefab, pos, Quaternion.identity);
            mg.GetComponent<SimpleMagic>().SetMultDamage(dopDmg);
            //print($"i={i}   pos=<{pos}>");
            Destroy(mg, 25f);
        }
    }

    public MyMagicAttack GetMyMagicAttack(int mode)
    {
        return myMagicAttacks[mode];
    }
}

public struct MyMagicAttack
{
    /// <summary>
    /// 1 - damage, 2 - accuracy, 3 - schild, 4 - hp
    /// </summary>
    public int mode;
    public int value;

    public MyMagicAttack(int m, int v)
    {
        mode = m; 
        value = v;
    }

    public override string ToString()
    {
        return $"MyMagicAttack mode={mode}  value={value}";
    }
}
