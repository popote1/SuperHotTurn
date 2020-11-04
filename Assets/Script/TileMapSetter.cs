using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class TileMapSetter : MonoBehaviour
{[Header("Configuration Ref")]
    public Tilemap _tilemap1;
    public Tilemap _tilemap2;
    public Tilemap _tilemap3;
    public GameObject EditPanel;
    public GameObject EditeCursor;
    [Header("Ui Elements")]
    public GameObject MainPannel;
    public TMP_Dropdown UIEditModeDropDown;
    public TMP_Text UIIndexChose;
    public TMP_Text UIDescriptionChose;
    public TMP_InputField UISaveInputField;
    public TMP_InputField UILoadInputField;
    public CanvasGroup MapSaveInfo;
    public CanvasGroup MapSaveErrorInfo;
    [FormerlySerializedAs("BackToMainMenuPannel")] public GameObject BackToMainMenuPanel;
    [Header("EditorChoises")]
    public editorActoie EditorActoie;
    public bool EditMode;
    public int IndexChoseTile;
    public int IndexChoseActor;
    public string SaveFileName;
    public string LoadFileName;
    [Header("EditorPresets")]
    public SOTempletActors TempletActors;
    public SOTempletBuilder TempletBuilder;
    [Header("MoreConfigurations")]
    public float SaveInfoFadeFactor;


    private PlayGridHolder _PlayGridHolder;
    private TilemapRenderer _tilemapRenderer;
    private GameObject _cursor;
    private bool _cursorOnPanel;
    private GameObject _player;
   
   public enum editorActoie
    {
        EditTiles,EditActor
    }

   private void Start()
   {
       UIPlusMoin(1);
       _PlayGridHolder = GetComponent<PlayGridHolder>();
       _PlayGridHolder.PlayGrid.TileTemple = TempletBuilder.name;
       UIChangeEditMode();

       for (int x = 0; x < _PlayGridHolder.PlayGrid._width; x++)
       {
           for (int y = 0; y <_PlayGridHolder.PlayGrid._hight; y++)
           { SetTile(TempletBuilder.EditPlayTiles[1],new Vector2Int(x,y),1);
           }
            
       }
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
                    SetTile(chosetile, _PlayGridHolder.PlayGrid.GetXY(mouseWorldPos),IndexChoseTile);
                } else if (EditorActoie == editorActoie.EditActor)
                {
                    EditGridActor choseActor = TempletActors.EditGridActors[IndexChoseActor];
                    SetActor(choseActor,_PlayGridHolder.PlayGrid.GetXY(mouseWorldPos),IndexChoseActor);
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

        if (MapSaveInfo.alpha != 0)
        {
            MapSaveInfo.alpha = Mathf.Clamp(MapSaveInfo.alpha - SaveInfoFadeFactor * Time.deltaTime, 0, 1);
        }
        if (MapSaveErrorInfo.alpha != 0)
        {
            MapSaveErrorInfo.alpha = Mathf.Clamp(MapSaveErrorInfo.alpha - (SaveInfoFadeFactor * Time.deltaTime), 0, 1);
        }
    }

    public void SaveMap()
    {
        if (_player != null)
        {
            SaveFileName = UISaveInputField.text;
            BinaryDataHandler.Save(BinaryDataHandler.UnityFolder.stremingAsset, _PlayGridHolder.PlayGrid, SaveFileName,
                BinaryDataHandler.DataFileExtention.map);
            MapSaveInfo.alpha = 1;
        }
        else
        {
            MapSaveErrorInfo.alpha = 0;
        }
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
                if (LoadedGrig.PlayTiles[x,y].Tag1!=0) SetTile(TempletBuilder.EditPlayTiles[LoadedGrig.PlayTiles[x,y].Tag1],new Vector2Int(x,y),LoadedGrig.PlayTiles[x,y].Tag1);
                if (LoadedGrig.PlayTiles[x,y].Tag2!=0) SetTile(TempletBuilder.EditPlayTiles[LoadedGrig.PlayTiles[x,y].Tag2],new Vector2Int(x,y),LoadedGrig.PlayTiles[x,y].Tag2);
                if (LoadedGrig.PlayTiles[x,y].Tag3!=0) SetTile(TempletBuilder.EditPlayTiles[LoadedGrig.PlayTiles[x,y].Tag3],new Vector2Int(x,y),LoadedGrig.PlayTiles[x,y].Tag3);
                if (LoadedGrig.PlayTiles[x,y].ActorIndex!=0)SetActor(TempletActors.EditGridActors[LoadedGrig.PlayTiles[x,y].ActorIndex],new Vector2Int(x,y),LoadedGrig.PlayTiles[x,y].ActorIndex);
            }
            
        }
    }
      //  Save(UnityFolder.stremingAsset,_PlayGridHolder.PlayGrid,SaveFileName);
      private void SetTile(EditPlayTile chosetile, Vector2Int gridPos, int index)
      {
          if (gridPos.x >= 0 && gridPos.x < _PlayGridHolder.Width && gridPos.y >= 0 &&
              gridPos.y < _PlayGridHolder.Hight)
          {
              if (IndexChoseTile != 0)
              {
                  switch (chosetile.Layer)
                  {
                      case 0:


                          if (chosetile.UsingRuleTile)
                          {
                              _tilemap1.SetTile(new Vector3Int(gridPos.x, gridPos.y, 0), chosetile.RuleTile);
                          }
                          else
                          {
                              _tilemap1.SetTile(new Vector3Int(gridPos.x, gridPos.y, 0), chosetile.Tile);
                          }

                          _PlayGridHolder.PlayGrid.GetPlayTile(gridPos.x, gridPos.y).IsWalable1 =
                              chosetile.IsWalkeble;
                          _PlayGridHolder.PlayGrid.GetPlayTile(gridPos.x, gridPos.y).Tag1 = index;

                          return;
                      case 1:


                          if (chosetile.UsingRuleTile)
                          {
                              _tilemap2.SetTile(new Vector3Int((int) gridPos.x, (int) gridPos.y, 0),
                                  chosetile.RuleTile);
                          }
                          else
                          {
                              _tilemap2.SetTile(new Vector3Int((int) gridPos.x, (int) gridPos.y, 0), chosetile.Tile);
                          }

                          _PlayGridHolder.PlayGrid.GetPlayTile((int) gridPos.x, (int) gridPos.y).IsWalable2 =
                              chosetile.IsWalkeble;
                          _PlayGridHolder.PlayGrid.GetPlayTile((int) gridPos.x, (int) gridPos.y).Tag2 = index;

                          return;
                      case 2:

                          if (chosetile.UsingRuleTile)
                          {
                              _tilemap3.SetTile(new Vector3Int((int) gridPos.x, (int) gridPos.y, 0),
                                  chosetile.RuleTile);
                          }
                          else
                          {
                              _tilemap3.SetTile(new Vector3Int((int) gridPos.x, (int) gridPos.y, 0), chosetile.Tile);
                          }

                          _PlayGridHolder.PlayGrid.GetPlayTile((int) gridPos.x, (int) gridPos.y).IsWalable3 =
                              chosetile.IsWalkeble;
                          _PlayGridHolder.PlayGrid.GetPlayTile((int) gridPos.x, (int) gridPos.y).Tag3 = index;

                          return;
                  }
              }
              else
              {
                  _tilemap1.SetTile(new Vector3Int(gridPos.x, gridPos.y, 0),null);
                  _tilemap2.SetTile(new Vector3Int(gridPos.x, gridPos.y, 0),null);
                  _tilemap3.SetTile(new Vector3Int(gridPos.x, gridPos.y, 0),null);
                  _PlayGridHolder.PlayGrid.GetPlayTile(gridPos.x, gridPos.y).IsWalable1 = false;
                  _PlayGridHolder.PlayGrid.GetPlayTile(gridPos.x, gridPos.y).Tag1 = 0;
                  _PlayGridHolder.PlayGrid.GetPlayTile(gridPos.x, gridPos.y).IsWalable2 = false;
                  _PlayGridHolder.PlayGrid.GetPlayTile(gridPos.x, gridPos.y).Tag2 = 0;
                  _PlayGridHolder.PlayGrid.GetPlayTile(gridPos.x, gridPos.y).IsWalable3 = false;
                  _PlayGridHolder.PlayGrid.GetPlayTile(gridPos.x, gridPos.y).Tag3 = 0;
              }
          }
      }

      private void SetActor(EditGridActor editGridActor, Vector2Int gridPos,int index)
      {
          if (_PlayGridHolder.PlayGrid.GetPlayTile(gridPos).ActorIndex != IndexChoseActor)
          {
              if (IndexChoseActor != 0)
              {
                  if (IndexChoseActor == 1 && _player != null) return;

                  GameObject newObject = Instantiate(editGridActor.GameObject,
                      _PlayGridHolder.PlayGrid.GetWorldPositionCentreCell(gridPos),
                      Quaternion.identity);
                  _PlayGridHolder.PlayGrid.GetPlayTile(gridPos).ActorIndex = index;
                  _PlayGridHolder.PlayGrid.GetPlayTile(gridPos).GridActor = newObject;
                  newObject.GetComponent<GridActor>().playGidHolder = gameObject;
                  newObject.GetComponent<GridActor>().PlayGrid = _PlayGridHolder.PlayGrid;
                  newObject.GetComponent<GridActor>().SetGridPos(gridPos);
                  if (IndexChoseActor == 1)
                  {
                      newObject.GetComponent<GridActorPlayer>().SetPause();
                      _player = newObject;
                  }
                 
              }
              else
              {
                  Destroy(_PlayGridHolder.PlayGrid.GetPlayTile(gridPos).GridActor);
                  _PlayGridHolder.PlayGrid.GetPlayTile(gridPos).ActorIndex = index;
                  _PlayGridHolder.PlayGrid.GetPlayTile(gridPos).GridActor =null;
              }
          }
      }

      public void UIChangeEditMode()
      {
          switch (UIEditModeDropDown.value)
          {
              case 0: EditorActoie = editorActoie.EditTiles;
                  UIIndexChose.text = ""+IndexChoseTile;
                  UIDescriptionChose.text = TempletBuilder.EditPlayTiles[IndexChoseTile].Name;
                  break;
              case 1: EditorActoie = editorActoie.EditActor;
                  UIIndexChose.text = "" + IndexChoseActor;
                  UIDescriptionChose.text = TempletActors.EditGridActors[IndexChoseActor].Name;
                  break;
          }
      }

      public void UIPlusMoin(int value)
      {
          if (EditorActoie == editorActoie.EditTiles)
          {
              IndexChoseTile += value;
              if (IndexChoseTile < 0) IndexChoseTile = 0;
              if (IndexChoseTile >= TempletBuilder.EditPlayTiles.Count)
              {
                 
                  IndexChoseTile = TempletActors.EditGridActors.Count;
              }

              UIIndexChose.text = ""+IndexChoseTile;
             UIDescriptionChose.text = TempletBuilder.EditPlayTiles[IndexChoseTile].Name;
          }else if (EditorActoie == editorActoie.EditActor)
          {
              IndexChoseActor += value;
              if (IndexChoseActor < 0) IndexChoseActor = 0;
              if (IndexChoseActor >= TempletActors.EditGridActors.Count)
                 IndexChoseActor = TempletActors.EditGridActors.Count;
              UIIndexChose.text = "" + IndexChoseActor;
             UIDescriptionChose.text = TempletActors.EditGridActors[IndexChoseActor].Name;
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
      

      public void UIOuiBackToMainMenu()
      {
          SceneManager.LoadScene(0);
      }

      public void UIOnOffBackToMainMenu()
      {
          BackToMainMenuPanel.SetActive(!BackToMainMenuPanel.activeSelf);
          MainPannel.SetActive(!MainPannel.activeSelf);
          EditMode = !EditMode;
      }

      public void UITestfade()
      {
          MapSaveInfo.alpha = 1;
      }

      public void ChangeNameSave(string name)
      {
          UISaveInputField.text = name;
      }

     
}
[Serializable]
public class EditPlayTile
{
    public string Name;
    public int Layer;
    public bool IsWalkeble;
    public bool UsingRuleTile;
    public Tile Tile;
    public RuleTile RuleTile;
}
[Serializable]
public class EditGridActor
{
    public string Name;
    public GameObject GameObject;
}
