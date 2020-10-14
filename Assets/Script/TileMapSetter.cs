using System;
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
    public GameObject EditPanel;
    public bool EditMode;
    public GameObject EditeCursor;
    public List<EditPlayTile> EditPlayTiles;
    public int IndexChoseTile;
    public string SaveFileName;


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
                EditPanel.SetActive(true);
            }
            _cursor.transform.position = _PlayGridHolder.PlayGrid.GetWorldPositionCentreCell(mouseWorldPos);

            if (Input.GetButton("Fire1"))
            {
                EditPlayTile chosetile = EditPlayTiles[IndexChoseTile];

                switch (chosetile.Layer)
                {case 0 :
                        if (_PlayGridHolder.PlayGrid.GetPlayTile(mouseWorldPos).Tag1!=IndexChoseTile)
                        {
                            Vector2 GridPos = _PlayGridHolder.PlayGrid.GetXY(mouseWorldPos);
                            if (chosetile.UsingRuleTile)
                            {
                                _tilemap1.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.RuleTile);
                            }
                            else
                            {
                                _tilemap1.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.Tile);
                            }

                            _PlayGridHolder.PlayGrid.GetTile((int) GridPos.x, (int) GridPos.y).IsWalable1 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetTile((int) GridPos.x, (int) GridPos.y).Tag1 = IndexChoseTile;
                        }
                    break;
                    case 1 :
                        if (_PlayGridHolder.PlayGrid.GetPlayTile(mouseWorldPos).Tag2!=IndexChoseTile)
                        {
                            Vector2 GridPos = _PlayGridHolder.PlayGrid.GetXY(mouseWorldPos);
                            if (chosetile.UsingRuleTile)
                            {
                                _tilemap2.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.RuleTile);
                            }
                            else
                            {
                                _tilemap2.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.Tile);
                            }

                            _PlayGridHolder.PlayGrid.GetTile((int) GridPos.x, (int) GridPos.y).IsWalable2 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetTile((int) GridPos.x, (int) GridPos.y).Tag2 = IndexChoseTile;
                        }
                        break;
                    case 2 :if (_PlayGridHolder.PlayGrid.GetPlayTile(mouseWorldPos).Tag3!=IndexChoseTile)
                        {
                            Vector2 GridPos = _PlayGridHolder.PlayGrid.GetXY(mouseWorldPos);
                            if (chosetile.UsingRuleTile)
                            {
                                _tilemap3.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.RuleTile);
                            }
                            else
                            {
                                _tilemap3.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.Tile);
                            }

                            _PlayGridHolder.PlayGrid.GetTile((int) GridPos.x, (int) GridPos.y).IsWalable3 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetTile((int) GridPos.x, (int) GridPos.y).Tag2 = IndexChoseTile;
                        }
                        break;
                }
                
            }
        }
        else
        {
            if (_cursor != null)
            {
                Destroy(_cursor);
                EditPanel.SetActive(false);
            }
        }
    }

   // public void SaveMap()
    
      //  Save(UnityFolder.stremingAsset,_PlayGridHolder.PlayGrid,SaveFileName);
    
}
[Serializable]
public class EditPlayTile
{
    public int Layer;
    public bool IsWalkeble;
    public bool UsingRuleTile;
    public Tile Tile;
    public RuleTile RuleTile;
}
