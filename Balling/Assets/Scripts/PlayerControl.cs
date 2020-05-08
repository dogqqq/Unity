using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	private Rigidbody rg;
    // Start is called before the first frame update
    private void Start()
    {
    	rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if(moveVertical != 0.0f || moveHorizontal != 0.0f){
        	Application.LoadLevel(0);
        }

    }

}
