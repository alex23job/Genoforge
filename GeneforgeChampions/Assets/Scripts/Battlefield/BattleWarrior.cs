using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BattleWarrior : MonoBehaviour
{
    [SerializeField] private GameObject _contur;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _hp_green;
    [SerializeField] private GameObject _hp_red;
    private BattleBoard _battleBoard = null;

    private WarPersonObraz _obraz = null;
    /// <summary>
    /// мой отряд - 1, отряд противника - 2
    /// </summary>
    private int _numberTroop = 0;

    private int _maxDamage = 0;
    private int _hp = 0;
    private int _intellect = 0;
    private int _accuracy = 0;
    private int _rage = 0;
    private int _regeneration = 0;
    private int _protection = 0;
    private int _magic = 0;

    private int _maxHp = 0;
    private int _numberPosition;
    private Vector3 _startRotation;
    private Animator _anim;
    private BattleWarrior _myBattleWarrior;

    private int _dopDamage = 0;
    private int _dopAccuracy = 0;
    private int _dopSchild = 0;

    public int WarID { get { return _obraz.WarID; } }
    public int TypeWarrior { get { return _obraz.Type; } }
    public int NumberPosition { get { return _numberPosition; } }
    public int NumberTroop { get { return _numberTroop; } }

    public int MaxDamage { get => _maxDamage; }
    public int Hp { get => _hp; }
    public int Intellect { get => _intellect; }
    public int Accuracy { get => _accuracy; }
    public int Rage { get => _rage; }
    public int Regeneration { get => _regeneration; }
    public int Protection { get => _protection; }
    public int Magic { get => _magic; }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _myBattleWarrior = GetComponent<BattleWarrior>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _hp_green.SetActive(false);
        _hp_red.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParams(WarPersonObraz personObraz, int nTr, int nPos)
    {
        _obraz = personObraz;
        _numberTroop = nTr;
        _numberPosition = nPos;

        Genofond gen = _obraz.GenoWar;
        _maxDamage = gen._power * 5;
        _hp = gen._endurance;
        _intellect = gen._intellect;
        _accuracy = gen._accuracy;
        _rage = gen._rage;
        _regeneration = gen._regeneration;
        _protection = gen._protection;
        _magic = gen._magic;

        _maxHp = _hp;
    }

    public void SetBattleBoard(BattleBoard board)
    {
        _battleBoard = board;
        _startRotation = transform.rotation.eulerAngles;
        _startRotation = _body.transform.rotation.eulerAngles;
    }

    public void ViewContur(bool isView)
    {
        _contur.SetActive(isView);
    }

    public void RegenerationWarrior()
    {
        _hp += _regeneration;
        if (_hp > _maxHp) _hp = _maxHp;
    }

    public void Attack(GameObject target = null)
    {
        RegenerationWarrior();
        _obraz.AddExp(1);
        if (target != null)
        {
            AttackWarrior(target);
        }
        else
        {
            GameObject myTarget = _battleBoard.GetTargetForAttack(_myBattleWarrior);
            if (myTarget != null)
            {
                AttackWarrior(myTarget);
            }
        }
        AnimatedAttack();
    }

    private void AttackWarrior(GameObject warrior)
    {
        Vector3 dir = warrior.transform.position - _body.transform.position; dir.y = 0;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        _body.transform.rotation = Quaternion.Slerp(_body.transform.rotation, lookRot, 45f);
        BattleWarrior enemy = warrior.GetComponent<BattleWarrior>();
        enemy.TakeDamage(_maxDamage + _dopDamage);
        _dopDamage = 0;
    }

    public void AnimatedAttack()
    {
        if (_anim != null)
        {
            _anim.SetBool("IsAttack", true);
        }
        Invoke("EndAttack", 2f);
    }

    public void AnimatedMagic()
    {
        if (_anim != null)
        {
            _anim.SetBool("IsMagic", true);
        }
        Invoke("EndAttack", 2f);
    }

    public void AnimatedLoss()
    {
        if (_anim != null)
        {
            _anim.SetBool("IsLoss", true);
        }
        //  Удалить через 2 секунды воина с поля ???
        _body.transform.rotation = Quaternion.Euler(new Vector3(90f, 0, 0));
    }

    private void EndAttack()
    {
        if (_anim != null)
        {
            _anim.SetBool("IsAttack", false);
        }
        if (_battleBoard != null)
        {
            _battleBoard.NextAttack();
        }
        _body.transform.rotation = Quaternion.Euler(_startRotation);
    }

    public void MagicEffect(MyMagicAttack mm)
    {
        //print(mm.ToString());
        switch(mm.mode)
        {
            case 1:
                _dopDamage = mm.value;
                break;
            case 2:
                _dopAccuracy = mm.value;
                break;
            case 3:
                _dopSchild = mm.value;
                break;
            case 4:
                _hp += mm.value;
                if (_hp > _maxHp) _hp = _maxHp;
                break;
        }
    }

    private void OnMouseUp()
    {
        if (_battleBoard != null)
        {
            _battleBoard.SelectedWarrior(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        int takeDamage = damage - _dopSchild;
        _hp -= takeDamage;
        if (_hp < 0) 
        {   //  бойца убило !!!
            _hp = 0; 
            _battleBoard.WarriorLoss(_myBattleWarrior);
        }
        //print($"nm={_obraz.NameWarrior}{_obraz.WarID}  _hp={_hp} dmg={damage}");
        //  показывать уровень оставшегося здоровья !?
        if (_hp < _maxHp)
        {
            _hp_green.SetActive(true);
            _hp_red.SetActive(true);
            Vector3 vol = _hp_green.transform.localScale;
            vol.x = 0.8f * ((float)_hp / (float)_maxHp);
            _hp_green.transform.localScale = vol;
            Vector3 posGreen = _hp_red.transform.localPosition;
            posGreen.z -= (0.8f - vol.x) / 2f;
            _hp_green.transform.localPosition = posGreen;

        }
    }
}
