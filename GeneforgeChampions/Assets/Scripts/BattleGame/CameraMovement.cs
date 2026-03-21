using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _maxLeft;
    [SerializeField] private float _maxRight;
    [SerializeField] private Scrollbar _cameraScrollbar;
    // Start is called before the first frame update
    void Start()
    {
        _cameraScrollbar.value = 0.3f;
    }

    public void OnValueChanged()
    {
        float sumMax = _maxRight - _maxLeft;
        Vector3 cameraPosition = transform.position;
        float posX = _maxLeft + _cameraScrollbar.value * sumMax;
        cameraPosition.x = Mathf.Clamp(posX, _maxLeft, _maxRight);
        transform.position = cameraPosition;
    }
}
