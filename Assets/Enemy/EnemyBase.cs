using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private int maxHP;

    private int hp;

    protected virtual void Start()
    {
         hp = maxHP;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            hp--;
            if (hp <= 0)
                Destroy(gameObject);
        }
    }
}
