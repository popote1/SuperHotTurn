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
    public int Tag1;
    public int Tag2;
    public int Tag3;
    public bool IsWalable1=true;
    public bool IsWalable2=true;
    public bool IsWalable3=true;
    
}
