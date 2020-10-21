using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class TileMapSetter : MonoBehaviour
{[Header("Configuration Ref")]
    public Tilemap _tilemap1;
    public Tilemap _tilemap2;
    public Tilemap _tilemap3;
    public GameObject EditPanel;
    public GameObject EditeCursor;
    [Header("Ui Elements")]
    public TMP_Dropdown UIEditModeDropDown;
    public TMP_Text UIIndexChose;
    public TMP_Text UIDescriptionChose;
    public TMP_InputField UISaveInputField;
    public TMP_InputField UILoadInputField;
    [Header("EditorChoises")]
    public editorActoie EditorActoie;
    public bool EditMode;
    public int IndexChoseTile;
    public int IndexChoseActor;
    public string SaveFileName;
    public string LoadFileName;
    [Header("EditorPresets")]
    //public List<EditPlayTile> EditPlayTiles;
    //public List<EditGridActor> EditGridActors;

    public SOTempletActors TempletActors;
    public SOTempletBuilder TempletBuilder;


    private PlayGridHolder _PlayGridHolder;
    private TilemapRenderer _tilemapRenderer;
    private GameObject _cursor;
    private bool _cursorOnPanel;
   public enum editorActoie
    {
        EditTiles,EditActor
    }

   private void Start()
   {
       _PlayGridHolder = GetComponent<PlayGridHolder>();
       _PlayGridHolder.PlayGrid.TileTemple = TempletBuilder.name;
       UIChangeEditMode();
       
      
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

            if (Input.GetButton("Fire1")&&!_cursorOnPanel)
            {
                
                 if (EditorActoie == editorActoie.EditTiles)
                {
                    EditPlayTile chosetile = TempletBuilder.EditPlayTiles[IndexChoseTile];
                    SetTile(chosetile, _PlayGridHolder.PlayGrid.GetXY(mouseWorldPos));
                } else if (EditorActoie == editorActoie.EditActor)
                {
                    EditGridActor choseActor = TempletActors.EditGridActors[IndexChoseActor];
                    SetActor(choseActor,_PlayGridHolder.PlayGrid.GetXY(mouseWorldPos));
                }

                /*switch (chosetile.Layer)
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

                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).IsWalable1 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).Tag1 = IndexChoseTile;
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

                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).IsWalable2 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).Tag2 = IndexChoseTile;
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

                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).IsWalable3 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).Tag2 = IndexChoseTile;
                        }
                        break;
                }*/
                
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

    public void SaveMap()
    {
        SaveFileName = UISaveInputField.text;
        BinaryDataHandler.Save(BinaryDataHandler.UnityFolder.stremingAsset,_PlayGridHolder.PlayGrid,SaveFileName,BinaryDataHandler.DataFileExtention.map);
    }

    public void Load()
    {
        LoadFileName = UILoadInputField.text;
        PlayGrid LoadedGrig = BinaryDataHandler.Load<PlayGrid>(BinaryDataHandler.UnityFolder.stremingAsset,
            LoadFileName, BinaryDataHandler.DataFileExtention.map);
        Debug.Log(LoadedGrig._hight);
       _PlayGridHolder.PlayGrid = LoadedGrig;
        for (int x = 0; x < LoadedGrig._hight; x++)
        {
            for (int y = 0; y <LoadedGrig._width; y++)
            {
                if (LoadedGrig.PlayTiles[x,y].Tag1!=0) SetTile(TempletBuilder.EditPlayTiles[LoadedGrig.PlayTiles[x,y].Tag1],new Vector2Int(x,y));
                if (LoadedGrig.PlayTiles[x,y].Tag2!=0) SetTile(TempletBuilder.EditPlayTiles[LoadedGrig.PlayTiles[x,y].Tag2],new Vector2Int(x,y));
                if (LoadedGrig.PlayTiles[x,y].Tag3!=0) SetTile(TempletBuilder.EditPlayTiles[LoadedGrig.PlayTiles[x,y].Tag3],new Vector2Int(x,y));
                if (LoadedGrig.PlayTiles[x,y].ActorIndex!=0)SetActor(TempletActors.EditGridActors[LoadedGrig.PlayTiles[x,y].ActorIndex],new Vector2Int(x,y));
            }
            
        }
    }
      //  Save(UnityFolder.stremingAsset,_PlayGridHolder.PlayGrid,SaveFileName);
      private void SetTile(EditPlayTile chosetile,Vector2Int gridPos)
      {
          switch (chosetile.Layer)
                {case 0 :
                        if (_PlayGridHolder.PlayGrid.GetPlayTile(gridPos).Tag1!=IndexChoseTile)
                        {
                            Vector2 GridPos = gridPos;
                            if (chosetile.UsingRuleTile)
                            {
                                _tilemap1.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.RuleTile);
                            }
                            else
                            {
                                _tilemap1.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.Tile);
                            }

                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).IsWalable1 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).Tag1 = IndexChoseTile;
                        }
                    break;
                    case 1 :
                        if (_PlayGridHolder.PlayGrid.GetPlayTile(gridPos).Tag2!=IndexChoseTile)
                        {
                            Vector2 GridPos = gridPos;
                            if (chosetile.UsingRuleTile)
                            {
                                _tilemap2.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.RuleTile);
                            }
                            else
                            {
                                _tilemap2.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.Tile);
                            }

                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).IsWalable2 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).Tag2 = IndexChoseTile;
                        }
                        break;
                    case 2 :if (_PlayGridHolder.PlayGrid.GetPlayTile(gridPos).Tag3!=IndexChoseTile)
                        {
                            Vector2 GridPos = gridPos;
                            if (chosetile.UsingRuleTile)
                            {
                                _tilemap3.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.RuleTile);
                            }
                            else
                            {
                                _tilemap3.SetTile(new Vector3Int((int) GridPos.x, (int) GridPos.y, 0), chosetile.Tile);
                            }

                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).IsWalable3 =
                                chosetile.IsWalkeble;
                            _PlayGridHolder.PlayGrid.GetPlayTile((int) GridPos.x, (int) GridPos.y).Tag2 = IndexChoseTile;
                        }
                        break;
                }
      }

      private void SetActor(EditGridActor editGridActor, Vector2Int gridPos)
      {
          if (_PlayGridHolder.PlayGrid.GetPlayTile(gridPos).ActorIndex != IndexChoseActor)
          {
              GameObject newObject= Instantiate(editGridActor.GameObject, _PlayGridHolder.PlayGrid.GetWorldPositionCentreCell(gridPos),
                  Quaternion.identity);
              _PlayGridHolder.PlayGrid.GetPlayTile(gridPos).ActorIndex = IndexChoseActor;
              _PlayGridHolder.PlayGrid.GetPlayTile(gridPos).GridActor=newObject; 
              newObject.GetComponent<GridActor>().playGidHolder =gameObject;
              newObject.GetComponent<GridActor>().PlayGrid = _PlayGridHolder.PlayGrid;
              newObject.GetComponent<GridActor>().SetGridPos(gridPos);
          }
      }

      public void UIChangeEditMode()
      {
          switch (UIEditModeDropDown.value)
          {
              case 0: EditorActoie = editorActoie.EditTiles;
                  UIIndexChose.text = ""+IndexChoseTile;
                  break;
              case 1: EditorActoie = editorActoie.EditActor;
                  UIIndexChose.text = "" + IndexChoseActor;
                  break;
          }
      }

      public void UIPlusMoin(int value)
      {
          if (EditorActoie == editorActoie.EditTiles)
          {
              IndexChoseTile += value;
              if (IndexChoseTile < 0) IndexChoseTile = 0;
              UIIndexChose.text = ""+IndexChoseTile;
          }else if (EditorActoie == editorActoie.EditActor)
          {
              IndexChoseActor += value;
              if (IndexChoseActor < 0) IndexChoseActor = 0;
              UIIndexChose.text = "" + IndexChoseActor;
          }
      }

      public void UICursorExitPanel()
      {
          _cursorOnPanel = false;
          Debug.Log("Sort Du Panel");
      }

      public void UICursorEntrePanel()
      {
          _cursorOnPanel = true;
          Debug.Log("Entre Dans le pannel");
      }

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
[Serializable]
public class EditGridActor
{
    public GameObject GameObject;
}
