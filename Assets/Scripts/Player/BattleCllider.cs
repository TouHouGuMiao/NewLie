using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BattleCllider : MonoBehaviour
{
    private Transform battleCollider;
    private float radius = 3.0F;
    public GameObject startGO;
    public GameObject mouse;

    bool IsOutSphere
    {
        get
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float dependFloat = (Mathf.Pow(mouseWorldPos.x - battleCollider.position.x, 2)) + (Mathf.Pow(mouseWorldPos.y - battleCollider.position.y, 2));
            if (dependFloat <= (radius * radius))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    private void Awake()
    {
        battleCollider = GameObject.FindWithTag("BattleCollider").transform;
        Cursor.visible = false;
    }
    // Use this for initialization
    void Start()
    {


        //AudioManager.Instance.PlayBg_Source("Religion");
        startGO.SetActive(true);


    }
    [System.Runtime.InteropServices.DllImport("user32.dll")] //引入dll
    public static extern int SetCursorPos(int x, int y);
    // Update is called once per frame
    void Update()
    {
        if (IsOutSphere)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2((mouseWorldPos.y - battleCollider.position.y), (mouseWorldPos.x - battleCollider.position.x));
            float x = battleCollider.position.x + Mathf.Cos(angle) * radius;
            float y = battleCollider.position.y + Mathf.Sin(angle) * radius;
           
            mouse.transform.position = new Vector2(x,y);
        }

        else
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.transform.position = mouseWorldPos;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {



        }
    }
}
