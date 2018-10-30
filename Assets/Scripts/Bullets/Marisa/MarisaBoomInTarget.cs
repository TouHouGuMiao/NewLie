using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaBoomInTarget
{
    private GameObject bulletPrefab;
    private GameObject effectPrefab;
    public void Init()
    {
        bulletPrefab = ResourcesManager.Instance.LoadParticleBullet("StarBoomParticle_NoLock");
        effectPrefab = ResourcesManager.Instance.LoadEffect("explosionEffect");
    }

    public void ShowSkill(Vector3 showVec)
    {
        IEnumeratorManager.Instance.StartCoroutine(ShowSkillIEnumerator(showVec));
    }

    IEnumerator ShowSkillIEnumerator(Vector3 showVec)
    {
        GameObject effect = GameObject.Instantiate(effectPrefab);
        effect.transform.position = showVec;
        yield return new WaitForSeconds(0.5f);
        GameObject go = GameObject.Instantiate(bulletPrefab);
        go.transform.position = effect.transform.position;
    }
}
