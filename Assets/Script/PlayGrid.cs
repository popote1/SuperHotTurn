using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayGrid
{
    public int _hight;
    public int Hight {
        get => _hight;
    }
    public int _width;

    public int Width {
        get => _width;
    }

    public string TileTemple;
    public PlayTile[,] PlayTiles;
    public float _cellsize;
    [NonSerialized]
    private Vector3 _origine;

    private float _oriX;
    private float _oriY;
    private float _oriZ;

    public PlayGrid(int hight, int width, float cellsize, Vector3 origine)
    {
        PlayTiles = new PlayTile[hight,width];
        _hight = hight;
        _width = width;
        _cellsize = cellsize;
        _origine = origine;
        _oriX = origine.x;
        _oriY = origine.y;
        _oriZ = origine.z;
        for (int x = 0; x < hight; x++)
        {
            for (int y = 0; y < width; y++)
            {
                
                PlayTiles[x,y] =new PlayTile();
                //PlayTiles[x, y].Tag1 = 50;
                //PlayTiles[x, y].Tag2 = 50;
                //PlayTiles[x, y].Tag3 = 50;
            }
            
        }
    }

   

    public Vector3 GetWorldPosition(int x, int z)
    {
        return _origine + new Vector3(x * _cellsize, 0, z * _cellsize);
    }

    public Vector3 GetWorldPositionCentreCell(Vector2Int gridPos)
    {
        return _origine + new Vector3(gridPos.x * _cellsize, 0, gridPos.y * _cellsize) + new Vector3(_cellsize / 2, 0, _cellsize / 2);
    }
    public Vector3 GetWorldPositionCentreCell(int x, int z)
    {
        return _origine + new Vector3(x * _cellsize, 0, z * _cellsize) + new Vector3(_cellsize / 2, 0, _cellsize / 2);
    }
    public Vector3 GetWorldPositionCentreCell(Vector3 worldPos)
    {
        
        return _origine + new Vector3(GetXY(worldPos).x * _cellsize, 0, GetXY(worldPos).y * _cellsize) + new Vector3(_cellsize / 2, 0, _cellsize / 2);
    }

    public Vector2Int GetXY(Vector3 worldPos)
    {
        return new Vector2Int((int)( (worldPos - _origine).x / _cellsize), (int)((worldPos - _origine).z / _cellsize));
    }

    public PlayTile GetPlayTile(Vector3 worldPos)
    {
        return PlayTiles[(int) GetXY(worldPos).x, (int) GetXY(worldPos).y];
    }

    public void SetPlayTile(Vector3 worldPos, PlayTile playTile)
    {
        PlayTiles[(int) GetXY(worldPos).x, (int) GetXY(worldPos).y] = playTile;
    }

    public PlayTile GetPlayTile(int x, int y)
    {
        return PlayTiles[x, y];
    }

    public PlayTile GetPlayTile(Vector2Int gridpos)
    {
        //Debug.Log("les valeurs sont de x "+gridpos.x+" et y "+gridpos.y);
        return PlayTiles[gridpos.x, gridpos.y];
    }

    public Vector3 GetGridOrinie()
    {
        return new Vector3(_oriX,_oriY,_oriZ);
    }

    public bool CheckIfWalkeble(int x, int y)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _hight) return false;
        if (!PlayTiles[x, y].IsWalable1) {return false; }
        if (!PlayTiles[x, y].IsWalable2) {return false;}
        if (!PlayTiles[x, y].IsWalable3) {return false;}
        
        if (PlayTiles[x, y].GridActor != null) { return false;}
       // Debug.Log("travail sur la casse"+ x +" , "+y+" et c'est bon et l'obet sur la case est"+PlayTiles[x,y].GridActor.name);
        return true;
    }
        


    }
