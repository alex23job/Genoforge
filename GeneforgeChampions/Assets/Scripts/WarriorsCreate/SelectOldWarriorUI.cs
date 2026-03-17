using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOldWarriorUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _btnWarriors;
    [SerializeField] private Button _btnUp;
    [SerializeField] private Button _btnDown;
    [SerializeField] private CharacteristicsUI _characteristics;

    private int _currentIndex = 0;
    private WarPersonObraz[] _obrazes;

    // Start is called before the first frame update
    void Start()
    {
        _obrazes = PlayersWarriors.Instance.GetAllWarriors();
        UpdateItemPanels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWarriorsBtnClick(int numBtn)
    {
        if ((_currentIndex + numBtn < _obrazes.Length) && (_characteristics != null))
        {
            _characteristics.ViewCharacteristics(_obrazes[_currentIndex + numBtn].GenoWar);
        }
    }

    public void OnUpBtnClick()
    {
        if (_currentIndex > 0) _currentIndex--;
        UpdateItemPanels();
    }

    public void OnDownBtnClick()
    {
        if (_currentIndex < (_obrazes.Length - 1)) _currentIndex++;
        UpdateItemPanels();
    }

    private void UpdateItemPanels()
    {
        _btnUp.interactable = (_obrazes.Length > 3) && (_currentIndex > 0);
        _btnDown.interactable = (_obrazes.Length > 3) && (_currentIndex < (_obrazes.Length - 1));
    }
}
