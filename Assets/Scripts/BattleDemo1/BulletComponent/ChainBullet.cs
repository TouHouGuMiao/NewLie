using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBullet : MonoBehaviour {

    private GameObject effect;
    public bool useChain=false;
	// Use this for initialization
	void Start () {
        effect = ResourcesManager.Instance.LoadEffect("chainBoomEffect");

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    

    public void ChainBomb(GameObject go, Transform point, int layer)//爆炸函数
    {
        StartCoroutine(ChainBombIEnumerator(go, point, layer));
    }

    private IEnumerator ChainBombIEnumerator(GameObject go, Transform point, int layer)
    {
        Collider[] tempList = Physics.OverlapSphere(point.position, 0.75f, 1 << layer);//获取所有碰撞体
        Destroy(point.gameObject, 1.0f);
        GameObject effectGo = GameObject.Instantiate(effect);
        effectGo.transform.SetParent(point, false);
        point.transform.GetComponent<BulletBaseComponent>().deadByChainBoom = true;
        effectGo.transform.localPosition = Vector3.zero;
        effectGo.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        List<Collider> others = new List<Collider>();
        for (int i = 0; i < tempList.Length; i++)
        {
            if (tempList[i].gameObject == null)
            {
                continue;
            }
            BulletBaseComponent tempBBC = tempList[i].transform.GetComponent<BulletBaseComponent>();

            if (tempBBC != null)
            {
                others.Add(tempBBC.GetComponent<Collider>());
            }
        }
        BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
        if (useChain)
        {
            for (int i = 0; i < others.Count; i++)
            {
                BulletBaseComponent otherBBC = others[i].GetComponent<BulletBaseComponent>();
                if (otherBBC.isDead)
                {
                    continue;
                }
                //other.AddExplosionForce(30000, point.position, 2, 10);//这个函数会自动根据距离给刚体衰减的力
                bool isDead = otherBBC.ComputerPower(bbc.power);
                if (isDead)
                {
                    ChainBomb(go, otherBBC.transform, layer);
                }
            }
        }
    }
}
