﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid
{
    private int _hight;
    public int Hight {
        get => _hight;
    }
    private int _width;

    public int Width {
        get => _width;
    }

    private PlayTile[,] PlayTiles;
    private float _cellsize;
    private Vector3 _origine;

    public PlayGrid(int hight, int width, float cellsize, Vector3 origine)
    {
        PlayTiles = new PlayTile[hight,width];
        _hight = hight;
        _width = width;
        _cellsize = cellsize;
        _origine = origine;
    }

   

    public Vector3 GetWorldPosition(int x, int z)
    {
        return _origine + new Vector3(x * _cellsize, 0, z * _cellsize);
    }

    public Vector3 GetWorldPositionCentreCell(int x, int z)
    {
        return _origine + new Vector3(x * _cellsize, 0, z * _cellsize) + new Vector3(_cellsize / 2, 0, _cellsize / 2);
    }
    

}
