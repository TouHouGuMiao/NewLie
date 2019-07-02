using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : IView {
    private UISlider m_Slider;
    private UILabel volumValue;
    private UIButton backBtn;

    public SettingsPanel() {
        m_Layer = Layer.UI;
    }
    protected override void OnStart()
    {
        m_Slider = this.GetChild("BackGround").GetComponent<UISlider>();
        volumValue = this.GetChild("volumeValue").GetComponent<UILabel>();
        backBtn = this.GetChild("backToLogin").GetComponent<UIButton>();

        AddDelegate_Btn();
    }
    protected override void OnShow()
    {
        curren_Value = AudioManager.Instance.BgVolume;
        m_Slider.value = curren_Value;     
    }
    protected override void OnDestroy()
    {

    }
    protected override void OnHide()
    {

    }
    void PlayEffectMusic_1() {
        AudioManager.Instance.PlayEffect_Source("clickAddPoints");
    }
    private void AddDelegate_Btn() {
        m_Slider.onChange.Add(new EventDelegate(OnChange));
        backBtn.onClick.Add(new EventDelegate(OnClickBackBtn));
    }
    void OnClickBackBtn() {
        PlayEffectMusic_1();
        GUIManager.HideView("SettingsPanel");
        GUIManager.ShowView("CoverPanel");
        GUIManager.ShowView("LoginPanel");
    }
    private float curren_Value;
    private string curren_Value_String;
    private string curren_Text;
    private void OnChange() {          
        curren_Value = m_Slider.value;
        curren_Value_String = Mathf.Round(curren_Value*100).ToString() + "%";
        volumValue.text = curren_Value_String;
        curren_Text = volumValue.text;
        string add = AddString();
        curren_Value = System.Int32.Parse(add);
        AudioManager.Instance.BgVolume = curren_Value/100;
    }
    private string AddString() {
        char[] arry = curren_Text.ToCharArray();
        if (arry.Length==3)
        {
            string a = arry[0].ToString();
            string b = arry[1].ToString();
            string add = a + b;
            return add;
        }
        else if (arry.Length == 4)
        {
            string a = arry[0].ToString();
            string b = arry[1].ToString();
            string c = arry[2].ToString();
            string add = a + b + c;
            return add;
        }
        else {
           string a = arry[0].ToString();
           return a;
        }
    }
}
