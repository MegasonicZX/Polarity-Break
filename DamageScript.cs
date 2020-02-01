using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    // Start is called before the first frame update
    public PolarityShield pol;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D (Collision2D col)
    {
       if (col.gameObject.CompareTag ("EnemyFire") && pol.shieldSwitch == false)
        {
            Debug.Log("Absorption");
        } else if (col.gameObject.CompareTag ("EnemyFire") && pol.shieldSwitch == true)
        {
            Debug.Log("Got Hit");
        }
    }
}
