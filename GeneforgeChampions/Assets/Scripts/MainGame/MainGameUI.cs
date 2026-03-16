using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewRecord()
    {

    }

    public void LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
