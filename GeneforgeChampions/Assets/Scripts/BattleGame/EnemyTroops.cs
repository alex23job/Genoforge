using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTroops : MonoBehaviour
{
    private List<TroopObraz> _enemyTroops = new List<TroopObraz>();

    // Start is called before the first frame update
    void Start()
    {
        CreateTroops();        
    }

    private void CreateTroops()
    {
        TroopObraz ob = new TroopObraz(2, "Камни", Vector3.zero, Vector3.zero);
        ob.AddWarObraz(new WarPersonObraz(200, "Воин", 0, new Genofond(1, 25, 1, 1, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(201, "Воин", 0, new Genofond(1, 40, 1, 1, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(202, "Воин", 0, new Genofond(1, 25, 1, 1, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(203, "Стрелок", 1, new Genofond(1, 15, 1, 2, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(204, "Стрелок", 1, new Genofond(1, 15, 1, 2, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(205, "Маг", 2, new Genofond(1, 15, 1, 1, 1, 1, 1, 2)));
        _enemyTroops.Add(ob);
        ob = new TroopObraz(1, "Брёвна", Vector3.zero, Vector3.zero);
        ob.AddWarObraz(new WarPersonObraz(206, "Воин", 0, new Genofond(1, 20, 1, 1, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(207, "Воин", 0, new Genofond(1, 30, 1, 1, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(208, "Воин", 0, new Genofond(1, 20, 1, 1, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(209, "Стрелок", 1, new Genofond(1, 10, 1, 3, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(210, "Стрелок", 1, new Genofond(1, 10, 1, 2, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(211, "Маг", 2, new Genofond(1, 10, 1, 1, 1, 1, 1, 3)));
        _enemyTroops.Add(ob);
        ob = new TroopObraz(3, "Золото", Vector3.zero, Vector3.zero);
        ob.AddWarObraz(new WarPersonObraz(212, "Воин", 0, new Genofond(1, 30, 1, 1, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(213, "Воин", 0, new Genofond(2, 50, 2, 1, 2, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(214, "Воин", 0, new Genofond(1, 30, 1, 1, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(215, "Стрелок", 1, new Genofond(1, 20, 1, 4, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(216, "Стрелок", 1, new Genofond(1, 20, 1, 4, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(217, "Маг", 2, new Genofond(1, 20, 1, 1, 1, 1, 1, 4)));
        _enemyTroops.Add(ob);
        ob = new TroopObraz(4, "Логово", Vector3.zero, Vector3.zero);
        ob.AddWarObraz(new WarPersonObraz(218, "Воин", 0, new Genofond(2, 40, 1, 1, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(219, "Воин", 0, new Genofond(3, 60, 3, 1, 4, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(220, "Воин", 0, new Genofond(2, 40, 1, 1, 1, 1, 1, 1)));
        ob.AddWarObraz(new WarPersonObraz(221, "Стрелок", 1, new Genofond(1, 20, 2, 4, 1, 2, 2, 1)));
        ob.AddWarObraz(new WarPersonObraz(222, "Стрелок", 1, new Genofond(1, 20, 2, 4, 1, 2, 2, 1)));
        ob.AddWarObraz(new WarPersonObraz(223, "Маг", 2, new Genofond(2, 20, 3, 1, 1, 1, 1, 6)));
        _enemyTroops.Add(ob);

        //print($"CreateEnemy \n<{_enemyTroops[0].ToCsvString()}>\n<{_enemyTroops[1].ToCsvString()}>\n<{_enemyTroops[2].ToCsvString()}>\n<{_enemyTroops[3].ToCsvString()}>");
    }

    public TroopObraz GetTroopObrazByID(int id)
    {
        foreach(TroopObraz obraz in _enemyTroops)
        {
            if (obraz.TroopID == id) return obraz;
        }
        return null;
    }
}
