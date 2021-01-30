using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform leftEnd;
    public Transform rightEnd;


    private void Update()
    {
        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;
        targetPos.z = transform.position.z;

        if (targetPos.x < leftEnd.position.x || targetPos.x > rightEnd.position.x) return;
        transform.position = targetPos;
    }
}
