using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridActorEnemieVertica : GridActorEnnemi
{
   public direction Direction = direction.haut;
   public enum direction
   {
      haut,bas
   }

   private bool _willMove;
   private Vector2Int _nextPos;
   private void Start()
   {
      TimeManager.TTStart.AddListener(TTStart);
      TimeManager.TTUpdate.AddListener(TTUpDate);
      TimeManager.TTLateUpdate.AddListener(TTLastUpDate);
      PlayGrid = playGidHolder.GetComponent<PlayGridHolder>().PlayGrid;
      _newPos = PlayGrid.GetWorldPositionCentreCell(_currentPos);

   }

   protected void TTStart()
   { // verifie les places disponibles
      if (Direction == direction.haut)
      {
         _nextPos = _currentPos+new Vector2Int(0,1);
      }
      else
      {
         _nextPos = _currentPos + new Vector2Int(0, -1);
      }

      if(PlayGrid.CheckIfWalkeble(_nextPos.x,_nextPos.y))
      {
         _willMove = true;
      }
      else
      {
         if (Direction == direction.haut)
         {
            Direction = direction.bas;
            _nextPos = _currentPos+new Vector2Int(0,-1);
         }
         else
         {
            Direction = direction.haut;
            _nextPos = _currentPos + new Vector2Int(0, 1);
         }

         if (PlayGrid.CheckIfWalkeble(-_nextPos.x, _nextPos.y))
         {
            _willMove = true;
         }
      }
      
      // effectue l'action de bouger
      if (_willMove)
      {
         PlayGrid.GetPlayTile(_currentPos).GridActor=null;
         PlayGrid.GetPlayTile(_nextPos).GridActor=null;
         _currentPos = _nextPos;
         _newPos = PlayGrid.GetWorldPositionCentreCell(_currentPos);

      }
   }

   protected void TTUpDate()
   {
      
   }

   protected void TTLastUpDate()
   {
      
      
   }

   private void Update()
   {
      transform.position = Vector3.Lerp(transform.position, _newPos,0.15f);
   }
}
