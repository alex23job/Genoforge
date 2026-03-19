using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WarriorsCreateUI : MonoBehaviour
{
    [SerializeField] private Button _btnWorking;
    [SerializeField] private Button _btnImprove;
    [SerializeField] private Image _imgArrows;
    [SerializeField] private GameObject _sphere;
    [SerializeField] private Text[] _arrRndTexts;
    [SerializeField] private GameObject _rndPanel;
    [SerializeField] private GameObject _newWarriorLeft;
    [SerializeField] private GameObject _newWarriorRight;
    [SerializeField] private SelectOldWarriorUI _selectPanelLeft;
    [SerializeField] private SelectOldWarriorUI _selectPanelRight;

    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private Text _hintText;
    [SerializeField] private Text _txtAttempts;

    private WarPersonObraz _oldWarriorLeft = null;
    private WarPersonObraz _oldWarriorRight = null;
    private WarPersonObraz _newWarLeft = null;
    private WarPersonObraz _newWarRight = null;

    // Start is called before the first frame update
    void Start()
    {
        ResetPanels();
        _btnImprove.interactable = false;
        _btnWorking.interactable = false;
        ViewAttempts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ViewAttempts()
    {
        _txtAttempts.text = $"Попытки : {GameManager.Instance.currentPlayer.countAttempts}";
    }

    public void OnImproveClick()
    {
        string[] rndStr = { "2 : 2", "3 : 1", "1 : 3" };
        string[] nameWarriors = { "Воин", "Стрелок", "Маг" };
        int i;
        if ((_oldWarriorLeft != null) && (_oldWarriorRight != null))
        {
            _btnImprove.gameObject.SetActive(false);
            //print($"left={_oldWarriorLeft.ToCsvString()} right={_oldWarriorRight.ToCsvString()}");
            int[] arrSum = new int[_arrRndTexts.Length];
            int[] nwcl = new int[_arrRndTexts.Length];
            int[] nwcr = new int[_arrRndTexts.Length];
            int[] wcl = _oldWarriorLeft.GenoWar.GetCharcs();
            int[] wcr = _oldWarriorRight.GenoWar.GetCharcs();
            for (i = 0; i < _arrRndTexts.Length; i++)
            {
                _arrRndTexts[i].text = "";
                if ((i < wcl.Length) && (i < wcr.Length)) arrSum[i] = wcl[i] + wcr[i];
                nwcl[i] = Mathf.CeilToInt(arrSum[i] * 3f / 4f);


                ViewingRndResult vrr = _arrRndTexts[i].gameObject.GetComponent<ViewingRndResult>();
                if (vrr != null)
                {
                    vrr.SetParams(0.5f * i, rndStr[1]);
                }
            }
            _newWarLeft = new WarPersonObraz(0, nameWarriors[0], 0, new Genofond(nwcl[0], nwcl[1], nwcl[2], nwcl[3], nwcl[4], nwcl[5], nwcl[6], nwcl[7]), _oldWarriorRight.Exp);
            PlayersWarriors.Instance.UpdateWarriorByID(_oldWarriorLeft.WarID, _newWarLeft);
            PlayersWarriors.Instance.RemoveWarriorByID(_oldWarriorRight.WarID);

            _rndPanel.SetActive(true);
            Invoke("ViewImproveResult", 4.1f);
        }
    }

    private void ViewImproveResult()
    {
        _selectPanelLeft.ResetPanel();
        _selectPanelRight.ResetPanel();
        _rndPanel.SetActive(false);
        _btnImprove.gameObject.SetActive(true);
        _btnImprove.interactable = false;
        _btnWorking.interactable = false;
        _oldWarriorRight = null;
        _hintPanel.SetActive(true);
    }

    public void OnCreateWarriorsClick()
    {
        if (GameManager.Instance.currentPlayer.countAttempts == 0) return;
        string[] rndStr = { "2 : 2", "3 : 1", "1 : 3" };
        string[] nameWarriors = { "Воин", "Стрелок", "Маг"};
        int i;
        if ((_oldWarriorLeft != null) && (_oldWarriorRight != null))
        {
            GameManager.Instance.currentPlayer.countAttempts--;
            ViewAttempts();
            if (GameManager.Instance.currentPlayer.countAttempts == 0) _btnWorking.gameObject.SetActive(false);
            _btnImprove.gameObject.SetActive(false);
            //print($"left={_oldWarriorLeft.ToCsvString()} right={_oldWarriorRight.ToCsvString()}");
            int[] rnd = new int[_arrRndTexts.Length];
            int[] arrSum = new int[_arrRndTexts.Length];
            int[] nwcl = new int[_arrRndTexts.Length];
            int[] nwcr = new int[_arrRndTexts.Length];
            int[] wcl = _oldWarriorLeft.GenoWar.GetCharcs();
            int[] wcr = _oldWarriorRight.GenoWar.GetCharcs();
            for (i = 0; i < _arrRndTexts.Length; i++)
            {
                rnd[i] = Random.Range(0, 3);
                _arrRndTexts[i].text = "";
                if ((i < wcl.Length) && (i < wcr.Length)) arrSum[i] = wcl[i] + wcr[i];

                if (rnd[i] == 0)
                {
                    nwcl[i] = wcl[i]; nwcr[i] = wcr[i];
                }
                else if (rnd[i] == 1)
                {
                    nwcl[i] = Mathf.RoundToInt(arrSum[i] * 3f / 4f);
                    nwcr[i] = Mathf.RoundToInt(arrSum[i] / 4f);
                    if (nwcr[i] < 1) nwcr[i] = 1;
                }
                else
                {
                    nwcr[i] = Mathf.RoundToInt(arrSum[i] * 3f / 4f);
                    nwcl[i] = Mathf.RoundToInt(arrSum[i] / 4f);
                    if (nwcl[i] < 1) nwcl[i] = 1;
                }
                ViewingRndResult vrr = _arrRndTexts[i].gameObject.GetComponent<ViewingRndResult>();
                if (vrr != null)
                {
                    vrr.SetParams(0.5f * i, rndStr[rnd[i]]);
                }
            }
            string nm = nameWarriors[0];
            int tp = 0;
            if ((nwcl[3] > nwcl[0]) && (nwcl[3] > nwcl[7])) { nm = nameWarriors[1]; tp = 1; }
            if ((nwcl[7] > nwcl[0]) && (nwcl[7] > nwcl[3])) { nm = nameWarriors[2]; tp = 2; }
            _newWarLeft = new WarPersonObraz(0, nm, tp, new Genofond(nwcl[0], nwcl[1], nwcl[2], nwcl[3], nwcl[4], nwcl[5], nwcl[6], nwcl[7]));
            nm = nameWarriors[0];
            tp = 0;
            if ((nwcr[3] > nwcr[0]) && (nwcr[3] > nwcr[7])) { nm = nameWarriors[1]; tp = 1; }
            if ((nwcr[7] > nwcr[0]) && (nwcr[7] > nwcr[3])) { nm = nameWarriors[2]; tp = 2; }
            _newWarRight = new WarPersonObraz(0, nm, tp, new Genofond(nwcr[0], nwcr[1], nwcr[2], nwcr[3], nwcr[4], nwcr[5], nwcr[6], nwcr[7]));

            _rndPanel.SetActive(true);
            Invoke("ViewResult", 4.1f);
        }
    }

    private void ViewResult()
    {
        _imgArrows.gameObject.SetActive(true);
        _newWarriorLeft.SetActive(true);
        CharacteristicsUI charcsLeft = _newWarriorLeft.GetComponent<CharacteristicsUI>();
        if (charcsLeft != null )
        {
            charcsLeft.ViewCharacteristics(_newWarLeft.GenoWar, _newWarLeft.Title);
        }
        _newWarriorRight.SetActive(true);
        CharacteristicsUI charcsRight = _newWarriorRight.GetComponent<CharacteristicsUI>();
        if (charcsRight != null)
        {
            charcsRight.ViewCharacteristics(_newWarRight.GenoWar, _newWarRight.Title);
        }
        PlayersWarriors.Instance.AddingWarriorObraz(_newWarLeft);
        PlayersWarriors.Instance.AddingWarriorObraz(_newWarRight);
        _selectPanelLeft.ResetPanel();
        _selectPanelRight.ResetPanel();
        Invoke("ResetPanels", 10f);
    }

    private void ResetPanels()
    {
        _rndPanel.SetActive(false);
        _newWarriorLeft.SetActive(false);
        _newWarriorRight.SetActive(false);
        _imgArrows.gameObject.SetActive(false);
        _hintPanel.SetActive(true);
        _btnImprove.gameObject.SetActive(true);
    }

    public void CreatingExit()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void SetOldWarrior(int num, WarPersonObraz obraz)
    {
        //print($"num={num}  obraz={obraz.ToCsvString()}");
        if (num == 1) _oldWarriorLeft = obraz;
        if (num == 2) _oldWarriorRight = obraz;
        if ((_oldWarriorLeft != null) && (_oldWarriorRight != null))
        {
            _hintPanel.SetActive(false);
            _btnWorking.interactable = true;
            if (_oldWarriorLeft.WarID != _oldWarriorRight.WarID) _btnImprove.interactable = true;
        }
    }
}
