using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridActorTester : MonoBehaviour
{
    public GameObject playGidHolder;
    private PlayGrid _playGrid;
    private Vector2Int _currentPos=new Vector2Int(0,0);
    private Vector3 _newPos;
   /*void Start()
    {
        _playGrid = playGidHolder.GetComponent<PlayGridHolder>().PlayGrid;
        _newPos = _playGrid.GetWorldPositionCentreCell(_currentPos.x, _currentPos.y);
    }*/
   
    void Update()
    {
        //initie ga grid associer
        if (_playGrid == null && playGidHolder != null)
        {
            _playGrid = playGidHolder.GetComponent<PlayGridHolder>().PlayGrid;
            _newPos = _playGrid.GetWorldPositionCentreCell(_currentPos.x, _currentPos.y);
        }
        
        
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (_playGrid.CheckIfWalkeble((int) _playGrid.GetXY(transform.position).x,
                    (int) _playGrid.GetXY(transform.position).y + 1))
                {
                    _newPos = _playGrid.GetWorldPositionCentreCell(_currentPos.x, _currentPos.y + 1);
                    _currentPos = _currentPos + new Vector2Int(0, 1);
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                if (_playGrid.CheckIfWalkeble((int) _playGrid.GetXY(transform.position).x,
                    (int) _playGrid.GetXY(transform.position).y - 1))
                {
                    _newPos = _playGrid.GetWorldPositionCentreCell(_currentPos.x, _currentPos.y - 1);
                    _currentPos = _currentPos + new Vector2Int(0, -1);
                }
            }
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                if (_playGrid.CheckIfWalkeble((int) _playGrid.GetXY(transform.position).x + 1,
                    (int) _playGrid.GetXY(transform.position).y))
                {
                    _newPos = _playGrid.GetWorldPositionCentreCell(_currentPos.x + 1, _currentPos.y);
                    _currentPos = _currentPos + new Vector2Int(1, 0);
                }
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                if (_playGrid.CheckIfWalkeble((int) _playGrid.GetXY(transform.position).x - 1,
                    (int) _playGrid.GetXY(transform.position).y))
                {
                    _newPos = _playGrid.GetWorldPositionCentreCell(_currentPos.x - 1, _currentPos.y);
                    _currentPos = _currentPos + new Vector2Int(-1, 0);
                }
            }
            
        }
       
        transform.position = Vector3.Lerp(transform.position , _newPos, 0.5f);
    }

    
}


