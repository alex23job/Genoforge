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
    [SerializeField] private WarriorsCreateUI _warriorsCreateUI;
    [SerializeField] private bool _isLeftPanel = true;

    private int _currentIndex = 0;
    private int _selectIndex = 0;
    private WarPersonObraz[] _obrazes;

    // Start is called before the first frame update
    void Start()
    {
        _obrazes = PlayersWarriors.Instance.GetAllWarriors();
        //print($"Count Wars = {_obrazes.Length}");
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
            _selectIndex = _currentIndex + numBtn;
            _characteristics.ViewCharacteristics(_obrazes[_selectIndex].GenoWar, _obrazes[_selectIndex].Title);
            if (_warriorsCreateUI != null)
            {
                _warriorsCreateUI.SetOldWarrior((_isLeftPanel ? 1 : 2), _obrazes[_currentIndex + numBtn]);
            }
        }
    }

    public void OnUpBtnClick()
    {
        if (_currentIndex > 0) _currentIndex--;
        UpdateItemPanels();
    }

    public void OnDownBtnClick()
    {
        if (_currentIndex < (_obrazes.Length - 3)) _currentIndex++;
        UpdateItemPanels();
    }

    private void UpdateItemPanels()
    {
        _btnUp.interactable = (_obrazes.Length > 3) && (_currentIndex > 0);
        _btnDown.interactable = (_obrazes.Length > 3) && (_currentIndex < (_obrazes.Length - 3));
        for (int i = 0; i < _btnWarriors.Length; i++) 
        {
            if (i < _obrazes.Length)
            {
                _btnWarriors[i].gameObject.SetActive(true);
                if ((i == 0) && (_characteristics != null))
                {
                    _characteristics.ViewCharacteristics(_obrazes[_selectIndex].GenoWar, _obrazes[_selectIndex].Title);
                }
                WarPersonObraz wpo = _obrazes[_currentIndex + i];
                Text txtTitle = _btnWarriors[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                if (txtTitle != null)
                {
                    txtTitle.text = $"{wpo.NameWarrior} {wpo.WarID}";
                }
                Text txtDescr = _btnWarriors[i].transform.GetChild(1).gameObject.GetComponent<Text>();
                if (txtDescr != null)
                {
                    txtDescr.text = $"Опыт: {wpo.Exp}";
                }
            }
            else
            {
                _btnWarriors[i].gameObject.SetActive(false);
            }
        }
    }

    public void ResetPanel()
    {
        _currentIndex = 0;
        _obrazes = PlayersWarriors.Instance.GetAllWarriors();
        if (_selectIndex > _obrazes.Length - 1) _selectIndex = 0;
        UpdateItemPanels();
    }
}
