using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridActorEnnemi : GridActor
{
    public int HP;
    public GameObject Particule1;
    public GameObject Particule2;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EnnemiKill(gameObject);
            Destroy(Instantiate(Particule1, transform.position, Quaternion.identity), 2);
            Destroy(Instantiate(Particule2, transform.position, Quaternion.identity), 2);
            Destroy(gameObject);
        }
    }
}
