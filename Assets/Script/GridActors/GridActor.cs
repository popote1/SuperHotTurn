using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridActor : MonoBehaviour
{
    public GameObject playGidHolder;
    public bool IsWakebel;
    public PlayGrid PlayGrid;
    public Vector2Int _currentPos=new Vector2Int(0,0);
    protected Vector3 _newPos;
    
    
    public void SetGridPos(Vector2Int gridPos)
    {
        _currentPos = gridPos;
    }

}
