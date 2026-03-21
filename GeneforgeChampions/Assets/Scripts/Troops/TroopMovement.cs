using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMovement : MonoBehaviour
{
    [SerializeField] private GameObject _troopRect;
    private bool _isUsed = false;
    private Vector3 _target = Vector3.zero;
    private List<Vector3> _path = new List<Vector3>();
    private int _currentPoint = 0;
    private float _movementSpeed = 3f;
    private float _rotationSpeed = 5f;
    private float stoppingDistance = 0.2f;

    //private Animator anim;
    private Rigidbody rb;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTroop(Time.deltaTime);
    }

    public void SetPath(List<Vector3> pt)
    {
        print($"SetLoopPath path.Count={pt.Count}");
        _path = pt;
        if (_path.Count > 0)
        {
            _currentPoint = 1;
            _target = _path[0];
            _isUsed = true;
        }
    }

    public void MoveTroop(float dt)
    {
        if (_isUsed == false) return;
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        Vector2 tg = new Vector2(_target.x, _target.z);
        if (Vector2.Distance(pos, tg) < stoppingDistance)
        {
            NextPoint();
        }
        else
        {
            LookAtWaypoint(dt);
            MoveTowardsWaypoint(dt);
        }
    }
    private void LookAtWaypoint(float dt)
    {
        Vector3 dir = _target - transform.position; dir.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, _rotationSpeed * Time.deltaTime);
        _troopRect.transform.rotation = Quaternion.Slerp(_troopRect.transform.rotation, lookRot, _rotationSpeed * dt);
    }

    private void NextPoint()
    {
        if (_currentPoint < _path.Count)
        {
            _target = _path[_currentPoint];
            if (_currentPoint < _path.Count - 1) rb.isKinematic = false;
            if (_currentPoint > 0)
            {
                if (_path[_currentPoint - 1].x == _path[_currentPoint].x)
                {
                    if (_path[_currentPoint - 1].z < _path[_currentPoint].z) _target.x += 0.15f; else _target.x -= 0.15f;
                }
                else
                {
                    if (_path[_currentPoint - 1].x < _path[_currentPoint].x) _target.z -= 0.15f; else _target.z += 0.15f;
                }
            }
            _currentPoint++;
        }
        else
        {                                      
            _currentPoint = 0;
            _isUsed = false;
        }
    }
    private void MoveTowardsWaypoint(float dt)
    {
        Vector3 dir = _target - transform.position; dir.y = 0f;
        rb.MovePosition(transform.position + _movementSpeed * dt * dir.normalized);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BattlePoint"))
        {
        }
    }
}
