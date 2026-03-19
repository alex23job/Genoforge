using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour
{
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

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadWarriorsScene()
    {
        SceneManager.LoadScene("CreateWarriorsScene");
    }
}
