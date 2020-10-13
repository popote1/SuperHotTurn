using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;
using UnityEngine.Tilemaps;

public class TileMapSetter : MonoBehaviour
{
    public RuleTile tile;
    public Grid GridE ;
    public Tilemap _tilemap1;
    public Tilemap _tilemap2;
    public Tilemap _tilemap3;
    public bool EditMode;
    public GameObject EditeCursor;
    public List<PlayTile> PlayTiles;
    
    
    private PlayGridHolder _PlayGridHolder;
    private TilemapRenderer _tilemapRenderer;
    private GameObject _cursor;


    void Start()
    {
        _PlayGridHolder = GetComponent<PlayGridHolder>();
        
        //_tilemap = GridE.GetComponentInChildren<Tilemap>();
        //_tilemapRenderer = GridE.GetComponentInChildren<TilemapRenderer>();
        _tilemap1.SetTile(Vector3Int.zero,tile);
        _tilemap1.SetTile(new Vector3Int(0,1,0),tile );
        _tilemap1.SetTile(new Vector3Int(-1,1,0),tile );
        _tilemap1.SetTile(new Vector3Int(1,1,0),tile );
        _tilemap1.SetTile(new Vector3Int(-1,2,0),tile );
        _tilemap1.SetTile(new Vector3Int(0,2,0),tile );
        _tilemap1.SetTile(new Vector3Int(1,2,0),tile );
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (EditMode)
        {
            Vector3 mouseWorldPos= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            if (_cursor == null)
            {
                _cursor=Instantiate(EditeCursor);
            }
            _cursor.transform.position = _PlayGridHolder.PlayGrid.GetWorldPositionCentreCell(mouseWorldPos);

            if (Input.GetButton("Fire1"))
            {
                PlayTile chosetile = PlayTiles[0];
                if (_PlayGridHolder.PlayGrid.GetPlayTile(mouseWorldPos)== null)
                {
                    Vector2 GridPos = _PlayGridHolder.PlayGrid.GetXY(mouseWorldPos);
                    _tilemap1.SetTile(new Vector3Int((int)GridPos.x,(int)GridPos.y,0),chosetile.RuleTile );
                }
            }
        }
        else
        {
            if (_cursor != null)
            {
                Destroy(_cursor);
            }
        }
    }
}
