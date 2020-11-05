using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGridHolder : MonoBehaviour
{
    public bool InEditMode;
    public int Hight;
    public int Width;
    public float CellSize;
    public Vector3 Origin;
    public PlayGrid PlayGrid;
    public TileMapSetter TileMapSetter;
    [SerializeField]public EditorCam EditorCam;

    [Header("Debug Options")] public bool ShowDebudLines = true;

    void Awake()
    {
        if (TileMapSetter != null)
        {
            
                MainMenuHendler info = GameObject.Find("MainMenu").GetComponent<MainMenuHendler>();
                Hight = info.NewHauteur;
                Width = info.NewLargeur;
                PlayGrid = new PlayGrid(info.NewHauteur, info.NewLargeur, CellSize, Origin);
                TileMapSetter.ChangeNameSave(info.NewName);
                Destroy(info.gameObject);
                Debug.Log("hauter"+ info.NewHauteur +"   Largeur "+info.NewLargeur);
                EditorCam.Xmax = PlayGrid._width * PlayGrid._cellsize;
                EditorCam.Ymax = PlayGrid._hight * PlayGrid._cellsize;
                
        }
        else{
            PlayGrid = new PlayGrid(Hight, Width, CellSize, Origin);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayGrid != null)
        {
            if (ShowDebudLines)
            {
                for (int x = 0; x < PlayGrid.Width; x++)
                {
                    for (int z = 0; z < PlayGrid.Hight; z++)
                    {
                        Debug.DrawLine(PlayGrid.GetWorldPosition(x, z), PlayGrid.GetWorldPosition(x, z + 1), Color.red);
                        Debug.DrawLine(PlayGrid.GetWorldPosition(x, z), PlayGrid.GetWorldPosition(x + 1, z), Color.red);
                    }

                }
            }
        }
    }

    public PlayGrid CreatNewPlaygrid(int hight , int width , float cellSize , Vector3 origine)
    {
        PlayGrid = new PlayGrid(hight,width,cellSize,origine);
        return PlayGrid;
    }

   
}
