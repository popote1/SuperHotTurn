using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EditorCam : MonoBehaviour
{
 public float ScreenBorderDetection =10;
 public float ScreenMoveSpeed = 0.1f;
 private Camera _cam;
 private int _hight;
 private int _width;
 private void Start()
 {
  
  _cam = GetComponent<Camera>();
  
 }

 private void Update()
 {
  _hight = _cam.pixelHeight;
  _width = _cam.pixelWidth;
  Vector3 moveVector= new Vector3();
  
  if (Input.mousePosition.x < _width / ScreenBorderDetection)
  {
   moveVector+= Vector3.left;
  }
  else if (Input.mousePosition.x > _width / ScreenBorderDetection * (ScreenBorderDetection - 1))
  {
   moveVector+= Vector3.right;
  }

  if (Input.mousePosition.y < _hight / ScreenBorderDetection)
  {
   moveVector+= Vector3.down;
  }
  else if (Input.mousePosition.y > _hight / ScreenBorderDetection * (ScreenBorderDetection - 1))
  {
   moveVector+=Vector3.up;
  }
  transform.Translate(moveVector*ScreenMoveSpeed);
 // Debug.Log(moveVector*ScreenMoveSpeed);
 }
}
