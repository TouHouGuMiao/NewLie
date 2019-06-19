using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    public static BattleCamera Instance;

    public bool isUseCamera=false;

    private int SightDistanceSpeed = 15;
    private bool CameraIsLock = false;
    private float RectSize = 50f;
    private float CameraMoveSpeed = 0.5f;
    private Transform Player;
    private float CarmeraFieldOfView = 60;
    private Camera camera;

    private Rect RectCenter;
    private Rect RectUp;
    private Rect RectDown;
    private Rect RectLeft;
    private Rect RectRight;

    private Vector3 playerVecState;

    public bool isRightStop = false;

    public bool isLeftStop = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RectUp = new Rect(0, Screen.height - RectSize, Screen.width, Screen.height);
        RectDown = new Rect(0, 0, Screen.width, RectSize);
        RectLeft = new Rect(0, 0, RectSize/2, Screen.height);
        RectRight = new Rect(Screen.width - RectSize, 0, RectSize/2, Screen.height);
        RectCenter = new Rect(Screen.width / 2, 0, 20, Screen.height);
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        camera = this.GetComponent<Camera>();
        playerVecState = Player.transform.position;
        StartCoroutine(StopCameraMoveUseCameraBollider());
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //{

        //    CameraIsLock = !CameraIsLock;
        //}

        UpdataPlayerOffest();
        //CameraMoveAndLock();
    }

 

    IEnumerator StopCameraMoveUseCameraBollider()
    {
        yield return new WaitForSeconds(0.1f);
        Collider[] colliderArrary = Physics.OverlapSphere(camera.transform.position, 20, 1<<LayerMask.NameToLayer("CameraCollider"), QueryTriggerInteraction.Collide);
        for (int i = 0; i < colliderArrary.Length; i++)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(colliderArrary[i].transform.position);
            if (RectRight.Contains(screenPos))
            {
               
                isRightStop = true;
            }

            else
            {
                isRightStop = false;
            }
            
            if (RectLeft.Contains(screenPos))
            {
                isLeftStop = true;
            }

            else
            {
                isLeftStop = false;
            }
        }
        StartCoroutine(StopCameraMoveUseCameraBollider());
    }

    void CameraMoveAndLock()
    {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        //}

        if (!CameraIsLock)
        {
            if (RectUp.Contains(Input.mousePosition))
            {
                Vector2 playerScreenVec = Camera.main.WorldToScreenPoint(Player.position);
                if (RectDown.Contains(playerScreenVec))
                {
                    return;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y + CameraMoveSpeed, transform.position.z);
            }

            if (RectDown.Contains(Input.mousePosition))
            {
                Vector2 playerScreenVec = Camera.main.WorldToScreenPoint(Player.position);
                if (RectUp.Contains(playerScreenVec))
                {
                    return;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y - CameraMoveSpeed, transform.position.z);
            }

            if (RectLeft.Contains(Input.mousePosition))
            {
                Vector2 playerScreenVec = Camera.main.WorldToScreenPoint(Player.position);
                if (RectRight.Contains(playerScreenVec))
                {
                    return;
                }
                transform.position = new Vector3(transform.position.x - CameraMoveSpeed, transform.position.y, transform.position.z);
            }

            if (RectRight.Contains(Input.mousePosition))
            {
                Vector2 playerScreenVec = Camera.main.WorldToScreenPoint(Player.position);
                if (RectLeft.Contains(playerScreenVec))
                {
                    return;
                }
                transform.position = new Vector3(transform.position.x + CameraMoveSpeed, transform.position.y, transform.position.z);
            }
        }
    }



    void UpdataPlayerOffest()
    {
        if (playerVecState != Player.position)
        {
            Vector3 offsetVec = Player.position - playerVecState;

            if (isLeftStop)
            {
                if (Player.position.x < playerVecState.x)
                {
                    offsetVec = Vector3.zero;
                }
            }

            if (isRightStop)
            {
                if (Player.position.x > playerVecState.x)
                {
                    offsetVec = Vector3.zero;
                }
            }

            playerVecState = Player.position;
            
            offsetVec = new Vector3(offsetVec.x, offsetVec.y, 0);
            transform.position = transform.position + offsetVec;
        }     
    }

    public enum MoveEnum
    {
        left=0,
        right=1,
    }
    #region 提供给外部的功能
    /// <summary>
    /// 移动相机，当相机触碰到Rect触碰到玩家或者特殊Collider的时候停止移动
    /// </summary>
    public void MoveCamera_StopWhenRectCashPlayerOrCollider(MoveEnum moveEnum,float speed, StoryHander OnMoveFished = null)
    {
        isUseCamera = true;
        StartCoroutine(MoveCamera_StopWhenRectCashPlayerOrCollider_IEnumerator(moveEnum,speed, OnMoveFished));
    }

    IEnumerator MoveCamera_StopWhenRectCashPlayerOrCollider_IEnumerator(MoveEnum moveEnum,float speed,StoryHander OnMoveFished=null)
    {
        yield return new WaitForSeconds(0.01f);

        if(moveEnum== MoveEnum.right)
        {
            transform.Translate(1 * Time.deltaTime*speed, 0, 0);
            if (RectLeft.Contains(Camera.main.WorldToScreenPoint(Player.transform.position)))
            {
                isUseCamera = false;
         
                if (OnMoveFished != null)
                {    
                    OnMoveFished();
                }
                yield break;
            }
            else if (RectRight.Contains(Camera.main.WorldToScreenPoint(Player.transform.position)))
            {
                isUseCamera = false;
                if (OnMoveFished != null)
                {
                    OnMoveFished();
                }
                yield break;
            }
        }

        else if(moveEnum == MoveEnum.left)
        {
            transform.Translate(-1 * Time.deltaTime*speed, 0, 0);
            if (RectLeft.Contains(Camera.main.WorldToScreenPoint(Player.transform.position)))
            {
                isUseCamera = false;
                yield break;
            }
            else if (RectRight.Contains(Camera.main.WorldToScreenPoint(Player.transform.position)))
            {
                isUseCamera = false;
                yield break;
            }
        }
       StartCoroutine(MoveCamera_StopWhenRectCashPlayerOrCollider_IEnumerator(moveEnum,speed, OnMoveFished));
    }
    #endregion
}
