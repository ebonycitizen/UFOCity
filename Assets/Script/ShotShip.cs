﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HI5;

public class ShotShip : MonoBehaviour
{
    [SerializeField]
    private Grab grab;
    [SerializeField]
    private Transform forward;

    [SerializeField]
    private float lineLength = 5f;
    [SerializeField]
    private float shotIntervalSec = 0.12f;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletPos;

    private LineRenderer lineRenderer;
    private RaycastHit hit;
    private Vector3 direction;

    private int layerMask;//for enemy

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        layerMask = 1 << 11;
        
    }

    void OnEnable()
    {
        StartCoroutine("StartShot");
    }

    // Update is called once per frame
    void Update()
    {
        direction = (forward.position - transform.position).normalized;
        UpdateLine();
    }

    private void UpdateLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + direction * lineLength);
    }

    private void Shot(GameObject target)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(target.transform);
    }

    private GameObject AimTarget()
    {
        bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask);

        if (isHit)
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private IEnumerator StartShot()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            GameObject target = AimTarget();
            if (target != null)
            {
                Shot(target);
                Vibrate();
                yield return new WaitForSeconds(shotIntervalSec);
            }
            yield return null;
        }
    }

    private void Vibrate()
    {
        if (grab == null)
            return;

        int time = 50;
        if (grab.gameObject.layer == 9)
            HI5_Manager.EnableRightVibration(time);
        if (grab.gameObject.layer == 10)
            HI5_Manager.EnableLeftVibration(time);
    }
}
