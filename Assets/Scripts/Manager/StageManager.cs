using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instacne;

    private GameObject gameObject=null;
    private Vector2 targetPos;

    private bool isMove=false;

    private void Awake()
    {
        Instacne = this;
    }


    private void Update()
    {
        if (gameObject == null)
        {
            isMove = false;
        }

        if (isMove == true)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, 10*Time.deltaTime);
        }
    }


    public void StageObjNeedMove(GameObject go,Vector2 targetPos)
    {
        gameObject = go;
        this.targetPos = targetPos;
        isMove = true;
    }
}
