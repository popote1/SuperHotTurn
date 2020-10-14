using System.Collections;
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

    public Vector3 GetWorldPositionCentreCell(int x, int z)
    {
        return _origine + new Vector3(x * _cellsize, 0, z * _cellsize) + new Vector3(_cellsize / 2, 0, _cellsize / 2);
    }
    public Vector3 GetWorldPositionCentreCell(Vector3 worldPos)
    {
        
        return _origine + new Vector3(GetXY(worldPos).x * _cellsize, 0, GetXY(worldPos).y * _cellsize) + new Vector3(_cellsize / 2, 0, _cellsize / 2);
    }

    public Vector2 GetXY(Vector3 worldPos)
    {
        return new Vector2((int) (worldPos - _origine).x / _cellsize, (int) (worldPos - _origine).z / _cellsize);
    }

    public PlayTile GetPlayTile(Vector3 worldPos)
    {
        return PlayTiles[(int) GetXY(worldPos).x, (int) GetXY(worldPos).y];
    }

    public void SetPlayTile(Vector3 worldPos, PlayTile playTile)
    {
        PlayTiles[(int) GetXY(worldPos).x, (int) GetXY(worldPos).y] = playTile;
    }

    public PlayTile GetTile(int x, int y)
    {
        return PlayTiles[x, y];
    }

    public bool CheckIfWalkeble(int x, int y)
    {
        
        if (!PlayTiles[x, y].IsWalable1) {return false; }
        if (!PlayTiles[x, y].IsWalable2) {return false;}
        if (!PlayTiles[x, y].IsWalable3) {return false;}
        Debug.Log("travail sur la casse"+ x +" , "+y+" et c'est bon");
        return true;
    }
    


    }
