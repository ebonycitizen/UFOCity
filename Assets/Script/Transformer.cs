using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    [SerializeField]
    private GameObject shotShipRight;
    [SerializeField]
    private GameObject shotShipLeft;
    [SerializeField]
    private GameObject alienShip;

    // Start is called before the first frame update
    void Start()
    {
        alienShip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableShotShip()
    {
        shotShipRight.SetActive(true);
        shotShipLeft.SetActive(true);
        alienShip.SetActive(false);
    }

    private void EnableAlienShip()
    {
        shotShipRight.SetActive(false);
        shotShipLeft.SetActive(false);
        alienShip.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Transformer")
            EnableAlienShip();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Transformer")
            EnableShotShip();
    }
}
