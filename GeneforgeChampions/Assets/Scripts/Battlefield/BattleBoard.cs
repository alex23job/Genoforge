using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleBoard : MonoBehaviour
{
    [SerializeField] private BattlefieldUI _battlefieldUI;
    [SerializeField] private GameObject[] _prefabsMagic;
    [SerializeField] private GameObject[] _prefabWarriors;
    [SerializeField] private GameObject _prefabField;
    [SerializeField] private GameObject _prefabGrass;
    [SerializeField] private float _ofsX = -20f;
    [SerializeField] private float _ofsY = 16f;

    private int[] myIndexCeils;
    private int[] enemyCeils;

    private List<GameObject> _warriors = new List<GameObject>();
    private List<GameObject> _enemys = new List<GameObject>();

    private List<BattleWarrior> _battleWarriors = new List<BattleWarrior>();
    private int _currentWarrior = 0;
    private GameObject _selectedWarrior = null;

    private MagicAttack _magicAttack;
    private Vector3 _centerMyTroop;
    private Vector3 _centerEnemyTroop;

    const int nCol = 41;
    const int nRow = 33;

    private void Awake()
    {
        _magicAttack = gameObject.GetComponent<MagicAttack>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
        SetBattleBoardToWarriors();
        BeginBattle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateBoard()
    {
        int x, y, i;
        Vector3 pos = Vector3.zero;
        GameObject prefab = _prefabGrass;
        for (y = 0; y < 33; y++)
        {
            for (x = 0; x < 41; x++)
            {
                pos.x = _ofsX + x;
                pos.z = _ofsY - y;
                if (((x > 18) && (x < 23)) && ((y > 15) && (y < 19)))
                {
                    prefab = _prefabField;
                }
                else prefab = _prefabGrass;
                GameObject ceil = Instantiate(prefab, pos, Quaternion.identity);
                ceil.transform.parent = transform;
            }
        }
        myIndexCeils = new int[6] { nCol * 16 + 20, nCol * 17 + 20, nCol * 18 + 20, nCol * 16 + 19, nCol * 17 + 19, nCol * 18 + 19 };
        enemyCeils = new int[6] { nCol * 16 + 21, nCol * 17 + 21, nCol * 18 + 21, nCol * 16 + 22, nCol * 17 + 22, nCol * 18 + 22 };
        _centerMyTroop = new Vector3(_ofsX + 19.5f, 1f, _ofsY - 17f);
        _centerEnemyTroop = new Vector3(_ofsX + 21.5f, 1f, _ofsY - 17f);
        TroopObraz enemyTroop = GameManager.Instance.currentPlayer.enemyTroop;
        TroopObraz myTroop = GameManager.Instance.currentPlayer.playerTroop;
        List<WarPersonObraz> enemys = enemyTroop.GetSortWarriors();
        List<WarPersonObraz> warriors = myTroop.GetSortWarriors();
        Vector3 posW = Vector3.zero;
        posW.y = 0.075f;
        for (i = 0; i < 6; i++)
        {
            posW.x = _ofsX + myIndexCeils[i] % nCol;
            posW.z = _ofsY - myIndexCeils[i] / nCol;
            if (i < warriors.Count)
            {
                GameObject warrior = Instantiate(_prefabWarriors[warriors[i].Type], posW, Quaternion.Euler(new Vector3(0, 90f, 0)));
                warrior.GetComponent<WarriorPerson>().SetObraz(warriors[i], 1, i);
                _battleWarriors.Add(warrior.GetComponent<BattleWarrior>());
                _warriors.Add(warrior);
            }
            posW.x = _ofsX + enemyCeils[i] % nCol;
            posW.z = _ofsY - enemyCeils[i] / nCol;
            if (i < enemys.Count)
            {
                GameObject enemy = Instantiate(_prefabWarriors[enemys[i].Type], posW, Quaternion.Euler(new Vector3(0, 270f, 0)));
                enemy.GetComponent<WarriorPerson>().SetObraz(enemys[i], 2, i);
                _battleWarriors.Add(enemy.GetComponent<BattleWarrior>());
                _enemys.Add(enemy);
            }
        }
    }

    private void SetBattleBoardToWarriors()
    {
        BattleBoard battleBoard = gameObject.GetComponent<BattleBoard>();
        foreach (BattleWarrior bw in _battleWarriors) bw.SetBattleBoard(battleBoard);

        _battleWarriors = _battleWarriors.OrderBy(w => w.Intellect).ToList();
    }

    private void BeginBattle()
    {
        _battleWarriors[_currentWarrior].ViewContur(true);
        WarriorAttack();
    }

    public void WarriorAttack()
    {
        if (_battleWarriors[_currentWarrior].NumberTroop == 2)
        {   //  воин из отряда противников
            _battleWarriors[_currentWarrior].Attack();
            //NextAttack();
        }
        else
        {
            if (_battleWarriors[_currentWarrior].TypeWarrior == 2)
            {   //  это наш маг => дать выбрать заклинание
                _battlefieldUI.ViewMagicPanel(true);
            }
        }
    }

    public void MyMagicAttack(int numMagic)
    {
        _battlefieldUI.ViewMagicPanel(false);
        _magicAttack.SetParams(_centerEnemyTroop, _prefabsMagic[numMagic], _battleWarriors[_currentWarrior].Magic); //  заклинания могут что-то делать и на своих воинов ?!
        //SimpleMagic sm = _prefabsMagic[numMagic].GetComponent<SimpleMagic>();
        //int magicDamage = 2;    //  нужно где-то узнать урон от заклинания в зависимости от прокачки самого мага или прокачки заклинания?
        //if (sm != null) magicDamage = sm.Damage;
        //foreach (BattleWarrior bw in _battleWarriors)
        //{
        //    bw.TakeDamage(magicDamage);
        //}
        _battleWarriors[_currentWarrior].AnimatedMagic();
        _selectedWarrior = null;
        //NextAttack();
    }
    public void MyMagicUpCharackters(int numMagic)
    {
        _battlefieldUI.ViewMyMagicPanel(false);
        if (_selectedWarrior != null)
        {
            BattleWarrior bw = _selectedWarrior.GetComponent<BattleWarrior>();
            if (bw != null) bw.MagicEffect(_magicAttack.GetMyMagicAttack(numMagic));
        }
        _magicAttack.SetParams(_centerEnemyTroop, _prefabsMagic[numMagic], _battleWarriors[_currentWarrior].Magic); //  заклинания могут что-то делать и на своих воинов ?!
        //int magicDamage = 2;    //  нужно где-то узнать урон от заклинания в зависимости от прокачки самого мага или прокачки заклинания?
        //foreach (BattleWarrior bw in _battleWarriors)
        //{
        //    bw.TakeDamage(magicDamage);
        //}
        _battleWarriors[_currentWarrior].AnimatedMagic();
        _selectedWarrior = null;
        //NextAttack();
    }

    public void NextAttack()
    {
        int res = CheckTroops();
        if (res > 0)
        {   //  один из отрядов уничтожен => конец битвы
            if (res == 2) _battlefieldUI.ViewWinPanel(_enemys);
        }
        else
        {
            _currentWarrior %= _battleWarriors.Count;
            _battleWarriors[_currentWarrior].ViewContur(false);
            _currentWarrior++;
            _currentWarrior %= _battleWarriors.Count;
            _battleWarriors[_currentWarrior].ViewContur(true);
            WarriorAttack();
        }
    }

    private int CheckTroops()
    {
        int countWarriors = 0, countEnemys = 0;
        foreach (BattleWarrior bw in _battleWarriors)
        {
            if (bw.NumberTroop == 1) countWarriors++;
            if (bw.NumberTroop == 2) countEnemys++;
        }
        if (countWarriors == 0) return 1;   //  наш отряд уничтожен
        if (countEnemys == 0) return 2;   //  отряд врагов уничтожен
        return 0;   //  оба отряда живы
    }

    public void SelectedWarrior(GameObject warrior)
    {
        _selectedWarrior = warrior;
        BattleWarrior enemy = warrior.GetComponent<BattleWarrior>();
        if (_battleWarriors[_currentWarrior].TypeWarrior == 0)
        {
            if (enemy.NumberTroop == 1)
            {  //  свой воин
                _battlefieldUI.ViewMyMagicPanel(true);
            }
            else
            {
                if (_battleWarriors[_currentWarrior].NumberPosition == 0)
                {
                    if (enemy.NumberPosition < 2) { _battleWarriors[_currentWarrior].Attack(warrior); return; }
                    else _battlefieldUI.ViewErrorPanel();
                }
                if (_battleWarriors[_currentWarrior].NumberPosition == 1)
                {
                    if (enemy.NumberPosition < 3) { _battleWarriors[_currentWarrior].Attack(warrior); return; }
                    else _battlefieldUI.ViewErrorPanel();
                }
                if (_battleWarriors[_currentWarrior].NumberPosition == 2)
                {
                    if ((enemy.NumberPosition == 2) || (enemy.NumberPosition == 1)) { _battleWarriors[_currentWarrior].Attack(warrior); return; }
                    else _battlefieldUI.ViewErrorPanel();
                }
                if (_battleWarriors[_currentWarrior].NumberPosition > 2)
                {   //  рядом с воином-танком только свои => можно только заклинания какие-то
                    _battleWarriors[_currentWarrior].AnimatedMagic();
                }
                _selectedWarrior = null;
            }
        }
        if (_battleWarriors[_currentWarrior].TypeWarrior == 1)
        {
            if (enemy.NumberTroop == 1)
            {   //  свой воин
                _battlefieldUI.ViewMyMagicPanel(true);
            }
            else
            {   //  чужой воин
                _battleWarriors[_currentWarrior].Attack(warrior);
                _selectedWarrior = null;
            }
        }
    }

    public GameObject GetTargetForAttack(BattleWarrior warrior)
    {
        if (warrior.TypeWarrior == 2)
        {
            int maxCountMagic = _prefabsMagic.Length;
            //if (maxCountMagic > warrior.Magic) maxCountMagic = warrior.Magic;
            int indexMagic = UnityEngine.Random.Range(0, maxCountMagic);
            _magicAttack.SetParams(_centerMyTroop, _prefabsMagic[indexMagic], warrior.Magic);
            //_magicAttack.SetParams(_centerMyTroop, _prefabsMagic[0]);
            //_magicAttack.SetParams(_centerMyTroop, _prefabsMagic[1]);
            //_magicAttack.SetParams(_centerMyTroop, _prefabsMagic[2]);
            //foreach (BattleWarrior bw in _battleWarriors)
            //{
            //    if (bw.NumberTroop == 1) bw.TakeDamage(2);
            //}
        }
        else if (warrior.TypeWarrior == 1)
        {
            List<int> numWarriors = new List<int>();
            foreach (BattleWarrior bw in _battleWarriors)
            {
                if (bw.NumberTroop == 1) numWarriors.Add(bw.NumberPosition);
            }
            if (numWarriors.Count > 0)
            {
                int num = Random.Range(0, numWarriors.Count);
                return _warriors[num];
            }
        }
        else
        {
            List<GameObject> listGO = new List<GameObject>();
            if (warrior.NumberPosition == 0)
            {
                foreach (BattleWarrior bw in _battleWarriors)
                {
                    if (bw.NumberTroop == 1 && ((bw.NumberPosition == 0) || (bw.NumberPosition == 1))) listGO.Add(_warriors[bw.NumberPosition]);
                }
            }
            if (warrior.NumberPosition == 2)
            {
                foreach (BattleWarrior bw in _battleWarriors)
                {
                    if (bw.NumberTroop == 1 && ((bw.NumberPosition == 2) || (bw.NumberPosition == 1))) listGO.Add(_warriors[bw.NumberPosition]);
                }
            }
            if (warrior.NumberPosition == 1)
            {
                foreach (BattleWarrior bw in _battleWarriors)
                {
                    if (bw.NumberTroop == 1 && (bw.NumberPosition < 3)) listGO.Add(_warriors[bw.NumberPosition]);
                }
            }
            int index = Random.Range(0, listGO.Count);
            return (listGO.Count > 0) ?  listGO[index] : null;
        }
        return null;
    }

    public void WarriorLoss(BattleWarrior lbw)
    {
        int removeIndex = -1;
        for (int i = 0; i < _battleWarriors.Count; i++)
        {
            if (_battleWarriors[i].WarID == lbw.WarID)
            {
                removeIndex = i; break;
            }
        }
        if (removeIndex >= 0)
        {
            _battleWarriors[removeIndex].AnimatedLoss();
            _battleWarriors.RemoveAt(removeIndex);
            if (removeIndex < _currentWarrior)
            {
                _currentWarrior--;
            }
        }
    }
}
