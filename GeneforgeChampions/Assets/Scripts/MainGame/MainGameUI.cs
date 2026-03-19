using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _zastavka;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.currentPlayer.isZastavkaView) CloseZastavka();
        Invoke("CloseZastavka", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CloseZastavka()
    {
        _zastavka.SetActive(false);
        GameManager.Instance.currentPlayer.isZastavkaView = true;
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
        GameManager.Instance.SaveGame();
        Application.Quit();
    }
}
