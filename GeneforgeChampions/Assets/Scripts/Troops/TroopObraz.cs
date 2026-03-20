using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class TroopObraz
{
    private int _troopID = -1;
    private string _nameTroop = "";
    private Vector3 _posTroop = Vector3.zero;
    private Vector3 _targetTroop = Vector3.zero;

    private List<WarPersonObraz> _warriors = new List<WarPersonObraz>();
    public int CountWarriors { get { return _warriors.Count; } }

    public int TroopID {  get { return _troopID; } }
    public string NameTroop { get { return _nameTroop; } }
    public Vector3 PositionTroop { get { return _posTroop; } }
    public Vector3 TargetTroop { get { return _targetTroop; } } 
    
    public TroopObraz() { }

    public TroopObraz(string csv, char sep = '=', char sep2 = ';')
    {
        string[] arr = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length >= 5)
        {
            if (int.TryParse(arr[0], out int a1)) _troopID = a1;
            _nameTroop = arr[1];
            _posTroop = CsvStringToVector3(arr[2]);
            _targetTroop = CsvStringToVector3(arr[3]);
            string[] arW = arr[4].Split(sep2, StringSplitOptions.RemoveEmptyEntries);
            _warriors.Clear();
            if (arW.Length > 0)
            {
                for (int i = 0; i < arW.Length; i++)
                {
                    _warriors.Add(new WarPersonObraz(arW[i], sep2));
                }
            }
        }
        else
        {
            _troopID = -1;
        }
    }

    public TroopObraz(int id, string nm, Vector3 pos, Vector3 tg)
    {
        _troopID = id;
        _nameTroop = nm;
        _posTroop = pos;
        _targetTroop = tg;
    }

    public void SetTarget(Vector3 tg)
    {
        _targetTroop = tg;
    }

    public void SetPosition(Vector3 pos)
    {
        _posTroop = pos;
    }

    public void AddExp(int exp)
    {
        foreach(WarPersonObraz obraz in _warriors)
        {
            obraz.AddExp(exp);
        }
    }

    public void AddWarObraz(WarPersonObraz wpo)
    {
        _warriors.Add(wpo);
    }

    public bool CheckWarObrazByID(int id)
    {
        for (int i = 0; i < _warriors.Count; i++)
        {
            if (_warriors[i].WarID == id)
            {
                return true;
            }
        }
        return false;
    }

    public bool RemoveWarObrazByID(int id)
    {
        for (int i = 0; i < _warriors.Count; i++)
        {
            if (_warriors[i].WarID == id)
            {
                _warriors.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public void RemoveWarObrazAt(int num)
    {
        _warriors.RemoveAt(num);
    }

    public WarPersonObraz[] GetWarriors()
    {
        return _warriors.ToArray();
    }

    public string ToCsvString(char sep = '=', char sep2 = ';')
    {
        StringBuilder sb = new StringBuilder($"{_troopID}{sep}{_nameTroop}{sep}{Vector3ToCsvString(_posTroop)}{sep}{Vector3ToCsvString(_targetTroop)}{sep}");
        for (int i = 0; i < _warriors.Count; i++) sb.Append($"{_warriors[i].ToCsvString()}{sep2}");
        sb.AppendLine($"{sep}");
        return sb.ToString();
    }

    public static Vector3 CsvStringToVector3(string csv, char sep = ';')
    {
        string[] ar = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (ar.Length >= 3)
        {
            if (float.TryParse(ar[0], out float fx) && float.TryParse(ar[1], out float fy) && float.TryParse(ar[2], out float fz))
            {
                return new Vector3(fx, fy, fz);
            }
        }
        return new Vector3(-1f, -1f, -1f);
    }

    public static string Vector3ToCsvString(Vector3 v3, char sep=';')
    {
        return $"{v3.x:F2}{sep}{v3.y:F2}{sep}{v3.z:F2}{sep}";
    }
}
