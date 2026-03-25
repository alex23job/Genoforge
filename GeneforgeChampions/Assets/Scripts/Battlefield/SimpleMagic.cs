using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMagic : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private ParticleSystem _final;
    [SerializeField] private int _baseDamage = 1;

    private float _dopPrcDamage = 1;
    private int _mult = 1;
    private bool _isRost = true;
    private bool _isUsed = true;
    Vector3 scale = new Vector3(1f, 1f, 1f);
    private int _damage = 1;

    public int Damage { get { return _damage; } }

    // Start is called before the first frame update
    void Start()
    {
        if (_final != null) _final.gameObject.SetActive(false);
        _damage = _baseDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isUsed)
        {
            if (_isRost)
            {
                if (_mult < 100)
                {
                    _mult++;
                }
                else
                {
                    _isRost = false;
                }
            }
            else
            {
                if (_mult > 50)
                {
                    _mult--;
                }
                else
                {

                }
            }
            _body.localScale = scale * (_mult / 200f);
        }
    }

    public void SetMultDamage(float dopDmg)
    {
        _dopPrcDamage = dopDmg;
        _damage = Mathf.CeilToInt(_baseDamage * (100f + _dopPrcDamage) / 100f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Warrior"))
        {
            BattleWarrior bw = other.gameObject.GetComponent<BattleWarrior>();
            if (bw != null) bw.TakeDamage(_damage);
            //print($"magic   {other.name}");
            if (_final != null)
            {
                _final.gameObject.SetActive(true);
                _final.Play();
            }
            _isUsed = false;
            Destroy(gameObject, 2f);
        }
    }
}
