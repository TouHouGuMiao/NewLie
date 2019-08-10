using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxPanel : IView
{
    private UILabel speakLabel;
    private TypewriterEffect writer;
    public static StoryData data;
    public static DialogManager.DialogDirection direEnmu;
    public static GameObject targetGameObject;
    private GameObject bg;
    protected override void OnStart()
    {
        speakLabel = this.GetChild("sperakLabel").GetComponent<UILabel>();
        writer = speakLabel.GetComponent<TypewriterEffect>();
        bg = this.GetChild("BG").gameObject;
    }

    protected override void OnShow()
    {
        if (data != null)
        {
            speakLabel.text = data.SpeakList[data.index];
        }
        SetDialogBoxPosAndRotate();

    

        speakLabel.gameObject.SetActive(true);





    }

    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {


    }



    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)||Input.GetMouseButtonDown(0))
        {

            if (speakLabel.gameObject.activeSelf == false)
            {
                speakLabel.enabled = true;
                return;
            }
            if (writer.isActive)
            {
                writer.Finish();

            }

            else if (!writer.isActive)
            {
                StoryHander hander = null;
                if (data.StoryHanderDic.TryGetValue(data.index, out hander))
                {
                    speakLabel.text = "";
                    speakLabel.gameObject.SetActive(false);
                    hander();
           
                }
            }
        }
    }


    private void SetDialogBoxPosAndRotate()
    {
        Vector3 screenVec = Camera.main.WorldToScreenPoint(targetGameObject.transform.position);
        screenVec.z = 0;
        Vector3 targetVec = UICamera.currentCamera.ScreenToWorldPoint(screenVec);
        targetVec = targetVec + new Vector3(0, 0.35f, 0);
        if(direEnmu== DialogManager.DialogDirection.left)
        {
            bg.transform.rotation = Quaternion.Euler(0, 180, 0);
            targetVec += new Vector3(0.35f, 0, 0);
        }

        else if(direEnmu == DialogManager.DialogDirection.right)
        {
            bg.transform.rotation = Quaternion.Euler(0, 0, 0);
            targetVec += new Vector3(-0.35f, 0, 0);
        }

        bg.transform.position = targetVec;
        speakLabel.transform.position = targetVec;
        AudioManager.Instance.PlayEffect_Source("sou");
    }
}
