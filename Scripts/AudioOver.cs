using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOver : MonoBehaviour
{
    public GameObject gameOver;

    void Update()
    {
        //게임 오버이면 기본 배경음 정지. GameOver 배경음 출력
        if (gameOver.activeSelf)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
