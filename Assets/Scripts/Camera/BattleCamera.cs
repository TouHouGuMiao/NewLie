using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    private int SightDistanceSpeed = 15;
    private bool CameraIsLock = false;
    private float RectSize = 50f;
    private float CameraMoveSpeed = 0.5f;
    private Transform Player;
    private float CarmeraFieldOfView = 60;
    private Camera camera;

    private Rect RectUp;
    private Rect RectDown;
    private Rect RectLeft;
    private Rect RectRight;

    private Vector3 playerVecState;
    private void Start()
    {
        RectUp = new Rect(0, Screen.height - RectSize, Screen.width, Screen.height);
        RectDown = new Rect(0, 0, Screen.width, RectSize);
        RectLeft = new Rect(0, 0, RectSize, Screen.width);
        RectRight = new Rect(Screen.width - RectSize, 0, Screen.width, Screen.height);

        Player = GameObject.FindGameObjectWithTag("Player").transform;

        camera = this.GetComponent<Camera>();
        playerVecState = Player.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {

            CameraIsLock = !CameraIsLock;
        }
        UpdataPlayerOffest();
        CameraMoveAndLock();
    }

    void CameraMoveAndLock()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        }

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
           
            transform.position = transform.position + offsetVec;
            playerVecState = Player.position;
        }


       
        
    }
}
