using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EatBall : MonoBehaviour
{

	public Text In_BallText;
    public Text ScoreText;
    public Text winText;

    private static int count;
    private static int score;
    private Rigidbody rb;

    void Start(){
    	count = 0;
    	score = 0;
    	SetScoreAndBallText();
    	winText.text = "";
    }

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball")){
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score + 1;
            SetScoreAndBallText();
            if(count >= 9){
            	winText.text = "You Win!";
            }
        }
        if(other.gameObject.CompareTag("MotherBall")){
			InitMotherBall(other);
			score = score - 1;
			SetScoreAndBallText();
        }
    }

    void SetScoreAndBallText(){
    	In_BallText.text = "In-Ball: " + count.ToString();
    	ScoreText.text = "Score: " + score.ToString();
    }

    void InitMotherBall(Collider other){
        other.gameObject.transform.position = new Vector3((float)0, (float)3, (float)0);
    	rb = other.gameObject.GetComponent<Rigidbody>();  //取得剛體
    	rb.velocity = Vector3.zero;	//移動速度 =0
    	rb.angularVelocity = Vector3.zero;
    }

}
