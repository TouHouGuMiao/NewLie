using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVisWithBattleCamera : MonoBehaviour {


    private void OnBecameVisible()
    {
        if (transform.tag == "rightLock")
        {
            BattleCamera.Instance.isRightStop = true;
        }


        else if (transform.tag == "leftLock")
        {
            BattleCamera.Instance.isLeftStop = true;
        }
    }

    private void OnBecameInvisible()
    {
        if (transform.tag == "rightLock")
        {
            BattleCamera.Instance.isRightStop = false;
        }


        else if (transform.tag == "leftLock")
        {
            BattleCamera.Instance.isLeftStop = false;
        }
    }
}
