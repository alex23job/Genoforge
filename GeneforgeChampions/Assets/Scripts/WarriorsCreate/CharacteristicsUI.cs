using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsUI : MonoBehaviour
{
    [SerializeField] private Text _txtTitle;
    [SerializeField] private Text[] _txtArrCharcs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewCharacteristics(Genofond gen, string title)
    {
        _txtTitle.text = title;
        int[] charcs = gen.GetCharcs();
        for (int i = 0; i < _txtArrCharcs.Length; i++)
        {
            if (i < charcs.Length)
            {
                if (i == 0)
                {
                    int power = 5 * charcs[i];
                    //print($"charcs[{i}]={charcs[i]}   power={power}");
                    _txtArrCharcs[i].text = $"{power}";
                }
                else _txtArrCharcs[i].text = charcs[i].ToString();
            }
        }

    }
}
