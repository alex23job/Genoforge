using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattlefieldUI : MonoBehaviour
{
    [SerializeField] private GameObject _errorPanel;
    [SerializeField] private GameObject _magicPanel;
    [SerializeField] private GameObject _myMagicPanel;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _createButton;
    [SerializeField] private CharacteristicsUI _characteristicsUI;

    private List<GameObject> _enemys = new List<GameObject>();
    private int _currentEnemy = 0;


    // Start is called before the first frame update
    void Start()
    {
        HideErrorPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BattleLoss()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void ViewErrorPanel(string txtError = "Недопустимая цель")
    {
        _errorPanel.SetActive(true);
        _errorPanel.GetComponentInChildren<Text>().text = txtError;
        Invoke("HideErrorPanel", 3f);
    }

    public void ViewMagicPanel(bool value)
    {
        _magicPanel.SetActive(value);
    }

    public void ViewMyMagicPanel(bool value)
    {
        _myMagicPanel.SetActive(value);
    }

    private void HideErrorPanel()
    {
        _errorPanel.SetActive(false);
    }

    public void ViewWinPanel(List<GameObject> enemys)
    {
        _enemys = enemys;
        UpdateEnemyCharks(_enemys[0].GetComponent<WarriorPerson>().ObrazWarrior);
        _winPanel.SetActive(true);
        _leftButton.interactable = false;
        _rightButton.interactable = _enemys.Count > 1;
    }

    public void OnLeftWinButtonClick()
    {
        if (_currentEnemy > 0)
        {
            _currentEnemy--;
            UpdateEnemyCharks(_enemys[_currentEnemy].GetComponent<WarriorPerson>().ObrazWarrior);
            _leftButton.interactable = _currentEnemy > 0;
            _rightButton.interactable = _currentEnemy < (_enemys.Count - 1);
        }
    }

    public void OnRightWinButtonClick()
    {
        if (_currentEnemy < (_enemys.Count - 1))
        {
            _currentEnemy++;
            UpdateEnemyCharks(_enemys[_currentEnemy].GetComponent<WarriorPerson>().ObrazWarrior);
            _leftButton.interactable = true;
            _rightButton.interactable = _currentEnemy < (_enemys.Count - 1);
        }
    }

    public void OnCreateWinButtonClick()
    {
        PlayersWarriors.Instance.AddingWarriorObraz(_enemys[_currentEnemy].GetComponent<WarriorPerson>().ObrazWarrior);
        _createButton.interactable = false;
    }

    public void OnMapWinButtonClick()
    {
        BattleLoss();
    }

    public void UpdateEnemyCharks(WarPersonObraz wpo)
    {
        _characteristicsUI.ViewCharacteristics(wpo.GenoWar, wpo.Title);
    }
}
