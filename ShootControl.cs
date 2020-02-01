using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    GameObject temp;
    public Transform firePoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.V))
        {
            temp = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Destroy(temp, 1f);
        }
   
    }
}
