using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiskFactor : MonoBehaviour
{
    public DiskData saucer;
    public static int num = 8;
    //创建一个数组来保存
    DiskData[] DiskList = new DiskData[num];
    static int prepared = num + 1;

    //初始化工厂
    public void initFactory()
    {
        for (int i = 0; i < num; i++)
        {
            DiskList[i] = Instantiate(saucer);
            DiskList[i].gameObject.SetActive(false);
        }
    }


    public bool isPrepared()
    {
        prepared = num + 1;
        for (int i = 0; i < num; i++)
        {
            if (DiskList[i].gameObject.activeSelf == false)
            {
                prepared = i;
                break;
            }
        }
        if (prepared < num)
            return true;
        else
            return false;
    }

    public void getDisk(Attributes attributes)
    {

        if (prepared < num)
        {
            DiskList[prepared].attributes = attributes;
            Debug.Log("DiskList[prepared].attributes: " + DiskList[prepared].gameObject.GetComponent<Renderer>().material.name);
            DiskList[prepared].gameObject.SetActive(true);
        }
    }



    public int freeDisk()
    {
        int flag = 1;
        for (int i = 0; i < num; i++)
        {
            if (DiskList[i].gameObject.transform.position.y < -2 ||
                DiskList[i].gameObject.transform.position.y > 60 ||
                DiskList[i].gameObject.transform.position.x < -80 ||
                DiskList[i].gameObject.transform.position.x > 80 ||
                DiskList[i].gameObject.transform.position.z < -10)
            {
                DiskList[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                DiskList[i].gameObject.SetActive(false);
                DiskList[i].gameObject.transform.position = Vector3.zero;
                //设置不能通关
                flag = 0;

            }
        }
        return flag;
    }

    public void freeDisk(DiskData disk)
    {
       
        disk.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        disk.gameObject.SetActive(false);
        disk.gameObject.transform.position = Vector3.zero;

    }

    //释放所有的飞碟，restart用
    public void freeAllDisk()
    {

        for (int i = 0; i < num; i++)
        {
           
            DiskList[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            DiskList[i].gameObject.SetActive(false);
            DiskList[i].gameObject.transform.position = Vector3.zero;

        }

    }


    public void runDisk()
    {
        for (int i = 0; i < num; i++)
        {
            if (DiskList[i].gameObject.activeSelf == true)
            {
                Rigidbody rb = DiskList[i].GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForce(Vector3.down * 9.8f);
                    rb.AddExplosionForce(30f * DiskList[i].attributes.speed, DiskList[i].attributes.direction, 10);
                }
            }
        }
    }



}
