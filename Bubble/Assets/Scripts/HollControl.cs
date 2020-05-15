using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HollControl : MonoBehaviour
{
    public Text ScoreText;

    private static int score;
    private Rigidbody rb;

    void Start(){
    	score = 0;
    	SetScoreText();
    }

	void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.CompareTag("Holl3")){
            InitMotherBall(other);
            score = score + 3;
            SetScoreText();

        }
        if(this.gameObject.CompareTag("Holl5")){
			InitMotherBall(other);
			score = score + 5;
			SetScoreText();
        }
        if(this.gameObject.CompareTag("Holl9")){
			InitMotherBall(other);
			score = score + 9;
			SetScoreText();
        }
    }

    void SetScoreText(){
    	ScoreText.text = "Score: " + score.ToString();
    }

    void InitMotherBall(Collider other){
        other.gameObject.transform.position = new Vector3((float)1.91, (float)-0.821, (float)-3.99);
    	rb = other.gameObject.GetComponent<Rigidbody>();  //取得剛體
    	rb.velocity = Vector3.zero;	//移動速度 =0
    	rb.angularVelocity = Vector3.zero;
    }

}
