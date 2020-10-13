using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class PlayTile
{
    public int CellType;
    public int XPos;
    public int ZPos;
    public string Name;
    public bool IsWalable1=true;
    public bool IsWalable2=true;
    public bool IsWalable3=true;
    public bool UsingRuleTile;
    public Tile Tile;
    public RuleTile RuleTile;
}
