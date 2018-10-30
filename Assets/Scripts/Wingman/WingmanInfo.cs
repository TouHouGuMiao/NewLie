using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingmanInfo : MonoBehaviour {

    private GameObject bulletPrefab;
    private Transform player;


    public float tempTime;
    

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(AttackTaeget());
    }

    void Update () {
        this.transform.Rotate(new Vector3(0, 0, 1) * 180 * Time.deltaTime) ;
	}




    //加载僚机所发射的子弹
    public void SetBullet(string name)
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet(name);
    }

    

    
    IEnumerator AttackTaeget()
    {
        if (this.enabled == false)
        {
            yield break;
        }
        yield return new WaitForSeconds(tempTime);
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 10, 1 << LayerMask.NameToLayer("enemy"));
        if (colliders.Length>0)
        {       
            GameObject go = GameObject.Instantiate(bulletPrefab);
            go.transform.position = this.transform.position;
        }
        StartCoroutine(AttackTaeget());
    }
}
