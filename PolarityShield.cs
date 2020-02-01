using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolarityShield : MonoBehaviour
{
   //Shield is 4x4 in' in size.
    [Header("Shield Switch")]
    public bool shieldSwitch;
    SpriteRenderer spr;

    [Header("Power Level")]
    public int powerGuage;

    private Image barImage;
    
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        barImage = transform.Find("Power").GetComponent<Image>();
        barImage.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ShieldSwitcher();
        
    }

    void ShieldSwitcher ()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            shieldSwitch = !shieldSwitch;
        }

        if (shieldSwitch == true)
        {
            spr.color = Color.red;
        }

        if (shieldSwitch == false)
        {
            spr.color = Color.blue;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //If hit while having the right shield up.
        if (col.gameObject.tag == "Blue Polarity" && shieldSwitch == false)
        {
            Destroy(col.gameObject);
            powerGuage += 1;
            barImage.fillAmount += 0.5f;
        }

        if (col.gameObject.tag == "Red Polarity" && shieldSwitch == true)
        {
            Destroy(col.gameObject);
            powerGuage += 1;
            barImage.fillAmount += 0.5f;
        }

        //If hit when having the wrong shield up.
        if (col.gameObject.tag == "Blue Polarity" && shieldSwitch == true)
        {
            Destroy(col.gameObject);
            powerGuage -= powerGuage;
            barImage.fillAmount -= barImage.fillAmount;
        }

        if (col.gameObject.tag == "Red Polarity" && shieldSwitch == false)
        {
            Destroy(col.gameObject);
            powerGuage -= powerGuage;
            barImage.fillAmount -= barImage.fillAmount;
        }
        Debug.Log(powerGuage);
    }
}
