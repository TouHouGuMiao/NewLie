using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSysTest : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particlesArray;
    public int particleCouts=625;

    private void Start()
    {
        particlesArray = new ParticleSystem.Particle[particleCouts];
        particleSystem = this.gameObject.GetComponent<ParticleSystem>();
        particleSystem.GetParticles(particlesArray);
        
    }

    private void Update()
    {
        Debug.LogError(particlesArray[0].velocity);
    }
}
