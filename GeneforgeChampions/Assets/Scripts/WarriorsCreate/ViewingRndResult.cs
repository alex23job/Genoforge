using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewingRndResult : MonoBehaviour
{
    private Text _txtRnd;
    private bool _isView = false;
    private int _count = 0;

    private void Awake()
    {
        _txtRnd = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isView)
        {
            _count++;
            transform.localScale = new Vector3(0.005f * _count, 0.005f * _count, 0.005f * _count);
            if (_count > 200) _isView = false;
        }
    }

    public void SetParams(float delay, string strRes)
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        _count = 1;
        _txtRnd.text = strRes;
        Invoke("BeginView", delay);
    }

    private void BeginView()
    {
        _isView = true;
    }
}
