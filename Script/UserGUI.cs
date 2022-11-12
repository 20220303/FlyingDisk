using System.Collections;
using System.Collections.Generic;
using UnityEngine;




class UserGUI : MonoBehaviour
{
    
    private Director director;
    private SceneController currentScene;
    private int status;
    //设置标签的样式
    GUIStyle style;
    GUIStyle titleStyle;



    void Start()
    {
        currentScene = Director.getInstance().currentSceneController;
        status = currentScene.getStatus();
        style = new GUIStyle();
        style.fontSize = 60;
        style.alignment = TextAnchor.MiddleCenter;

        titleStyle = new GUIStyle();
        style.fontSize = 30;
        style.alignment = TextAnchor.MiddleCenter;

    }

    void OnGUI()
    {

        if(status==1 || status == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 60, 15, 120, 40), "      Round: "+status, titleStyle);
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 60, 15, 120, 40), "Playing Flying Disk", titleStyle);
        }
        

        if (GUI.Button(new Rect(15, 45, 60, 30), "Start"))
        {
            status = 1;
            currentScene.restart();
        }

        if (GUI.Button(new Rect(15, 85, 60, 30), "Exit"))
        {
            status = 0;
            currentScene.GetExit();
        }

        status = currentScene.getStatus();


        if (status == -1)
        {
            currentScene.stopGame(status);

            GUI.Label(new Rect(Screen.width / 2-50, Screen.height / 2-20,100, 40), "Try Again!",style);
      

        }
        else if(status == 3)
        {
            currentScene.stopGame(status);
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 100, 40), "Conguatulation!",style);
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 20, 100, 40), "   Score: "+ currentScene.getScore(), style);
        }
        

    }
}
