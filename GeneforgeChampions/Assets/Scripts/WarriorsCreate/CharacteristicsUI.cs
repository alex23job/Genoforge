using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsUI : MonoBehaviour
{
    [SerializeField] private Text[] _txtArrCharcs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewCharacteristics(Genofond gen)
    {
        int[] charcs = gen.GetCharcs();
        for (int i = 0; i < _txtArrCharcs.Length; i++)
        {
            if (i < charcs.Length)
            {
                _txtArrCharcs[i].text = charcs[i].ToString();
            }
        }

    }
}
