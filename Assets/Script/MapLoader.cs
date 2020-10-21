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

    private void Awake()
    {
        GameObject LoadData =GameObject.Find("MainMenu");
        _PlayGridHolder = GetComponent<PlayGridHolder>(); 
        savePlayGrid = LoadData.GetComponent<MainMenuHendler>().PlayGrid;
        newPlayGrid=_PlayGridHolder.CreatNewPlaygrid(savePlayGrid._hight, savePlayGrid._width, savePlayGrid._cellsize, savePlayGrid.GetGridOrinie());
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
                if (savePlayGrid.PlayTiles[x,y].Tag1!=0) SetTile(TempletBuilder.EditPlayTiles[savePlayGrid.PlayTiles[x,y].Tag1],new Vector2Int(x,y),newPlayGrid.PlayTiles[x,y].Tag1);
                if (savePlayGrid.PlayTiles[x,y].Tag2!=0) SetTile(TempletBuilder.EditPlayTiles[savePlayGrid.PlayTiles[x,y].Tag2],new Vector2Int(x,y),newPlayGrid.PlayTiles[x,y].Tag2);
                if (savePlayGrid.PlayTiles[x,y].Tag3!=0) SetTile(TempletBuilder.EditPlayTiles[savePlayGrid.PlayTiles[x,y].Tag3],new Vector2Int(x,y),newPlayGrid.PlayTiles[x,y].Tag3);
                if (savePlayGrid.PlayTiles[x,y].ActorIndex!=0)SetActor(TempletActors.EditGridActors[savePlayGrid.PlayTiles[x,y].ActorIndex],new Vector2Int(x,y),newPlayGrid.PlayTiles[x,y].ActorIndex);
            }
            
        }
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
                        
                    break;
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
                        
                        break;
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
                        
                        break;
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
          if (index ==0){ newObject.GetComponentInChildren<Camera>().enabled = true;Debug.Log("Activation de la camera , l'index est de"+index);}

      }
}

