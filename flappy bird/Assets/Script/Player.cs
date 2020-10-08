using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D PlayerRigid;
    Transform PlayerTrans;
    SpriteRenderer PlayerRenderer;
    AudioSource PlayerAudio;

    public AudioClip JumpSnd;
    public AudioClip Deadsnd;

    public float fJumpPower;
    bool bJUmp = true;

    void Start()
    {
        PlayerRigid = GetComponent<Rigidbody2D>();
        PlayerTrans = GetComponent<Transform>();
        PlayerRenderer = GetComponent<SpriteRenderer>();
        PlayerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameMng.Ins.bpause && !GameMng.Ins.bCount && bJUmp)
        {
            Jump();
        }
        JumpRot();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRigid.velocity = new Vector2(0, fJumpPower);
            PlayerAudio.clip = JumpSnd;
            PlayerAudio.Play();
        }
    }

    void JumpRot()
    {
        PlayerTrans.rotation = Quaternion.Euler(0.0f, 0.0f, PlayerRigid.velocity.y * 5);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Score"))
        {
            GameMng.Ins.ScoreAdd();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Obstacle"))
        {
            GameMng.Ins.bpause = true;
            PlayerAudio.clip = Deadsnd;
            if (!PlayerAudio.isPlaying)
                PlayerAudio.Play();
            GameMng.Ins.GameOver();
        }
    }
}