using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TroopsUI : MonoBehaviour
{
    [SerializeField] private GameObject _namePanel;
    [SerializeField] private InputField _nameInput;

    [SerializeField] private GameObject[] _troopsItems;
    [SerializeField] private GameObject[] _warriorsItems;
    [SerializeField] private GameObject[] _troopWarriorsItems;
    [SerializeField] private Text _troopTitle;

    [SerializeField] private Button _selectTroopUp;
    [SerializeField] private Button _selectTroopDown;
    [SerializeField] private Button _selectWarriorLeft;
    [SerializeField] private Button _selectWarriorRight;

    private TroopObraz[] _troops = null;
    private TroopObraz _currentTroop = null;
    private int _currentTroopIndex = 0;
    private int _currentWarriorIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAllPanels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNewTroopClick()
    {
        _namePanel.SetActive(true);
    }

    public void OnDelTroopClick()
    {
        if (_currentTroop != null)
        {   //  удалить отряд и всё обновить
            PlayersTroops.Instance.RemoveTroopByID(_currentTroop.TroopID);
        }
    }

    private void UpdateAllPanels()
    {
        _troops = PlayersTroops.Instance.GetAllObrazs();
        UpdateSelectTroops();
        UpdateTroopWarriors();
    }

    public void SelectTroop(int num)
    {
        _currentTroop = _troops[_currentTroopIndex + num];
        UpdateTroopWarriors();
    }

    public void AddingSelectedWarrior(int num)
    {

    }

    public void RemoveSelectedWarrior(int num)
    {

    }

    public void OnLeftWarriorsClick()
    {

    }

    public void OnRightWarriorsClick()
    {

    }

    public void OnTroopUpClick()
    {
        if (_currentTroopIndex > 0) 
        {
            _currentTroopIndex--; 
            UpdateSelectTroops();
        }
    }

    public void OnTroopDownClick()
    {
        if (_currentTroopIndex < _troops.Length - 3)
        {
            _currentTroopIndex++;
            UpdateSelectTroops();
        }
    }

    public void UpdateSelectTroops()
    {
        for (int i = 0; i < _troopsItems.Length; i++)
        {
            if ((_currentTroopIndex + i) < _troops.Length)
            {
                _troopsItems[i].SetActive(true);
                Text title = _troopsItems[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                if (title != null) title.text = $"Отряд N% {_troops[_currentTroopIndex + i].TroopID}";
                Text descr = _troopsItems[i].transform.GetChild(1).gameObject.GetComponent<Text>();
                if (descr != null) descr.text = $"{_troops[_currentTroopIndex + i].NameTroop}";
            }
            else
            {
                _troopsItems[i].SetActive(false);
            }
        }
    }

    public void UpdateTroopWarriors()
    {
        if (_currentTroop != null)
        {
            WarPersonObraz[] warriors = _currentTroop.GetWarriors();
            int i, j;
            for (i = 0; i < _troopWarriorsItems.Length; i++)
            {
                if (i < warriors.Length)
                {
                    _troopWarriorsItems[i].SetActive(true);
                    Text title = _troopWarriorsItems[i].transform.GetChild(1).gameObject.GetComponent<Text>();
                    if (title != null) title.text = $"{warriors[i].Title} {warriors[i].Exp}";
                    int[] charcs = warriors[i].GenoWar.GetCharcs();
                    for (j = 0; j < charcs.Length; j++)
                    {
                        Text txtValue = _troopWarriorsItems[i].transform.GetChild(10 + j).gameObject.GetComponent<Text>();
                        if (txtValue != null) txtValue.text = charcs[j].ToString();
                    }
                }
                else
                {
                    _troopWarriorsItems[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int t = 0; t < _troopWarriorsItems.Length; t++)
            {
                _troopWarriorsItems[t].SetActive(false);
            }
        }
    }

    public void OnSaveNameTroopClick()
    {
        if (_nameInput != null)
        {
            string name = _nameInput.text;
            PlayersTroops.Instance.AddingTroop(name, Vector3.zero, Vector3.zero);
            UpdateAllPanels();
        }
        _namePanel.SetActive(false);
    }

    public void TroopsExit()
    {
        SceneManager.LoadScene("BattleScene");
    }

}
