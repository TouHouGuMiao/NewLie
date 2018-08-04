using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLDraw : MonoBehaviour
{

    public Texture m_Texture;
    public Shader m_Shader;
    public Material lineMaterial;
    private Vector2 frontPoint;
    private Vector2 backPoint;
    private Color PainterColor = Color.black;
    List<Vector3> allPoints=new List<Vector3> ();

  
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 tmpView = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            allPoints.Add(tmpView);
        }

        if (Input.GetMouseButtonUp(1))
        {
            bool isSphere=false;
            isSphere= DenpentSequ();
            Debug.LogError(isSphere);
            allPoints.Clear();
        }


    }


    public void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = m_Shader;
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
            //lineMaterial.SetTexture("_Texture", m_Texture);
        }
    }

    public void OnRenderObject()
    {
        CreateLineMaterial();
      
        GL.PushMatrix();
        lineMaterial.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        GL.Color(Color.black);
     

        for (int i = 0; i < allPoints.Count-1; i++)
        {
            frontPoint = allPoints[i];
            backPoint = allPoints[i + 1];
            GL.Vertex3(frontPoint.x, frontPoint.y, 0);
            GL.Vertex3(backPoint.x, backPoint.y, 0);
        }
      
        GL.End();
        GL.PopMatrix();
    }

    /// <summary>
    /// 判断用户是否在画圆
    /// </summary>
    private bool DenpentSequ()
    {
       
        if (allPoints.Count < 1)
            return false;

        float SEdistance = Vector2.Distance(allPoints[0], allPoints[allPoints.Count - 1]);
        float twoPointDistance = Vector2.Distance(allPoints[0], allPoints[1]);
        Debug.LogError(SEdistance);
        Debug.LogError(twoPointDistance);
        if (SEdistance >= twoPointDistance*5)
        {
            return false;
        }
         int centerIndex = 0;
        Vector3 startPoint = allPoints[0];
        centerIndex = (allPoints.Count - 1) / 2;
        Vector3 centerPoint = allPoints[centerIndex];
       

        Vector3 sphCenter = Vector3.zero;
        for (int i = 0; i < centerIndex; i++)
        {
            sphCenter += (allPoints[i] + allPoints[centerIndex + i]);
        }
        sphCenter = sphCenter / centerIndex;

        float r = 0;
        for (int i = 0; i < allPoints.Count; i++)
        {
            r += Vector2.Distance(allPoints[i], sphCenter);
        }
        r = r / allPoints.Count;
        for (int i = 0; i < allPoints.Count; i++)
        {
            float maxR = r + r * 0.2f;
            float minR = r - r * 0.2f;
            float distance = Vector2.Distance(sphCenter, allPoints[i]);
            if (distance < minR || distance > maxR)
            {
                return false;
            }
         }
        return true;
    }
}
