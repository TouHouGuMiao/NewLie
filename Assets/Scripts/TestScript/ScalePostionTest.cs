using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePostionTest : MonoBehaviour {
    public Vector3 from=Vector3.one;
    public Vector3 to=Vector3.one;
    public float Duration = 0;

    private float time_f=0;
    private float factor=0;
    private float time_delay;

    [HideInInspector]
    public bool IsActive
    {
        get
        {
            return isStart;
        }
    }

    public float delay = 0;
    private bool isStart = false;

    public delegate void OnFishedDelegate();

    public OnFishedDelegate OnFished;

    private void Update()
    {

        if (isStart)
        {
            if (Duration == 0)
            {
                return;
            }
            time_f += Time.deltaTime;
            if (time_f >= Duration)
            {
                if (OnFished != null)
                {
                    OnFished();
                }
                isStart = false;
            }
            Vector3 tempScale = Vector3.Lerp(from, to, factor);
            factor = (time_f / Duration);
            Vector3 tempPos = (transform.localScale - tempScale) / 2;
            transform.localScale = tempScale;
            transform.localPosition += tempPos;
        }

        if (delay >= 0)
        {
            if (time_delay >= delay)
            {
                isStart = true;
                return;
            }
            time_delay += Time.deltaTime;
        }

    }

    public void ResetToBegin()
    {
        time_f = 0;
        time_delay = 0;
        isStart = true;
    }
}
