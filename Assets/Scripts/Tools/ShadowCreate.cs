using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCreate : MonoBehaviour {
    public float vector3_X;
    public float vector3_Y;
    public float vector3_Z;
    public float vector3_Rotate_X;
    public float vector3_Rotate_Y;
    public float vector3_Rotate_Z;
    
    private GameObject player;    
    void Start () {
        player = this.gameObject;       
        AddShadow();       
    }
	void Update () {

	}   
    void AddShadow() {
        AddPlayerShadow();
        AddStandOthersShadow();
    }
    void AddPlayerShadow() {
        if (player.tag == "Player") {
            GameObject shadow = GameObject.Instantiate(player);
            shadow.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 180);
            shadow.transform.SetParent(player.transform);
            shadow.GetComponent<SpriteRenderer>().sortingOrder = -1;
            shadow.transform.localPosition = new Vector3(vector3_X, vector3_Y, vector3_Z);
            shadow.transform.localRotation = Quaternion.Euler(vector3_Rotate_X, vector3_Rotate_Y, vector3_Rotate_Z);
            AddRigiBodyByTag();
            shadow.AddComponent<shadowScript>();
            shadow.GetComponent<ShadowCreate>().enabled = false;
        }
    }
    void AddStandOthersShadow() {
        if (player.tag == "SceneObject") {
            GameObject shadow = GameObject.Instantiate(player);
            shadow.transform.SetParent(player.transform);
            shadow.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 200);
            shadow.GetComponent<SpriteRenderer>().sortingOrder = -1;
            shadow.transform.localPosition = new Vector3(vector3_X, vector3_Y, vector3_Z);
            shadow.transform.localRotation = Quaternion.Euler(vector3_Rotate_X, vector3_Rotate_Y, vector3_Rotate_Z);
            shadow.GetComponent<ShadowCreate>().enabled = false;
        }
    }
    void AddRigiBodyByTag() {
        if (player.tag == "Player") {
            if (player.GetComponent<Rigidbody>() == null)
            {
                player.AddComponent<Rigidbody>();
                //player.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeRotationX;
                // player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
                //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                player.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
        }
    }
}
