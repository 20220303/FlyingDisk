using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Director : System.Object
{
    //µ¼ÑÝ£¬¿ØÖÆ³¡¾°ÇÐ»»
    private static Director _instance;
    public SceneController currentSceneController { get; set; }

    public bool running { get; set; }

    public static Director getInstance()
    {
        if (_instance == null)
        {
            _instance = new Director();
        }
        return _instance;
    }

    public int getFPS()
    {
        return Application.targetFrameRate;
    }

    public void setFPS(int fps)
    {
        Application.targetFrameRate = fps;
    }

    public void NextScene()
    {

    }
}







