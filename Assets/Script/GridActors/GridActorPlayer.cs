using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridActorPlayer : GridActor
{
    public UnityEvent DirectorMove;

    public GameObject Canon;
    public GameObject FirePoint;
    public GameObject Munition;
    public float Munitionspeed;
    private bool _hasFire;
    // Start is called before the first frame update
    void Start()
    {
        DirectorMove.AddListener(TimeManager.DirectorMove);
        Debug.Log(PlayGrid._hight);
        PlayGrid = playGidHolder.GetComponent<PlayGridHolder>().PlayGrid;
        _newPos = PlayGrid.GetWorldPositionCentreCell(_currentPos.x, _currentPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (PlayGrid == null && playGidHolder != null)
        {
            Debug.Log("Set de la playGrid");
            
        }*/
           if (Input.GetButtonDown("Horizontal"))
        {
            Debug.Log("BP horizontal");
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (PlayGrid.CheckIfWalkeble((int) PlayGrid.GetXY(transform.position).x,
                    (int) PlayGrid.GetXY(transform.position).y + 1))
                {
                    _newPos = PlayGrid.GetWorldPositionCentreCell(_currentPos.x, _currentPos.y + 1);
                    PlayGrid.GetPlayTile(_currentPos).GridActor = null;
                    _currentPos = _currentPos + new Vector2Int(0, 1);
                    PlayGrid.GetPlayTile(_currentPos).GridActor = gameObject;
                    DirectorMove.Invoke();
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                if (PlayGrid.CheckIfWalkeble((int) PlayGrid.GetXY(transform.position).x,
                    (int) PlayGrid.GetXY(transform.position).y - 1))
                {
                    _newPos = PlayGrid.GetWorldPositionCentreCell(_currentPos.x, _currentPos.y - 1);
                    PlayGrid.GetPlayTile(_currentPos).GridActor = null;
                    _currentPos = _currentPos + new Vector2Int(0, -1);
                    PlayGrid.GetPlayTile(_currentPos).GridActor = gameObject;
                    DirectorMove.Invoke();
                }
            }
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                Debug.Log("BP vertical");
                if (PlayGrid.CheckIfWalkeble((int) PlayGrid.GetXY(transform.position).x + 1,
                    (int) PlayGrid.GetXY(transform.position).y))
                {
                    _newPos = PlayGrid.GetWorldPositionCentreCell(_currentPos.x + 1, _currentPos.y);
                    PlayGrid.GetPlayTile(_currentPos).GridActor = null;
                    _currentPos = _currentPos + new Vector2Int(1, 0);
                    PlayGrid.GetPlayTile(_currentPos).GridActor = gameObject;
                    DirectorMove.Invoke();
                }
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                if (PlayGrid.CheckIfWalkeble((int) PlayGrid.GetXY(transform.position).x - 1,
                    (int) PlayGrid.GetXY(transform.position).y))
                {
                    _newPos = PlayGrid.GetWorldPositionCentreCell(_currentPos.x - 1, _currentPos.y);
                    PlayGrid.GetPlayTile(_currentPos).GridActor = null;
                    _currentPos = _currentPos + new Vector2Int(-1, 0);
                    PlayGrid.GetPlayTile(_currentPos).GridActor = gameObject;
                    DirectorMove.Invoke();
                }
            }
            
        }
        transform.position = Vector3.Lerp(transform.position , _newPos, 0.2f);
        Vector3 mouseWorldPos= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Canon.transform.forward = (mouseWorldPos-new Vector3(Canon.transform.position.x,0,Canon.transform.position.z));

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(Munition, transform.position, Canon.transform.rotation);
            bullet.GetComponent<BulletScript>().NexPos = transform.position;
            bullet.GetComponent<BulletScript>().TTMove = Canon.transform.forward * Munitionspeed;
            bullet.GetComponent<BulletScript>().TTmove();
        }
    }
    
}
