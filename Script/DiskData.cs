using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Attributes
{
    //飞盘大小
    public float size;
    //飞盘颜色
    public Color color;
    //发射飞盘的位置
    public Vector3 position;
    //飞盘的发射方向
    public Vector3 direction;
    //飞盘速度
    public float speed;
    //飞碟材料
    public Material material;

    //打击飞盘的分数
    public int score;

    //构造函数
    public Attributes(float size, Color color, Vector3 position, Vector3 direction, float speed, Material material)
    {
        this.size = size;
        this.color = color;
        this.material = material;
        this.position = position;
        this.direction = direction;
        this.speed = speed;
        this.score = ((int)(this.speed + 10 - this.size));
    }
}



public class DiskData : MonoBehaviour
{

    Attributes _attributes;

    public Attributes attributes
    {
        get
        {
            return _attributes;
        }
        set
        {
            _attributes = value;
            this.gameObject.GetComponent<Transform>().position = value.position;
            this.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1) * value.size;
            //this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = value.color;
            //this.gameObject.GetComponent<Renderer>().material = value.material;
            this.GetComponentsInChildren<Renderer>()[0].materials[0] = value.material;

            Transform[] father = this.gameObject.GetComponentsInChildren<Transform>();

            foreach (var child in father)
            {
                child.GetComponent<Renderer>().material = value.material;
            }


            this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = value.material;
            Debug.Log("this.gameObject.GetComponentsInChildren<Renderer>()[0].materials[0]: " + value.material.name + this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name);
            //Debug.Log("this.gameObject.GetComponent<Renderer>().materials[0]: " + value.material.name);
            //this.gameObject.GetComponent<Renderer>().sharedMaterial = value.material;
        }
    }

}
