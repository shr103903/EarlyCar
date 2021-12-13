using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMove : MonoBehaviour
{
    public int speed;

    public ButtonCtrl btnCtrl;
    public GameManager gameManager;

    public GameObject nextStagePanel;
    public Transform respawn;
    public float maxTorque;
    public Transform centerOfMass;
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];
    public float setNextTime = 0;
    public float clearTime = -1;
    float steer;
    float accelerate;


    void Start()
    {
        //무게중심
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
    }


    void FixedUpdate()
    {
        //자동차 이동
        if(btnCtrl.isPlaying == true)
        {
            steer = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            accelerate = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            float finalAngle = steer * 45;
            wheelColliders[0].steerAngle = finalAngle;
            wheelColliders[1].steerAngle = finalAngle;

            for (int i = 0; i < 4; i++)
            {
                wheelColliders[i].motorTorque = accelerate * maxTorque;
            }

            //Ctrl키를 누르면 브레이크 작동
            if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))
            {
                for (int i = 0; i < 4; i++)
                {
                    wheelColliders[i].brakeTorque = maxTorque;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    wheelColliders[i].brakeTorque = 0;
                }
            }
        }
    }

    void Update()
    {
        //타이어 회전
        for (int i = 0; i < 4; i++)
        {
            Vector3 pos;
            Quaternion quat;

            wheelColliders[i].GetWorldPose(out pos, out quat);

            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;
        }

        //스테이지 넘어갈 때 5초 후 자동 이동
        setNextTime -= Time.deltaTime;
        if (nextStagePanel.activeSelf)
        {
            if(setNextTime <= 0)
            {
                nextStagePanel.SetActive(false);
                SetPlace();
                btnCtrl.isPlaying = true;
                gameManager.playTime = 0;
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        //골에 도착하면 다음 스테이지 이동
        if(collision.transform.tag == "Goal")
        {
            clearTime = gameManager.playTime;
            GetNext();
        }
        //빨간 장애물에 부딪히면 게임 오버
        else if(collision.transform.tag == "Obstacle")
        {
            if(btnCtrl.isPlaying == true)
            {
                gameManager.Die();
                btnCtrl.isPlaying = false;
            }
        }
    }

    //바닥에서 떨어지면 시작점 위치에서 부활
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Fall")
        {
            SetPlace();
        }
    }

    //다음 스테이지로 변화
    void GetNext()
    {
        SetPlace();
        nextStagePanel.SetActive(true);
        btnCtrl.isPlaying = false;
        setNextTime = 5.0f;             //5초 후 자동 이동
        gameManager.MakeObstacles();    //장애물 생성 초기화
        gameManager.stageNum++;
    }

    //시작점 위치로 자동차 이동
    public void SetPlace()
    {
        transform.position = respawn.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        steer = 0;
        accelerate = 0;
    }
}
