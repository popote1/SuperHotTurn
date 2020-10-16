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

    [Header("Debug Options")] public bool ShowDebudLines = true;

    void Awake()
    {
        if (InEditMode)
        {
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

   
}
