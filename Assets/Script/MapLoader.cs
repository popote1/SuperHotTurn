using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapLoader : MonoBehaviour
{
    public PlayGrid newPlayGrid;
    public Tilemap _tilemap1;
    public Tilemap _tilemap2;
    public Tilemap _tilemap3;
    [Header("EditorPresets")]
    public List<EditPlayTile> EditPlayTiles;
    //public List<EditGridActor> EditGridActors;
    public SOTempletBuilder TempletBuilder;
    public SOTempletActors TempletActors;

    private PlayGridHolder _PlayGridHolder;
    private PlayGrid savePlayGrid;
    private GameManager _gameManager;

    private GameObject _player;
    private List<GameObject> _ennemis=new List<GameObject>();

    private void Awake()
    {
        GameObject LoadData =GameObject.Find("MainMenu");
        _PlayGridHolder = GetComponent<PlayGridHolder>(); 
        savePlayGrid = LoadData.GetComponent<MainMenuHendler>().PlayGrid;
        newPlayGrid=_PlayGridHolder.CreatNewPlaygrid(savePlayGrid._hight, savePlayGrid._width, savePlayGrid._cellsize, savePlayGrid.GetGridOrinie());
        _gameManager = GetComponent<GameManager>();
        Load();
        Destroy(LoadData);
    }

    public void Load()
    {
        
        
        Debug.Log(newPlayGrid._hight);
      // _PlayGridHolder.PlayGrid = newPlayGrid;
        for (int x = 0; x < savePlayGrid._hight; x++)
        {
            for (int y = 0; y <savePlayGrid._width; y++)
            {
                if (savePlayGrid.PlayTiles[x,y].Tag1!=0) SetTile(TempletBuilder.EditPlayTiles[savePlayGrid.PlayTiles[x,y].Tag1],new Vector2Int(x,y),savePlayGrid.PlayTiles[x,y].Tag1);
                if (savePlayGrid.PlayTiles[x,y].Tag2!=0) SetTile(TempletBuilder.EditPlayTiles[savePlayGrid.PlayTiles[x,y].Tag2],new Vector2Int(x,y),savePlayGrid.PlayTiles[x,y].Tag2);
                if (savePlayGrid.PlayTiles[x,y].Tag3!=0) SetTile(TempletBuilder.EditPlayTiles[savePlayGrid.PlayTiles[x,y].Tag3],new Vector2Int(x,y),savePlayGrid.PlayTiles[x,y].Tag3);
                if (savePlayGrid.PlayTiles[x,y].ActorIndex!=0)SetActor(TempletActors.EditGridActors[savePlayGrid.PlayTiles[x,y].ActorIndex],new Vector2Int(x,y),savePlayGrid.PlayTiles[x,y].ActorIndex);
            }
            
        }
        _gameManager.SetUpGame(_ennemis , _player);
    }
      //  Save(UnityFolder.stremingAsset,_PlayGridHolder.PlayGrid,SaveFileName);
      private void SetTile(EditPlayTile chosetile,Vector2Int gridPos, int index)
      {
          switch (chosetile.Layer)
                {case 0 :
                       
                            
                            if (chosetile.UsingRuleTile)
                            {
                                _tilemap1.SetTile(new Vector3Int( gridPos.x,  gridPos.y, 0), chosetile.RuleTile);
                            }
                            else
                            {
                                _tilemap1.SetTile(new Vector3Int( gridPos.x,  gridPos.y, 0), chosetile.Tile);
                            }

                            _PlayGridHolder.PlayGrid.GetPlayTile( gridPos.x, gridPos.y).IsWalable1 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetPlayTile(gridPos.x,  gridPos.y).Tag1 =index;
                        
                    return;
                    case 1 :
                       
                           
                            if (chosetile.UsingRuleTile)
                            {
                                _tilemap2.SetTile(new Vector3Int((int) gridPos.x, (int) gridPos.y, 0), chosetile.RuleTile);
                            }
                            else
                            {
                                _tilemap2.SetTile(new Vector3Int((int) gridPos.x, (int) gridPos.y, 0), chosetile.Tile);
                            }

                            _PlayGridHolder.PlayGrid.GetPlayTile((int) gridPos.x, (int) gridPos.y).IsWalable2 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetPlayTile((int) gridPos.x, (int) gridPos.y).Tag2 = index;
                        
                        return;
                    case 2 :
                            
                            if (chosetile.UsingRuleTile)
                            {
                                _tilemap3.SetTile(new Vector3Int((int) gridPos.x, (int) gridPos.y, 0), chosetile.RuleTile);
                            }
                            else
                            {
                                _tilemap3.SetTile(new Vector3Int((int) gridPos.x, (int) gridPos.y, 0), chosetile.Tile);
                            }

                            _PlayGridHolder.PlayGrid.GetPlayTile((int) gridPos.x, (int) gridPos.y).IsWalable3 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetPlayTile((int) gridPos.x, (int) gridPos.y).Tag2 = index;
                        
                        return;
                }
      }

      private void SetActor(EditGridActor editGridActor, Vector2Int gridPos, int index)
      {
          GameObject newObject= Instantiate(editGridActor.GameObject, _PlayGridHolder.PlayGrid.GetWorldPositionCentreCell(gridPos), 
              Quaternion.identity);
          _PlayGridHolder.PlayGrid.GetPlayTile(gridPos).ActorIndex = index;
          _PlayGridHolder.PlayGrid.GetPlayTile(gridPos).GridActor=newObject; 
          newObject.GetComponent<GridActor>().playGidHolder =gameObject;
          newObject.GetComponent<GridActor>().PlayGrid = newPlayGrid;
          newObject.GetComponent<GridActor>().SetGridPos(gridPos);
          Debug.Log("L'index est de"+index);
          if (index ==1)
          {
              newObject.transform.GetChild(0).gameObject.SetActive(true);
              _player = newObject;
             }

          if (index == 2)
          {
              _ennemis.Add(newObject);
          }

      }
}

