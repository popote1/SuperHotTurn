using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;
using UnityEngine.Tilemaps;

public class TileMapSetter : MonoBehaviour
{
    public RuleTile tile;
    public Grid GridE ;
    private Tilemap _tilemap;
    private TilemapRenderer _tilemapRenderer;
    
    
    void Start()
    {
        _tilemap = GridE.GetComponentInChildren<Tilemap>();
        _tilemapRenderer = GridE.GetComponentInChildren<TilemapRenderer>();
        _tilemap.SetTile(Vector3Int.zero,tile);
        _tilemap.SetTile(new Vector3Int(0,1,0),tile );
        _tilemap.SetTile(new Vector3Int(-1,1,0),tile );
        _tilemap.SetTile(new Vector3Int(1,1,0),tile );
        _tilemap.SetTile(new Vector3Int(-1,2,0),tile );
        _tilemap.SetTile(new Vector3Int(0,2,0),tile );
        _tilemap.SetTile(new Vector3Int(1,2,0),tile );
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
