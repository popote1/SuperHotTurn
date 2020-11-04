
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 TTMove;
    public Vector3 NexPos;

    public int _lifeTime = 5;

    private void Start()
    {
        TimeManager.TTStart.AddListener(TTmove);
    }
    
    public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, NexPos, 0.1f);
    }

    public void TTmove()
    {
        Debug.Log("la bullet bouge");
        NexPos +=TTMove;
        _lifeTime--;
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
