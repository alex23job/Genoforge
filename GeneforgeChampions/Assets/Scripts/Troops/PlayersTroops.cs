using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayersTroops : MonoBehaviour
{
    private string _loadingCsvTroopsString = "";
    private string _defaultSeparator = "#";

    private List<TroopObraz> _troopObrazs = new List<TroopObraz>();

    public int CountTroops { get { return _troopObrazs.Count; } }

    public static PlayersTroops Instance;

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

    public void SetTroopsCsvString(string csvString)
    {
        _loadingCsvTroopsString = csvString;
        CreateAllObrazs(csvString);
    }

    public void UpdateCsvString()
    {
        _loadingCsvTroopsString = TroopsToCsvString("#");
    }

    public void CreateAllObrazs(string csvString)
    {
        _loadingCsvTroopsString = csvString;
        if (_loadingCsvTroopsString == "") return;
        _troopObrazs.Clear();
        string[] ar = _loadingCsvTroopsString.Split(_defaultSeparator, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string troopCsv in ar)
        {
            //print($"troopCsv <{troopCsv}>");
            //int id = CarPassport.GetCarIDFromCsv(carCsv, "=");
            //GameObject prefabCar = PrefabsPak.Instance.GetCarPrefab(id - 1);
            //GameObject car = Instantiate(prefabCar);
            TroopObraz to = new TroopObraz(troopCsv, '$', '%');
            //CarPassportInfo carPassport = new CarPassportInfo(carCsv);
            //if (carPassport != null)
            //{
            //carPassport.SetParamsFromCsv(carCsv);
            //    carPassports.Add(carPassport);
            //}
            if (to.TroopID != -1) _troopObrazs.Add(to);
        }
        //print($"CreateAllPassports countPassports=<{carPassports.Count}>");
    }

    public TroopObraz[] GetAllObrazs()
    {
        return _troopObrazs.ToArray();
    }

    public void AddingWarriorToTroopByID(int id, WarPersonObraz wpo)
    {
        foreach(TroopObraz to in _troopObrazs)
        {
            if (to.TroopID == id)
            {
                to.AddWarObraz(wpo);
                _loadingCsvTroopsString = TroopsToCsvString("#");
                return;
            }
        }
    }

    public bool CheckWarriorByID(int warID)
    {
        foreach (TroopObraz to in _troopObrazs)
        {
            if (to.CheckWarObrazByID(warID))
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveWarriorByID(int warID)
    {
        foreach(TroopObraz to in _troopObrazs)
        {
            if (to.RemoveWarObrazByID(warID))
            {
                _loadingCsvTroopsString = TroopsToCsvString("#");
                return;
            }
        }        
    }

    public void RemoveTroopByID(int troopID)
    {
        for (int i = 0; i < _troopObrazs.Count; i++)
        {
            if (_troopObrazs[i].TroopID == troopID)
            {
                _troopObrazs.RemoveAt(i);
                _loadingCsvTroopsString = TroopsToCsvString("#");
                return;
            }
        }
    }

    public void AddingTroop(string nameTroop, Vector3 pos, Vector3 tg)
    {
        _troopObrazs.Add(new TroopObraz(GetNextTroopID(), nameTroop, pos, tg));
        _loadingCsvTroopsString = TroopsToCsvString();
    }

    public void AddingExpToTroopByID(int id, int exp)
    {
        foreach(TroopObraz to in _troopObrazs)
        {
            if (to.TroopID == id)
            {
                to.AddExp(exp);
                _loadingCsvTroopsString = TroopsToCsvString();
                return;
            }
        }
    }

    public void SetPositionByID(int id, Vector3 pos)
    {
        foreach (TroopObraz to in _troopObrazs)
        {
            if (to.TroopID == id)
            {
                to.SetPosition(pos);
                _loadingCsvTroopsString = TroopsToCsvString();
                return;
            }
        }
    }

    public void SetTargetByID(int id, Vector3 pos)
    {
        foreach (TroopObraz to in _troopObrazs)
        {
            if (to.TroopID == id)
            {
                to.SetTarget(pos);
                _loadingCsvTroopsString = TroopsToCsvString();
                return;
            }
        }
    }

    private int GetNextTroopID()
    {
        if (CountTroops > 0)
        {
            int maxID = 0;
            foreach (TroopObraz to in _troopObrazs)
            {
                if (to.TroopID > maxID) maxID = to.TroopID;
            }
            return maxID + 1;
        }
        return 1;

    }

    public string TroopsToCsvString(string sep = "#")
    {
        if (CountTroops > 0)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TroopObraz obraz in _troopObrazs)
            {
                sb.Append($"{obraz.ToCsvString('$', '%')}{sep}");
            }
            return sb.ToString();
        }
        return "";
    }

}
