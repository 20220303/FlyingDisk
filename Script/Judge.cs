using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//判断游戏胜利情况
public class Judge
{
    //0->stop,1->Round1,2->Round2
    private int status;
    private int is_win;


    //这个函数也可以用于重新开始
    public void initJudge()
    {
        this.status = 1;
        this.is_win = 1;
        Debug.Log("initJudge: " + status);
    }

    public void setStatus(int status_)
    {
        status = status_;
    }

    

    public int getStatus()
    {
        return status;
    }

    public void setIs_Win(int is_win_)
    {
        is_win = is_win_;
    }

    public int getIs_Win()
    {
        return is_win;
    }


    //判断能否进入下一回合
    //1->进入Round2，2->获胜,0->失败
    public void WinGame(int trails)
    {
        if(status==1 && is_win == 1 && trails==0)//下一轮
        {
            status = 2;
        }
        else if(status == 2 && is_win == 1 && trails == 0)//胜利
        {
            status = 3;
        }
        else if (is_win == 0)//失败
        {
            status = -1;
        }
        else//继续
        {
        }

    }


}
