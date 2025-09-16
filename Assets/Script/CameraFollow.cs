using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 0, 0);

    void Update()
    {
        // 直接将相机位置设置为玩家位置加上偏移量
        transform.position = player.position + offset;
    }
}
