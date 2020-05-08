using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleControl : MonoBehaviour
{
	public Text countText;
    public Text strikeText;

    private static int count;
    private float center;
    private bool check;
    private Rigidbody rg;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        strikeText.text = "";
        check = false;
        SetText();
        rg = GetComponent<Rigidbody>();
        center = rg.position.y + rg.centerOfMass.y;
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(rg.position.y + rg.centerOfMass.y < center-0.1 && !check){
        	count += 1;
        	check = true;
        }
        SetText();
    }

    void SetText(){
    	countText.text = "Count: " + count.ToString();
    	if(count >= 10){
    		strikeText.text = "Strike!!!!!";
    	}
    }
}
