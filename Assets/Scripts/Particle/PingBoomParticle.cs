using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingBoomParticle : MonoBehaviour
{
    private ParticleSystem paritcleSystem;
    private ParticleSystem.Particle[] particleArray;


    private void Awake()
    {
        paritcleSystem = this.gameObject.GetComponent<ParticleSystem>();
        particleArray = new ParticleSystem.Particle[paritcleSystem.main.maxParticles];
    }

    private void Start()
    {
        paritcleSystem.GetParticles(particleArray);
        for (int i = 0; i < particleArray.Length; i++)
        {

            if (particleArray[i].position.z != 0 || particleArray[i].velocity.z != 0)
            {
                particleArray[i].position = new Vector3(particleArray[i].position.x, particleArray[i].position.y, 0);
                particleArray[i].velocity = new Vector3(particleArray[i].velocity.x, particleArray[i].velocity.y, 0);
            }
        }
        paritcleSystem.SetParticles(particleArray, particleArray.Length);
    }
 
}
