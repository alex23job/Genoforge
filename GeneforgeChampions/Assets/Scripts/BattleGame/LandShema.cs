using System;
using System.Collections.Generic;

[Serializable]
public class LandShema
{
    private string _nameLand;
    private List<SimpleLand> _lands = new List<SimpleLand>();
    private List<SimpleBuild> _builds = new List<SimpleBuild>();

    public LandShema() { }
    public LandShema(string name) 
    {
        _nameLand = name;
    }

    public string NameLandShema {  get { return _nameLand; } }

    public List<int> GetLands()
    {
        List<int> res = new List<int>();
        foreach (SimpleLand land in _lands) res.Add(land.Land);
        return res;
    }
    public List<int> GetBuilds()
    {
        List<int> res = new List<int>();
        foreach (SimpleBuild build in _builds) res.Add(build.Build);
        return res;
    }

    public void AddLandCeil(SimpleLand sl)
    {
        _lands.Add(sl);
    }

    public void AddBuild(SimpleBuild sb)
    {
        _builds.Add(sb);
    }
}

[Serializable]
public class SimpleLand
{
    private int _col; // landID & 0xff;
    private int _row; // (landID >> 8) & 0xff;
    private int _id; // (landID >> 16) & 0x3f;
    private int _rot; // (landID >> 22) & 0x3;

    public SimpleLand() { }
    public SimpleLand(int row, int col, int id, int rot = 0)
    {
        _col = col;
        _row = row;
        _id = id;
        _rot = rot;
    }

    public int Col { get { return _col; } }
    public int Row { get { return _row; } }
    public int LandID { get { return _id; } }
    public int Rot { get { return _rot; } }
    public int Land
    {
        get
        {
            return (_rot << 22) + (_id << 16) + (_row << 8) + (_col);
        }
    }

    public override string ToString()
    {
        return $"ID={_id} Rot={_rot * 90} гр.  Row={_row} Col={_col}    Land={Land}";
    }
}

[Serializable]
public class SimpleBuild
{
    private int _col; // landID & 0xff;
    private int _row; // (landID >> 8) & 0xff;
    private int _id; // (landID >> 16) & 0x3f;
    private int _rot; // (landID >> 22) & 0x3;

    public SimpleBuild() { }
    public SimpleBuild(int row, int col, int id, int rot = 0)
    {
        this._col = col;
        this._row = row;
        this._id = id;
        this._rot = rot;
    }

    public int Col { get { return _col; } }
    public int Row { get { return _row; } }
    public int BuildID { get { return _id; } }
    public int Rot { get { return _rot; } }
    public int Build
    {
        get
        {
            return (_rot << 22) + (_id << 16) + (_row << 8) + (_col);
        }
    }

    public override string ToString()
    {
        return $"ID={_id} Rot={_rot * 90} гр.  Row={_row} Col={_col}    Build={Build}";
    }
}
