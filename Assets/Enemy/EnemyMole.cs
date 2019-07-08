using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMole : MonoBehaviour
{
    [SerializeField]
    private float speed=0.5f;
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float shotIntervalSec = 1;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Shot");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        var relativePos = target.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePos);
        transform.rotation =
          Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);

    }

    IEnumerator Shot()
    {
        while(true)
        {
            if (target == null)
                yield return null;

            GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Init(transform.forward);

            yield return new WaitForSeconds(shotIntervalSec);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            target = other.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            target = null;
    }
}
