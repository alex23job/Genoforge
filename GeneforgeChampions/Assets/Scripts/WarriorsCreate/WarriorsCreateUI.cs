using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WarriorsCreateUI : MonoBehaviour
{
    [SerializeField] private Image _imgArrows;
    [SerializeField] private GameObject _sphere;
    [SerializeField] private Text[] _arrRndTexts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCreateWarriorsClick()
    {

    }

    public void CreatingExit()
    {
        SceneManager.LoadScene("BattleScene");
    }
}
