using System;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    public Cube Cube;
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Cube.Position = transform.position;
            DataHandler.Save(UnityFolder.stremingAsset,Cube,"cube");
        }

        else if (Input.GetButton("Fire2"))
        {
            Cube loadCube =DataHandler.Load<Cube>(UnityFolder.stremingAsset, "cube");
            Cube.Name = loadCube.Name;
            gameObject.transform.position = loadCube.Position;
        }
    }
}
[Serializable]
public class Cube
{
    public String Name;
    public Vector3 Position;
    
}