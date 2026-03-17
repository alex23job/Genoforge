using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayersWarriors : MonoBehaviour
{
    private string _loadingCsvWarriorsString = "";
    private string _defaultSeparator = "#";

    private List<WarPersonObraz> _personObrazs = new List<WarPersonObraz>();

    public int CountWarriors { get { return _personObrazs.Count; } }

    public static PlayersWarriors Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetWarriorsCsvString(string csvString)
    {
        _loadingCsvWarriorsString = csvString;
    }

    public void AddingWarriorObraz(WarPersonObraz wpo)
    {
        WarPersonObraz newWpo = new WarPersonObraz(GenerateNextWarriorID(), wpo.NameWarrior, wpo.Type, wpo.GenoWar, wpo.Exp);
        _personObrazs.Add(newWpo);
        _loadingCsvWarriorsString = WarriorsToCsvString("#");
        print($"3) AddingWarriorObraz CountWarriors={CountWarriors} WarID={newWpo.WarID} war={newWpo.NameWarrior} csv={_loadingCsvWarriorsString}");
    }

    private int GenerateNextWarriorID()
    {
        if (CountWarriors > 0)
        {
            int maxID = 0;
            foreach(WarPersonObraz wpo in _personObrazs)
            {
                if (wpo.WarID > maxID) maxID = wpo.WarID;
            }
            return maxID + 1;
        }
        return 1;
    }

    public void CreateAllObrazs(string csvString)
    {
        _loadingCsvWarriorsString = csvString;
        if (_loadingCsvWarriorsString == "") return;
        _personObrazs.Clear();
        string[] ar = _loadingCsvWarriorsString.Split(_defaultSeparator, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string warCsv in ar)
        {
            //int id = CarPassport.GetCarIDFromCsv(carCsv, "=");
            //GameObject prefabCar = PrefabsPak.Instance.GetCarPrefab(id - 1);
            //GameObject car = Instantiate(prefabCar);
            WarPersonObraz wpo = new WarPersonObraz(warCsv, '=');
            //CarPassportInfo carPassport = new CarPassportInfo(carCsv);
            //if (carPassport != null)
            //{
                //carPassport.SetParamsFromCsv(carCsv);
            //    carPassports.Add(carPassport);
            //}
            if (wpo.WarID != -1) _personObrazs.Add(wpo);
        }
        //print($"CreateAllPassports countPassports=<{carPassports.Count}>");
    }

    public string WarriorsToCsvString(string sep = "#")
    {
        if (CountWarriors > 0)
        {
            StringBuilder sb = new StringBuilder();
            foreach (WarPersonObraz obraz in _personObrazs)
            {
                sb.Append($"{obraz.ToCsvString('=')}{sep}");
            }
            return sb.ToString();
        }
        return "";
    }

}
