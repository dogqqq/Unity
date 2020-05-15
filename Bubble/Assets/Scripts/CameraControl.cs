using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public Text BubbleText;

	public float speed = 6.0f;
	public float forceSpd = 3.0f; //母球 蓄力 速度
	public float distance = 6.0f; //攝影機 離 母球 距離 初始值

	public float xSpeed = 120.0f; //滑鼠左右移動速度
    public float ySpeed = 120.0f; //滑鼠上下移動速度

    public float yMinLimit = -20f;  //滑鼠上下 轉仰角 下限
    public float yMaxLimit = 80f;   //滑鼠上下 轉仰角 上限

    public float distanceMin = .5f;  //滾輪 拉 攝影機 離 母球 距離下限
    public float distanceMax = 15f;  //滾輪 拉 攝影機 離 母球 距離上限

	private Vector3 offset;
	private float force = 0.0f; //母球 蓄力 大小
	private Rigidbody rbody;
	private int bubbleCount = 0;
    private bool moved;
	    
	float x = 0.0f;
	float y = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
    	offset = transform.position - player.transform.position;
    	Vector3 angles = transform.eulerAngles;
    	x = angles.y;
    	y = angles.x;
    	rbody = player.GetComponent<Rigidbody>();
        moved = false;
    	SetBubbleText();
    }

    // Update is called once per frame
    void LateUpdate(){
    	float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if(moveVertical != 0.0f || moveHorizontal != 0.0f){
        	Application.LoadLevel(0);
        }
        if(rbody.position.y <= -5 || (rbody.velocity == new Vector3(0.0f, 0.0f, 0.0f) && moved)){
            InitMotherBall();
        }
        if(Input.GetMouseButton(0)){
        	x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
        	y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        	y = ClampAngle(y, yMinLimit, yMaxLimit);
        	Quaternion rotation = Quaternion.Euler(y, x, 0);
        	distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
        	Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        	offset = rotation * negDistance; //依新角度，距離 重新算相對位置
			transform.rotation = rotation; // 攝影機 新角度
        }
        transform.position = player.transform.position + offset;
        if (Input.GetMouseButton(1))  // 按滑鼠右鍵 按住 蓄力
        {
			force += Time.deltaTime*forceSpd; // 大小和時間成正比
  		}
        else if (Input.GetMouseButtonUp(1))  // 按滑鼠右鍵 放開 發射
        {
        	bubbleCount += 1;
        	SetBubbleText();
            moved = true;
  		//眼睛看的方向the direction of camera(eye)：
		// Camera.main.transform.forward
            Vector3  movement = Camera.main.transform.forward;
			movement.y = 0.0f;      // no vertical movement 不上下移動
			//力量模式impulse:衝力，speed：初速大小
            rbody.AddForce(movement * speed * force, ForceMode.Impulse);
			force = 0.0f;  // 力量用盡歸0，準備下次重新蓄力
        }

    }
    public static float ClampAngle(float angle, float min, float max)
    { // 用上下限 夾值
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    void SetBubbleText(){
    	BubbleText.text = "Bubbles: " + bubbleCount.ToString();
    }

    void InitMotherBall(){
        player.gameObject.transform.position = new Vector3((float)1.91, (float)-0.821, (float)-3.99);
        rbody.velocity = Vector3.zero; //移動速度 =0
        rbody.angularVelocity = Vector3.zero;
        moved = false;
    }
}
