using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotShip : MonoBehaviour
{
    [SerializeField]
    private Grab grab;
    [SerializeField]
    private Transform forward;

    [SerializeField]
    private float lineLength = 5f;
    [SerializeField]
    private GameObject bulletPrefab;

    private LineRenderer lineRenderer;
    private RaycastHit hit;

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
        UpdateLine();
    }

    private void UpdateLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + forward.position.normalized * lineLength);
    }

    private void Shot()
    {
        Instantiate(bulletPrefab, transform.position + forward.position, Quaternion.identity);
    }

    private GameObject AimTarget()
    {
        bool isHit = Physics.Raycast(transform.position, forward.position.normalized, out hit, Mathf.Infinity, layerMask);

        if (isHit)
            return hit.collider.gameObject;

        return null;
    }

    private IEnumerator StartShot()
    {
        while(true)
        {
            GameObject target = AimTarget();
            if (target != null)
            {
                Shot();
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
