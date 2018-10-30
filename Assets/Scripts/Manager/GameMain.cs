using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{


	// Use this for initialization
	void Start () {
        AddSomeCompent();
        DoSmoeSetting();
	}
	
    void AddSomeCompent()
    {
        this.gameObject.AddComponent<DownLoadManager>();
        this.gameObject.AddComponent<GameStateManager>();
        this.gameObject.AddComponent<IEnumeratorManager>();
    }
    void DoSmoeSetting()
    {
        Application.runInBackground = true;
        CharacterPropManager.Instance.InitCharacterDic();
        Texture2D texture = ResourcesManager.Instance.LoadTexture2D("mouseUI");
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);

    }
        // Update is called once per frame
	void Update () {
        GUIManager.Update();
	}
}
