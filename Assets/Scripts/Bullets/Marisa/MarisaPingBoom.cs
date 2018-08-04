using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaPingBoom
{
    private GameObject pingPrefab;


    public void Init()
    {
        pingPrefab = ResourcesManager.Instance.LoadBullet("PingZi");
    }

    public void ShowSkill(Vector3 pointVec,Transform marisaTF)
    {
        GameObject go = GameObject.Instantiate(pingPrefab);
        go.transform.position = pointVec;
        ForceBoomBullet forceBullet = go.GetComponent<ForceBoomBullet>();
        if (marisaTF.rotation.eulerAngles.y == 180)
        {
            forceBullet.forceVec = new Vector2(-1, 2)*300;
        }

        else
        {
            forceBullet.forceVec = new Vector2(1, 2)*300;
        }
    }
}
