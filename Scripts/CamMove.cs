using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{

    public int dist;
    public int height;
    public Transform car;
    Vector3 offset;

    void Update()
    {
        //자동차 따라 카메라 이동
        offset = car.forward * dist + Vector3.down * height;
        transform.position = Vector3.MoveTowards(transform.position, car.position - offset, 5f);

        transform.LookAt(car);
    }
}
