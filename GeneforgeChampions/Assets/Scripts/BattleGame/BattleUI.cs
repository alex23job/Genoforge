using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private GameObject _hintPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTroopsClick()
    {
        SceneManager.LoadScene("TroopsScene");
    }

    public void OnEndDayClick()
    {
        GameManager.Instance.currentPlayer.DayComplete();
    }

    public void ViewHintPanel(bool value, string txtHint, Vector3 pos)
    {
        Text hintText = _hintPanel.transform.GetChild(0).GetComponent<Text>();
        if (hintText != null)
        {
            hintText.text = txtHint;
        }
        _hintPanel.SetActive(value);
        _hintPanel.transform.position = pos;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadWarriorsScene()
    {
        SceneManager.LoadScene("CreateWarriorsScene");
    }

    public void LoadBattlefieldScene()
    {
        SceneManager.LoadScene("BattlefieldScene");
    }
}
