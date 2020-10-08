using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Transform ObstacleTrans;
    Rigidbody2D ObstacleRigid;
    bool bUse = false;

    void Update()
    {
        if(GameMng.Ins.bpause)
            ObstacleRigid.velocity = new Vector2(0, 0);
        else
            ObstacleRigid.velocity = new Vector2(-3.5f, 0);

        if (transform.position.x <= -5.0f)
            Dead();
    }

    public void Dead()
    {
        bUse = false;
        gameObject.SetActive(false);
    }

    public bool GetUse()
    {
        return bUse;
    }

    public void CreateObstacle(Vector3 spawnpos)
    {
        if(ObstacleRigid == null || ObstacleTrans == null)
        {
            ObstacleRigid = GetComponent<Rigidbody2D>();
            ObstacleTrans = GetComponent<Transform>();
        }

        gameObject.SetActive(true);

        transform.position = spawnpos;

        bUse = true;

    }
}
