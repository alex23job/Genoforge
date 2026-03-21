using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Recorder.OutputPath;

public class BattleLand : MonoBehaviour
{
    [SerializeField] private GameObject _troopPrefab;
    [SerializeField] private GameObject[] _prefabLands;
    [SerializeField] private GameObject[] _prefabBuilds;
    [SerializeField] private float _ofsX = -30f;
    [SerializeField] private float _ofsY = 13f;
    [SerializeField] private Vector3 _outCastle;
    [SerializeField] private GameObject[] _prefabWarriors;

    private LandShema _landShema = null;
    private GameObject[] _lands;
    private int[] _pole;
    List<int> _builds = new List<int>();
    List<int> _grids = new List<int>();
    private int _nRows = 27;
    private int _nCols = 61;

    private List<GameObject> _troops = new List<GameObject>();

    private BattleLand _battleLand = null;

    private void Awake()
    {
        _battleLand = GetComponent<BattleLand>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateLandShema();
        CreatePole();
        CreateTroops();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateTroops()
    {
        TroopObraz[] troopObrazs = PlayersTroops.Instance.GetAllObrazs();
        foreach(TroopObraz obraz in troopObrazs)
        {
            GameObject troop = Instantiate(_troopPrefab);
            if (obraz.PositionTroop.y == 0)
            {   //  отряд ещё не был использован => новый !!!
                troop.transform.position = _outCastle;
                PlayersTroops.Instance.SetPositionByID(obraz.TroopID, _outCastle);
            }
            else
            {
                troop.transform.position = obraz.PositionTroop;
            }
            Troop troopControl = troop.GetComponent<Troop>();
            if (troopControl != null)
            {
                troopControl.SetObraz(obraz, _battleLand);
            }
            _troops.Add(troop);
        }
    }

    public GameObject GetWarriorsPrefabByTypeID(int typeID)
    {
        foreach(GameObject prefab in _prefabWarriors)
        {
            WarriorPerson wp = prefab.GetComponent<WarriorPerson>();
            if (wp != null)
            {
                if (wp.WarriorType == typeID) return prefab;
            }
        }
        return null;
    }

    private void CreateLandShema()
    {
        _landShema = new LandShema("BaseLandShema");
        _landShema.AddLandCeil(new SimpleLand(15, 18, 1, 1));
        _landShema.AddLandCeil(new SimpleLand(15, 19, 1, 1));
        _landShema.AddLandCeil(new SimpleLand(15, 20, 3, 0));
        _landShema.AddLandCeil(new SimpleLand(14, 20, 1, 0));
        _landShema.AddLandCeil(new SimpleLand(16, 20, 1, 0));
        _landShema.AddLandCeil(new SimpleLand(15, 21, 1, 1));
        _landShema.AddLandCeil(new SimpleLand(15, 22, 1, 1));
        _landShema.AddLandCeil(new SimpleLand(15, 23, 2, 2));
        _landShema.AddLandCeil(new SimpleLand(16, 23, 2, 0));
        _landShema.AddLandCeil(new SimpleLand(16, 24, 1, 1));
        _landShema.AddLandCeil(new SimpleLand(16, 25, 4, 3));
        _landShema.AddLandCeil(new SimpleLand(16, 26, 1, 1));
        _landShema.AddLandCeil(new SimpleLand(16, 27, 2, 3));
        //  средний верх
        _landShema.AddLandCeil(new SimpleLand(15, 27, 2, 1));
        _landShema.AddLandCeil(new SimpleLand(15, 28, 2, 3));

        //  средний низ
        _landShema.AddLandCeil(new SimpleLand(17, 25, 1, 0));
        _landShema.AddLandCeil(new SimpleLand(18, 25, 2, 0));
        _landShema.AddLandCeil(new SimpleLand(18, 26, 2, 2));
        _landShema.AddLandCeil(new SimpleLand(19, 26, 2, 0));

        //  самый верх
        _landShema.AddLandCeil(new SimpleLand(13, 20, 2, 1));
        _landShema.AddLandCeil(new SimpleLand(13, 21, 1, 1));
        _landShema.AddLandCeil(new SimpleLand(13, 22, 2, 3));
        _landShema.AddLandCeil(new SimpleLand(12, 22, 2, 1));
        _landShema.AddLandCeil(new SimpleLand(12, 23, 2, 3));
        _landShema.AddLandCeil(new SimpleLand(11, 23, 2, 1));
        _landShema.AddLandCeil(new SimpleLand(11, 24, 2, 3));
        _landShema.AddLandCeil(new SimpleLand(10, 24, 2, 1));
        _landShema.AddLandCeil(new SimpleLand(10, 25, 2, 3));
        _landShema.AddLandCeil(new SimpleLand(9, 25, 2, 1));
        _landShema.AddLandCeil(new SimpleLand(9, 26, 2, 3));

        //  самый низ
        _landShema.AddLandCeil(new SimpleLand(17, 20, 1, 0));
        _landShema.AddLandCeil(new SimpleLand(18, 20, 2, 0));
        _landShema.AddLandCeil(new SimpleLand(18, 21, 1, 1));
        _landShema.AddLandCeil(new SimpleLand(18, 22, 2, 2));
        _landShema.AddLandCeil(new SimpleLand(19, 22, 2, 0));
        _landShema.AddLandCeil(new SimpleLand(19, 23, 2, 2));
        _landShema.AddLandCeil(new SimpleLand(20, 23, 2, 0));
        _landShema.AddLandCeil(new SimpleLand(20, 24, 2, 2));
        _landShema.AddLandCeil(new SimpleLand(21, 24, 2, 0));
        _landShema.AddLandCeil(new SimpleLand(21, 25, 2, 2));
        _landShema.AddLandCeil(new SimpleLand(22, 25, 2, 0));
        _landShema.AddLandCeil(new SimpleLand(22, 26, 2, 2));

        //  точки для сражений
        _landShema.AddBuild(new SimpleBuild(12, 21, 0));
        _landShema.AddBuild(new SimpleBuild(16, 21, 1));
        _landShema.AddBuild(new SimpleBuild(14, 27, 2));
        _landShema.AddBuild(new SimpleBuild(19, 25, 3));
    }

    private void CreatePole()
    {
        int i, j;
        _pole = new int[_nCols * _nRows];
        _lands = new GameObject[_nCols * _nRows];
        Vector3 pos = Vector3.zero;
        if (_landShema != null)
        {
            _builds = _landShema.GetBuilds();
            _grids = _landShema.GetLands();
        }
        foreach(int landID in _grids)
        {
            int col = landID & 0xff;
            int row = (landID >> 8) & 0xff;
            int id = (landID >> 16) & 0x3f;
            int rot = (landID >> 22) & 0x3;
            pos.x = _ofsX + col;
            pos.z = _ofsY - row;
            GameObject land = Instantiate(_prefabLands[id], pos, Quaternion.identity);
            if (rot > 0) for (i = 0; i < rot; i++) land.transform.Rotate(0, 90, 0, Space.World);
            _lands[row * _nCols + col] = land;
            _pole[row * _nCols + col] = id;
        }
        for (i = 0; i < _nRows; i++)
        {
            for (j = 0; j < _nCols; j++)
            {
                if (_pole[i * _nCols + j] == 0)
                {
                    pos.x = _ofsX + j;
                    pos.z = _ofsY - i;
                    GameObject land = Instantiate(_prefabLands[0], pos, Quaternion.identity);
                    _lands[i * _nCols + j] = land;
                }
            }
        }
        pos.y = 0.12f;
        foreach(int buildID in _builds)
        {
            int col = buildID & 0xff;
            int row = (buildID >> 8) & 0xff;
            int id = (buildID >> 16) & 0x3f;
            int rot = (buildID >> 22) & 0x3;
            pos.x = _ofsX + col;
            pos.z = _ofsY - row;
            GameObject build = Instantiate(_prefabBuilds[id], pos, Quaternion.identity);
            if (rot > 0) for (i = 0; i < rot; i++) build.transform.Rotate(0, 90, 0, Space.World);
            //_lands[row * _nCols + col] = land;
            //_pole[row * _nCols + col] = id;
        }
    }
}
