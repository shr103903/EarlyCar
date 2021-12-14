using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    public CarMove car;
    public GameManager gameManager;

    public GameObject audioManager;
    public GameObject startMenu;
    public GameObject gameOver;
    public GameObject nextStagePanel;
    public GameObject stopPanel;
    public GameObject optionPanel;
    public GameObject quitOkPanel;
    public bool isPlaying;

    //게임 시작 버튼
    public void StartButton()
    {
        car.SetPlace();
        startMenu.SetActive(false);
        isPlaying = true;
        gameManager.playTime = 0;
        Time.timeScale = 1;
    }

    //게임 종료 버튼
    public void ExitButton()
    {
        Application.Quit();
    }

    //시작점 위치로 이동 버튼
    public void RespawnButton()
    {
        car.SetPlace();
    }

    //일시정지 버튼
    public void StopButton()
    {
        Time.timeScale = 0;
        stopPanel.SetActive(true);
    }

    //다음 스테이지로 넘어가는 버튼
    public void NextButton()
    {
        car.setNextTime = 0;
        nextStagePanel.SetActive(false);
        isPlaying = true;
        car.SetPlace();
        gameManager.playTime = 0;
    }

    //일시정지일 때 다시 이어하는 버튼
    public void ResumeButton()
    {
        Time.timeScale = 1;
        stopPanel.SetActive(false);
    }

    //옵션창 여는 버튼
    public void OptionButton()
    {
        optionPanel.SetActive(true);
    }

    //플레이 중 또는 일시정지일 때 현재 플레이 나가기 버튼
    public void QuitButton()
    {
        Time.timeScale = 0;
        quitOkPanel.SetActive(true);
    }

    //나가기 확인
    public void QuitOK()
    {
        if (stopPanel.activeSelf)
        {
            stopPanel.SetActive(false);
        }
        else if (nextStagePanel.activeSelf)
        {
            nextStagePanel.SetActive(false);
        }
        quitOkPanel.SetActive(false);
        startMenu.SetActive(true);

        if(isPlaying == true)
        {
            gameManager.finishStageNum = gameManager.stageNum;
        }
        else
        {
            gameManager.finishStageNum = gameManager.stageNum - 1;
        }

        gameManager.stageNum = 1;
    }

    //나가기 취소
    public void QuitBack()
    {
        quitOkPanel.SetActive(false);
        if (nextStagePanel.activeSelf)
        {
            Time.timeScale = 1;
        }
    }

    //게임 오버화면에서 재시작 버튼
    public void ReStartButton()
    {
        gameManager.stageNum = 1;
        gameOver.SetActive(false);
        car.SetPlace();
        isPlaying = true;
        Time.timeScale = 1;
        audioManager.GetComponent<AudioSource>().Play();   //게임 오버 배경음 멈추고 기본 배경음 다시 출력
    }

    //옵션창에서 뒤로가기 버튼
    public void BackButton_Option()
    {
        optionPanel.SetActive(false);
    }
}
