using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneController : MonoBehaviour
{
    public Text Score;
    DiskFactor diskFactor;
    Director director;
    Color[] colors = { Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };
    public Material[] materials;
    float count = 0;
    Attributes attributes;
    ScoreRecorder scoreRecorder;
    Judge judge;
    public float interval1 = 2f;
    public float interval2 = 4f;
    private int trails1 = 10;
    private int trails2 = 10;

    private void Awake()
    {
        
        director = Director.getInstance();
        director.setFPS(30);
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
        scoreRecorder = new ScoreRecorder();
        judge = new Judge();
        judge.initJudge();
        judge.setStatus(1);

    }

    private void LoadResources()
    {
        diskFactor = Singleton<DiskFactor>.instance;
        diskFactor.initFactory();

    }

    private void Update()
    {
       
        if(judge.getStatus() == 2)//Round2
        {


            //Debug.Log("judge.getStatus() == 2: " + judge.getIs_Win() + "  " + judge.getStatus());
            count += Time.deltaTime;
            if (count >= interval2)
            {
                //释放两个飞盘
                if (diskFactor.isPrepared())
                {
                    attributes = GetAttributes2();
                    diskFactor.getDisk(attributes);
                }

                if (diskFactor.isPrepared())
                {
                    attributes = GetAttributes2();
                    diskFactor.getDisk(attributes);
                }
                count = 0;
            }

            //释放飞盘
            if (diskFactor.freeDisk() == 0) judge.setIs_Win(0);

            //集中事件检测
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    scoreRecorder.addScore(hit.transform.gameObject.GetComponent<DiskData>().attributes.score);

                    diskFactor.freeDisk(hit.transform.gameObject.GetComponent<DiskData>());
                    setTextContent();
                    trails2--;
                }
            }
            judge.WinGame(trails2);


            
        }
        else if(judge.getStatus() == 1)//Round1
        {
            //Debug.Log(": " + judge.getIs_Win() + "  " + judge.getStatus());
            count += Time.deltaTime;
            if (count >= interval1)
            {
                if (diskFactor.isPrepared())
                {
                    attributes = GetAttributes();
                    diskFactor.getDisk(attributes);
                }
                count = 0;
            }

            //释放飞盘
            if (diskFactor.freeDisk() == 0) judge.setIs_Win(0);


            //集中事件检测
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    scoreRecorder.addScore(hit.transform.gameObject.GetComponent<DiskData>().attributes.score);

                    diskFactor.freeDisk(hit.transform.gameObject.GetComponent<DiskData>());
                    setTextContent();
                    trails1--;
                }
            }
            judge.WinGame(trails1);
            //Debug.Log("judge.getStatus() == 1: " + judge.getIs_Win() + "  " + judge.getStatus());
        }

        
    }



    private void FixedUpdate()
    {
        diskFactor.runDisk();
    }

    private Attributes GetAttributes()
    {
        float size = Random.Range(4f, 5f);
        Color color = colors[Random.Range(0, 9)];
        Material material= materials[Random.Range(0, 13)];
        Vector3 position = new Vector3(0, 5, 100);
        Vector3 direction = new Vector3(Random.Range(-10f, 10f), Random.Range(4f, 7f), Random.Range(-8f, -5f));

        direction.Normalize();
        direction = position - direction;

        float speed = Random.Range(1f, 2f);

        return new Attributes(size, color, position, direction, speed, material);
    }

    private Attributes GetAttributes2()
    {
        float size = Random.Range(4f, 4.5f);
        Color color = colors[Random.Range(0, 9)];
        Material material = materials[Random.Range(0, 13)];
        Vector3 position = new Vector3(0, 5, 100);
        Vector3 direction = new Vector3(Random.Range(-10f, 10f), Random.Range(4f, 7f), Random.Range(-8f, -5f));

        direction.Normalize();
        direction = position - direction;

        float speed = Random.Range(1f, 2f);

        return new Attributes(size, color, position, direction, speed, material);
    }

    void setTextContent()
    {
        print("Score: " + scoreRecorder.getScore());
        Score.text = "Score: " + scoreRecorder.getScore().ToString();
    }
    public int getStatus()
    {
        return judge.getStatus();
    }

    public int getScore()
    {
        return scoreRecorder.getScore();
    }

    //重新开始
    public void restart()
    {
        //重新设置比分
        scoreRecorder.setScore(0);
        setTextContent();
        judge.initJudge();

        //重新设置飞盘
        diskFactor.freeAllDisk();
        trails1 = 10;
        trails2 = 10;
    }

    //游戏结束
    public void stopGame(int status_)
    {
        judge.setStatus(status_);
        diskFactor.freeAllDisk();
    }

    //退出游戏
    public void GetExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}

