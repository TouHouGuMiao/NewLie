using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMoPao : MonoBehaviour
{
    private ParticleSystem particleSys;
    private List<Texture> textureList = new List<Texture>();
    private Material material;
    private void Awake()
    {
        particleSys= transform.GetComponent<ParticleSystem>();
        Texture greedStar = ResourcesManager.Instance.LoadTexture("greedStar");
        textureList.Add(greedStar);
        Texture kingStar = ResourcesManager.Instance.LoadTexture("kingStar");
        textureList.Add(kingStar);
        Texture purpleStar = ResourcesManager.Instance.LoadTexture("purpleStar");
        textureList.Add(purpleStar);
        Texture redStar = ResourcesManager.Instance.LoadTexture("redStar");
        textureList.Add(redStar);

    }

    private void Start()
    {

        material = transform.GetComponent<Renderer>().material;

        ParticleSystem.Particle particle;
        

        StartCoroutine(ChangeMaterilTexture());
    }

    IEnumerator ChangeMaterilTexture()
    {
        material.mainTexture = textureList[(int)Random.Range(0, textureList.Count-0.1f)];
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ChangeMaterilTexture());
    }
     

}
