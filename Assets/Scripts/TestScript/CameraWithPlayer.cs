using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWithPlayer : MonoBehaviour {
    public Transform Player;
    private Vector3 playerVecState;

    public bool isRightStop = false;

    public bool isLeftStop = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdataPlayerOffest();

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

            else
            {
                playerVecState = Player.position;
            }
            offsetVec = new Vector3(offsetVec.x, 0, offsetVec.z);
            transform.position = transform.position + offsetVec;

        }




    }
}
