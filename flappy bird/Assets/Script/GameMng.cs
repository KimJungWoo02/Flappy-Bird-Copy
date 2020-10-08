using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMng : MonoBehaviour
{
    List<Obstacle> ObstacleList = new List<Obstacle>();
    public GameObject OriginalObject;
    public GameObject ObstacleMng;

    AudioSource GameAudio;

    public GameObject Menu;
    public GameObject Result;
    public GameObject playerObj;

    public Text Score;
    public Text Score2;
    public Text CountText;

    public Image newImg;

    public bool bCount;
    public bool bpause;
    public int nScore = 0;

    int nHighScore = 0;
    int nCount = 4;
    float fObstacleCreateTime = 0;
    float fCountCheckTime = 0;


    private static GameMng gamemng;
    public static GameMng Ins
    {
        get
        {
            if (gamemng == null)
            {
                gamemng = FindObjectOfType<GameMng>();

                if (gamemng == null)
                {
                    GameObject gameobj = new GameObject();
                    gameobj.name = "gameMng";
                    gamemng = gameobj.AddComponent<GameMng>();
                }
            }
            return gamemng;
        }
    }

    void Start()
    {
        GameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (bCount)
        {
            Count();
        }

        if (!bpause)
        {
            fObstacleCreateTime += Time.deltaTime;

            if (fObstacleCreateTime >= 1.25f && !bCount)
            {
                fObstacleCreateTime = 0;
                CreateObstacle();
            }

            Score.text = nScore.ToString();
            Score2.text = nScore.ToString();
        }
    }

    public void CreateObstacle()
    {
        Vector3 spawnpos = ObstacleMng.transform.position + new Vector3(0, Random.Range(-1.8f, 1.8f), 0);
        bool findBullet = false;
        for (int i = 0; i < ObstacleList.Count; i++)
        {
            if (!ObstacleList[i].GetUse())
            {
                ObstacleList[i].CreateObstacle(spawnpos);
                findBullet = true;
                break;
            }
        }


        if (!findBullet)
        {
            Obstacle playerBullet = Instantiate(OriginalObject).GetComponent<Obstacle>();
            ObstacleList.Add(playerBullet);
            playerBullet.transform.parent = ObstacleMng.transform;
            playerBullet.CreateObstacle(spawnpos);
        }
    }


    public void GameOver()
    {
        bpause = true;
        
        Menu.SetActive(false);
        Result.SetActive(true);
        Score.gameObject.SetActive(false);
        Score2.gameObject.SetActive(true);
        ObstacleReset();

        if(nHighScore < nScore)
        {
            nHighScore = nScore;
            newImg.gameObject.SetActive(true);
        }
        else
        {
            newImg.gameObject.SetActive(false);
        }
    }

    public void GameMenu()
    {
        Menu.SetActive(true);
        Result.SetActive(false);
        playerObj.SetActive(false);
        bpause = true;
        ObstacleReset();
    }

    public void GotoGame()
    {
        Menu.SetActive(false);
        Result.SetActive(false);
        playerObj.SetActive(true);
        Score.gameObject.SetActive(true);
        Score2.gameObject.SetActive(false);
        CountText.gameObject.SetActive(true);
        bpause = false;
        bCount = true;
        nCount = 3;
        fCountCheckTime = 0;
        nScore = 0;

        playerObj.transform.position = Vector3.zero;
        playerObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void ScoreAdd()
    {
        nScore++;
        GameAudio.Play();
    }

    void ObstacleReset()
    {
        for (int i = 0; i < ObstacleList.Count; i++) 
        {
            Obstacle obs = ObstacleList[i].GetComponent<Obstacle>();
            obs.Dead();
        }
    }

    void Count()
    {
        if(bCount)
        {
            Rigidbody2D rb = playerObj.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            CountText.text = nCount.ToString();
            fCountCheckTime += Time.deltaTime;

            if (fCountCheckTime >= 1)
            {
                fCountCheckTime = 0;
                nCount--;
            }

            if (nCount == 0)
            {
                bCount = false;
                CountText.gameObject.SetActive(false);
                rb.gravityScale = 2.5f;
            }
        }
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
