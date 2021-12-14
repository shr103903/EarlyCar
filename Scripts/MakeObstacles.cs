using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObstacles : MonoBehaviour
{
    public Transform shootPlace;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public GameObject obstacle4;
    public GameObject obstacle5;
    public int r;
    float time = 0.0f;

    void Start()
    {
        //5개의 장애물 중 하나 생성
        r = Random.Range(1, 6);
    }


    void Update()
    {
        time -= Time.deltaTime;

        //장애물 발사. 시간 간격이 다 똑같지는X
        if(time <= 0)
        {
            if (r == 1)
            {
                Object obj = Instantiate(obstacle1, shootPlace.position, Quaternion.Euler(0, 0, 0));
                Destroy(obj, 10.0f);
                time = 3;
            }
            if (r == 2)
            {
                Object obj = Instantiate(obstacle2, shootPlace.position, Quaternion.Euler(0, 0, 90));
                Destroy(obj, 10.0f);
                time = 4;
            }
            if (r == 3)
            {
                Object obj = Instantiate(obstacle3, shootPlace.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 0));
                Destroy(obj, 10.0f);
                time = 3;
            }
            if (r == 4)
            {
                Object obj = Instantiate(obstacle4, shootPlace.position - new Vector3(0, 0.2f, 0), Quaternion.Euler(0, 0, 0));
                Destroy(obj, 10.0f);
                time = 3;
            }
            if (r == 5)
            {
                Object obj = Instantiate(obstacle5, shootPlace.position + new Vector3(0, 0, 0), Quaternion.Euler(-5, 0, 0));
                Destroy(obj, 10.0f);
                time = 3.5f;
            }
        }  
    }
}
