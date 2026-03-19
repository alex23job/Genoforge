using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WarPersonObraz
{
    private int _warID;
    private string _nameWarrior;
    private int _type;
    private int _exp;
    private Genofond _genofond;

    public int WarID { get => _warID; }
    public string NameWarrior { get => _nameWarrior; }
    public int Type { get => _type; }
    public int Exp { get => _exp; }
    public Genofond GenoWar { get => _genofond; }

    public string Title
    {
        get 
        {
            string strNum = (_warID > 0) ? _warID.ToString() : "";
            return $"{_nameWarrior} {strNum}"; 
        }
    }

    public WarPersonObraz() { }

    public WarPersonObraz(int id, string nm, int tp, Genofond gen, int exp = 0)
    {
        _warID = id;
        _nameWarrior = nm;
        _type = tp;
        _exp = exp;
        _genofond = new Genofond(gen._power, gen._endurance, gen._intellect, gen._accuracy, gen._rage, gen._regeneration, gen._protection, gen._magic);
    }
    public WarPersonObraz(string csv, char sep = '=')
    {
        string[] arr = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length >= 5)
        {
            if (int.TryParse(arr[0], out int a1)) _warID = a1;
            _nameWarrior = arr[1];
            if (int.TryParse(arr[2], out int a2)) _type = a2;
            if (int.TryParse(arr[3], out int a3)) _exp = a3;
            _genofond = new Genofond(arr[4]);
        }
        else
        {
            _warID = -1;
            _genofond = new Genofond(1, 20, 1, 1, 1, 1, 1, 1);
        }
    }

    public void AddZnGen(int numGen, int value)
    {
        switch(numGen)
        {
            case 1: _genofond._power += value; break;
            case 2: _genofond._endurance += value; break;
            case 3: _genofond._intellect += value; break;
            case 4: _genofond._accuracy += value; break;
            case 5: _genofond._rage += value; break;
            case 6: _genofond._regeneration += value; break;
            case 7: _genofond._protection += value; break;
            case 8: _genofond._magic += value; break;
        }
    }

    public void AddExp(int exp)
    {
        _exp += exp;
    }

    public void CopyParams(WarPersonObraz wpo)
    {
        _exp += wpo._exp;
        _genofond._power = wpo._genofond._power;
        _genofond._endurance = wpo._genofond._endurance;
        _genofond._intellect = wpo._genofond._intellect;
        _genofond._accuracy = wpo._genofond._accuracy;
        _genofond._rage = wpo._genofond._rage;
        _genofond._regeneration = wpo._genofond._regeneration;
        _genofond._protection = wpo._genofond._protection;
        _genofond._magic = wpo._genofond._magic;
    }

    public string ToCsvString(char sep = '=')
    {
        return $"{_warID}{sep}{_nameWarrior}{sep}{_type}{sep}{_exp}{sep}{_genofond.ToCsvString()}{sep}";
    }
}

[Serializable]
public class Genofond
{
    public int _power;
    public int _endurance;
    public int _intellect;
    public int _accuracy;
    public int _rage;
    public int _regeneration;
    public int _protection;
    public int _magic;

    public Genofond(int pw, int end, int um, int acc, int rg, int reg, int prot, int mg)
    {
        _power = pw;
        _endurance = end;
        _intellect = um;
        _accuracy = acc;
        _rage = rg;
        _regeneration = reg;
        _protection = prot;
        _magic = mg;
    }

    public Genofond(string csv, char sep = ';')
    {
        string[] arr = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (arr.Length >= 8)
        {
            if (int.TryParse(arr[0], out int a1)) _power = a1; else _power = 1;
            if (int.TryParse(arr[1], out int a2)) _endurance = a2; else _endurance = 20;
            if (int.TryParse(arr[2], out int a3)) _intellect = a3; else _intellect = 1;
            if (int.TryParse(arr[3], out int a4)) _accuracy = a4; else _accuracy = 1;
            if (int.TryParse(arr[4], out int a5)) _rage = a5; else _rage = 1;
            if (int.TryParse(arr[5], out int a6)) _regeneration = a6; else _regeneration = 1;
            if (int.TryParse(arr[6], out int a7)) _protection = a7; else _protection = 1;
            if (int.TryParse(arr[7], out int a8)) _magic = a8; else _magic = 1;
            //Debug.Log($"arr[0]={arr[0]}  a1={a1}  _power={_power}");
        }
        else
        {
            _power = 1;
            _endurance = 20;
            _intellect = 1;
            _accuracy = 1;
            _rage = 1;
            _regeneration = 1;
            _protection = 1;
            _magic = 1;
        }
    }

    public int[] GetCharcs()
    {
        List<int> res = new List<int>();
        res.Add(_power);
        res.Add(_endurance);
        res.Add(_intellect);
        res.Add(_accuracy);
        res.Add(_rage);
        res.Add(_regeneration);
        res.Add(_protection);
        res.Add(_magic);
        return res.ToArray();
    }

    public string ToCsvString(char sep = ';')
    {
        return $"{_power}{sep}{_endurance}{sep}{_intellect}{sep}{_accuracy}{sep}{_rage}{sep}{_regeneration}{sep}{_protection}{sep}{_magic}{sep}";
    }
}

