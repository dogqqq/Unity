using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBall : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball")){
            other.gameObject.SetActive(false);
        }
    }

}
