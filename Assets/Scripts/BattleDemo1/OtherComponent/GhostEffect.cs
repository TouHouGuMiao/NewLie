
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;

#endif

[RequireComponent(typeof(SpriteRenderer))]
public class GhostEffect : MonoBehaviour
{
    [Range(2, 200)]
    public int trailSize = 20;

    [Range(0, 20)]
    public int spacing = 0;

    public Color color = new Color(255, 255, 255, 100);

    [ContextMenuItem("Clear List", "DeleteMaterials")]
    public List<Material> ghostMaterial;

    public int ghostSpriteSortingOrder;

    public bool renderOnMotion;

    public bool colorAlphaOverride;

    [Range(0, 1)]
    public float alphaFluctuationOverride;

    [Range(0, 250)]
    public int alphaFluctuationDivisor;

    //==========

    private int spacingCounter = 0;
    private SpriteRenderer character;
    private List<Sprite> spriteList = new List<Sprite>();
    private List<Vector3> spritePositions = new List<Vector3>();
    private List<GameObject> ghostList = new List<GameObject>();
    private bool hasRigidBody2D;
    private int zAnchor;
    private float alpha;
    private bool killSwitch;

    void Start()
    {
        if (gameObject != null)
        {
            character = GetComponent<SpriteRenderer>();
        }

        ghostMaterial = TruncateMaterialList(ghostMaterial);

        if (ghostMaterial.Count == 0)
        {
            ghostMaterial.Add(character.GetComponent<SpriteRenderer>().material);
        }

        Vector3 position = transform.position;

        for (int i = 0; i < trailSize; i++)
            Populate(position, true);

        alpha = color.a;

        if (spacing < 0)
            spacing = 0;


        zAnchor = transform.position.z > Camera.main.transform.position.z ? 1 : -1;

        hasRigidBody2D = GetComponent<Rigidbody2D>() != null ? true : false;

        ghostMaterial.Reverse();
    }

    public void ClearTrail()
    {
        trailSize = 2;
        foreach (Sprite s in spriteList)
        {
            Destroy(s);
        }

        spriteList.Clear();
        spritePositions.Clear();
    }

    private void KillSwitchEngage()
    {
        killSwitch = true;
        foreach (GameObject g in ghostList)
        {
            Destroy(g);
        }
    }

    void OnDestroy()
    {
        killSwitch = true;
        KillSwitchEngage();
    }

    public void AddToMaterialList(Material material)
    {
        ghostMaterial.Add(material);
    }

#if UNITY_EDITOR
    public void RestoreDefaults()
    {
        Undo.RecordObject(gameObject.GetComponent<GhostEffect>(), "Restore Defaults");
        ghostMaterial.Clear();
        trailSize = 20;
        color = new Color(255, 255, 255, 50);
        spacing = 0;
        ghostSpriteSortingOrder = 0;
        renderOnMotion = false;
        colorAlphaOverride = false;
        alphaFluctuationOverride = 0;
        alphaFluctuationDivisor = 1;
    }
#endif

    void Update()
    {
        if (killSwitch)
        {
            return;
        }

        if (ghostMaterial.Count == 0)
            ghostMaterial.Add(GetComponent<SpriteRenderer>().material);

        if (trailSize < ghostList.Count)
        {
            for (int i = trailSize; i < ghostList.Count - 1; i++)
            {
                GameObject gone = ghostList[i];
                Destroy(gone);
                ghostList.RemoveAt(i);
            }
        }

        zAnchor = this.gameObject.transform.position.z > Camera.main.transform.position.z ? 1 : -1;
        if (spacingCounter < spacing)
        {
            spacingCounter++;
            return;
        }

        Vector3 position = gameObject.transform.position;

        if (ghostList.Count < trailSize)
        {
            Populate(position, false);
        }
        else
        {
            GameObject gone = ghostList[0];
            ghostList.RemoveAt(0);
            GameObject.Destroy(gone);
            Populate(position, false);
        }

        float temp;
        if (colorAlphaOverride)
            temp = alphaFluctuationOverride;
        else
        {
            temp = alpha;
        }

        int materialDivisor = (ghostList.Count - 1) / ghostMaterial.Count + 1;
        for (int i = ghostList.Count - 1; i >= 0; i--)
        {
            temp -= colorAlphaOverride && alphaFluctuationDivisor != 0 ? alphaFluctuationOverride / alphaFluctuationDivisor : alpha / ghostList.Count;
            color.a = temp;
            SpriteRenderer sprite = ghostList[i].GetComponent<SpriteRenderer>();
            sprite.color = color;
            int subMat = (int)Mathf.Floor(i / materialDivisor);
            sprite.material = subMat <= 0 ? ghostMaterial[0] : ghostMaterial[subMat];
            ghostList[i].transform.position = new Vector3(ghostList[i].transform.position.x,
                ghostList[i].transform.position.y,
                 ghostList[i].transform.position.z);
        }

        spacingCounter = 0;
    }

    private void Populate(Vector3 position, bool allowPositionOverride)
    {
        if (RenderOnMotion
            && hasRigidBody2D
            && gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero
            && !allowPositionOverride)
        {
            if (ghostList.Count > 0)
            {
                GameObject gone = ghostList[0];
                ghostList.RemoveAt(0);
                Destroy(gone);
            }

            return;
        }

        GameObject g = new GameObject();
        g.transform.SetParent(GameObject.Find("Fishes").transform,false);
        g.name = gameObject.name + " - GhostSprite";
        g.AddComponent<SpriteRenderer>();
        g.transform.position = position;
        g.transform.localScale = gameObject.transform.localScale;
        g.transform.rotation = gameObject.transform.rotation;
        g.GetComponent<SpriteRenderer>().sprite = character.sprite;
        g.GetComponent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder-1;
        ghostList.Add(g);
    }

    private List<Material> TruncateMaterialList(List<Material> materialList)
    {
        List<Material> tempList = new List<Material>();
        foreach (Material material in materialList)
        {
            if (material)
                tempList.Add(material);
        }

        return tempList;
    }

    private void DeleteMaterials()
    {
        ghostMaterial.Clear();
    }

    //==========

    public int ZAnchor
    {
        get { return zAnchor; }
        set
        {
            if (value == 1 || value == -1)
                zAnchor = value;
        }
    }

    public int GhostSpriteSortingOrder
    {
        get { return ghostSpriteSortingOrder; }
        set { ghostSpriteSortingOrder = value; }
    }

    public bool RenderOnMotion
    {
        get { return renderOnMotion; }
        set { renderOnMotion = value; }
    }

    public int TrailSize
    {
        get { return trailSize; }
        set
        {
            if (value >= 2)
                trailSize = value;
        }
    }

    public int Spacing
    {
        get { return spacing; }
        set
        {
            if (value >= 0 && value <= 20)
                spacing = value;
        }
    }

}