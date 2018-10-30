using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleReturnNoInstate : MonoBehaviour {

    private ParticleSystem paritcleSystem;
    private ParticleSystem.Particle[] particleArray;

    private GameObject bulletPrefab;
    private void Awake()
    {
        paritcleSystem = this.gameObject.GetComponent<ParticleSystem>();
        particleArray = new ParticleSystem.Particle[paritcleSystem.main.maxParticles];
        bulletPrefab = ResourcesManager.Instance.LoadBullet("WeiBaLockBullet");

    }

    private void Start()
    {

        paritcleSystem.GetParticles(particleArray);
        for (int i = 0; i < particleArray.Length; i++)
        {

            if (particleArray[i].position.z != 0 || particleArray[i].velocity.z != 0)
            {
                particleArray[i].position = new Vector3(particleArray[i].velocity.x, particleArray[i].velocity.y, 0);
                particleArray[i].velocity = new Vector3(particleArray[i].velocity.x, particleArray[i].velocity.y, 0);
            }
        }
        paritcleSystem.SetParticles(particleArray, particleArray.Length);
        StartCoroutine(TimeDepent());
    }

    private void Update()
    {
     
        if (time >= 1.0f)
        {
            Destroy(gameObject);
        }
        if (time >= 0.5f)
        {
            if (isReturn == true)
                return;
            paritcleSystem.GetParticles(particleArray);
            for (int i = 0; i < particleArray.Length; i++)
            {
                particleArray[i].velocity = new Vector3(-particleArray[i].velocity.x, -particleArray[i].velocity.y, 0);

            }
            paritcleSystem.SetParticles(particleArray, particleArray.Length);
            isReturn = true;
        }

    }

    bool isReturn = false;

    float time = 0;
    IEnumerator TimeDepent()
    {

        yield return new WaitForSeconds(0.1f);
        time += 0.1f;
        StartCoroutine(TimeDepent());
    }

}
