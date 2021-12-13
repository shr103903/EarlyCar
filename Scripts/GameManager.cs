using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ButtonCtrl btnCtrl;
    public CarMove car;

    public GameObject startMenu;
    public GameObject gameOver;

    public int stageNum = 1;
    public float playTime;
    public Text currentStageText;
    public Text moveStageText;
    public Text timeNext;
    public Text clearTimeText;
    public Text recordStageText;
    public Text recordTimeText;
    float recordTime = -1;
    int recordStageNum = -1;
    public int finishStageNum = -1;

    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;
    public GameObject cube6;
    public GameObject cube7;
    public GameObject cube8;


    void Start()
    {
        btnCtrl.isPlaying = false;     //isPlaying이 true면 자동차 이동 조작 가능, 현재 플레이 중임을 표시
        startMenu.SetActive(true);
        Time.timeScale = 1;
    }

    void Update()
    {
        playTime += Time.deltaTime;

        currentStageText.text = stageNum.ToString("000");        //현재 플레이 중인 스테이지
        moveStageText.text = "Clear Stage" + (stageNum - 1).ToString("000") + "!!!";     //방금 클리어한 스테이지
        timeNext.text = ">>" + ((int)car.setNextTime + 1) + "s";      //방금 클리어한 스테이지 클리어 시간

        LoadRecord();          //시작화면에 기록을 변경
    }

    public void MakeObstacles()
    {
        //8개의 큐브에서 발생하는 장애물 랜덤하게 변경
        cube1.GetComponent<MakeObstacles>().r = Random.Range(1, 6);
        cube2.GetComponent<MakeObstacles>().r = Random.Range(1, 6);
        cube3.GetComponent<MakeObstacles>().r = Random.Range(1, 6);
        cube4.GetComponent<MakeObstacles>().r = Random.Range(1, 6);
        cube5.GetComponent<MakeObstacles>().r = Random.Range(1, 6);
        cube6.GetComponent<MakeObstacles>().r = Random.Range(1, 6);
        cube7.GetComponent<MakeObstacles>().r = Random.Range(1, 6);
        cube8.GetComponent<MakeObstacles>().r = Random.Range(1, 6);
    }

    //게임 오버
    public void Die()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);

        finishStageNum = stageNum;
    }

    public void LoadRecord()
    {
        
        if (stageNum >= 2)
        {
            recordTime = car.clearTime;
            //방금 클리어한 스테이지 기록 출력
            clearTimeText.text = "기록: " + (recordTime / 60).ToString("00") + ":"
                                + (recordTime % 60).ToString("00") + ":"
                                + (recordTime * 100 % 60).ToString("00");
            recordTimeText.text = (recordTime / 60).ToString("00") + "분" + (recordTime % 60).ToString("00") + "초";
        }
        else if (recordTime == -1)
        {
            //게임 초기에는 스테이지 클리어 기록 없음
            recordTimeText.text = "기록 없음";
        }
        else if (recordTime > car.clearTime)
        {
            //기록 갱신
            recordTime = car.clearTime;
            recordTimeText.text = recordTime / 60 + "분" + recordTime % 60 + "초";
        }

        if (finishStageNum != -1)
        {
            //기록 갱신
            if (recordStageNum <= finishStageNum)
            {
                recordStageNum = finishStageNum;
                recordStageText.text = recordStageNum.ToString("000");
            }
        }
        else if (recordStageNum == -1)
        {
            //게임 초기에는 스테이지 기록 없음
            recordStageText.text = "기록 없음";
        }
    }
}
