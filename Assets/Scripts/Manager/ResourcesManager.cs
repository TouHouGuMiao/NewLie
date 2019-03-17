using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    private static ResourcesManager _instance=null;
    public static ResourcesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourcesManager();
            }
            return _instance;
        }
    }

    private string path = "UI/Panel";

    public GameObject LoadPanel(string name)
    {
      return LoadPrefab(name,path);
    }

    public Texture2D LoadTexture2D(string name)
    {
        string path = "Texture" + "/" + name;
        Texture2D texture = Resources.Load(path, typeof(Texture2D)) as Texture2D;
        if (texture == null)
        {
            Debug.LogError("texture is null");
            return null;
        }
        return texture;
    }


    public Texture2D LoadCG(string name)
    {
        string path = "CG" + "/" + name;
        Texture2D texture = Resources.Load(path, typeof(Texture2D)) as Texture2D;
        if (texture == null)
        {
            Debug.LogError("texture is null");
            return null;
        }
        return texture;
    }



    private string bulletPath = "BulletPrefab";
    public GameObject LoadBullet(string name)
    {
        GameObject go = LoadPrefab(name, bulletPath);
        return go;
    }

    private GameObject LoadPrefab(string name,string path)
    {
        string m_path = path + "/" + name;
        GameObject go = Resources.Load(m_path, typeof(GameObject)) as GameObject;
        if (go == null)
        {
            Debug.LogError(m_path + "is null");
            return null;
        }
        return go;
    }

    string atlasPath = "Atlas";


    
   public UIAtlas LoadAtlas(string name)
    {
        GameObject atlasGO = LoadPrefab(name, atlasPath);
        if (atlasGO == null)
        {
            Debug.LogError("atlas is null" + name);
            return null;
        }
        UIAtlas m_Atlas = atlasGO.GetComponent<UIAtlas>();
        return m_Atlas;
    }

   public GameObject LoadHeroModel(string name)
    {
        GameObject heroGo = LoadPrefab(name, "HeroPrefab");
        if (heroGo == null)
        {
            Debug.LogError("HeroPrefab is null");
            return null;
        }
        return heroGo;
    }


    public AudioClip LoadAudioClip(string name)
    {
        string path = "Audio" + "/" + name;
        AudioClip clip = Resources.Load(path, typeof(AudioClip)) as AudioClip;

        if (clip == null)
        {
            Debug.LogError("audioClip is null");
            return null;
        }
        return clip;
    }


    public GameObject LoadEffect(string name)
    {
        string path = "Effect" + "/" + name;

        GameObject effect = Resources.Load(path) as GameObject;
        if (effect == null)
        {
            Debug.LogError("effect is null");
            return null;
        }
        return effect;
    }

    public GameObject LoadDiceCard(string name)
    {
        string path = "Model" + "/" + "DiceCard" + "/"+ name;

        GameObject effect = Resources.Load(path) as GameObject;
        if (effect == null)
        {
            Debug.LogError("diceModel is null");
            return null;
        }
        return effect;
    }

    public Sprite LoadSpriteBullet(string name,int width=128,int heigt=128)
    {
        string path = "BulletSprite" + "/" + name;

        Texture2D texture = Resources.Load(path) as Texture2D;

        Sprite sprite = Sprite.Create(texture, new Rect(0,0, width, heigt),new Vector2 (0.5f,0.5f));
        if (sprite == null)
        {
            Debug.LogError("sprite is null"+name);
            return null;
        }
        return sprite;
    }

    public Sprite LoadWingmanSprite(string name,int width = 128,int heigt= 128)
    {
        string path = "wingmanSprite" + "/" + name;

        Texture2D texture = Resources.Load(path) as Texture2D;

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, width, heigt), new Vector2(0.5f, 0.5f));
        if (sprite == null)
        {
            Debug.LogError("sprite is null" + name);
            return null;
        }
        return sprite;
    }

    public Texture LoadTexture(string name)
    {
        string path = "BulletSprite" + "/" + name;

        Texture2D texture = Resources.Load(path) as Texture2D;


        if (texture == null)
        {
            Debug.LogError("texture is null" + name);
            return null;
        }
        return texture;
    }
    public GameObject LoadParticleBullet(string name)
    {
        string path = "ParitcleBullet" + "/" + name;

        GameObject particle = Resources.Load(path) as GameObject;
        if (particle == null)
        {
            Debug.LogError("particle is null");
            return null;
        }
        return particle;
    }


    public TextAsset LoadConfig(string path,string name)
    {
        TextAsset textAsset = null;
        string loadPath = path + "/" + name;
        textAsset = Resources.Load(loadPath, typeof(TextAsset)) as  TextAsset;

        if (textAsset == null)
        {
            Debug.LogError(loadPath + " ..." + "not found config by the path");
            return null;
        }
        return textAsset;

    }
}
