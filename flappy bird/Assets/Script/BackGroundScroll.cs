using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;
    Transform scrollTrans;

    void Start()
    {
        startPosition = transform.position;
        scrollTrans = GetComponent<Transform>();
    }

    void Update()
    {
        if(!GameMng.Ins.bpause && !GameMng.Ins.bCount)
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
            transform.position = startPosition + Vector3.right * newPosition;
        }
    }
}
