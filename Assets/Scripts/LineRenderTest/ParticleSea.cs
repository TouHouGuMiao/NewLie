using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSea : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particlesArray;
    private ParticleSettings[] psetting;
    public int seaResolution = 25;
    public static float MaxRadius = 120f;
    public static float MinRadius = 50f;
    public float radius = 100.0f;
    public Gradient colorGradient; 

    private void Start()
    {
        particlesArray = new ParticleSystem.Particle[seaResolution * seaResolution];
        psetting = new global::ParticleSettings[seaResolution * seaResolution];
        particleSystem = this.gameObject.GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.maxParticles = seaResolution * seaResolution;
        particleSystem.Emit(seaResolution * seaResolution);
        particleSystem.GetParticles(particlesArray);
        SetInitialPosition();
    }

    private void Update()
    {
        RotateParticles();
        particleSystem.SetParticles(particlesArray, particlesArray.Length);
       
    }

    void SetInitialPosition()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                psetting[i * seaResolution + j] = new ParticleSettings(radius);
                particlesArray[i * seaResolution + j].position = psetting[i * seaResolution + j].GetPosition();
               
            }

        }
        particleSystem.SetParticles(particlesArray, particlesArray.Length);
    }

    void RotateParticles()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                psetting[i * seaResolution + j].Rotate();
                particlesArray[i * seaResolution + j].position = psetting[i * seaResolution + j].GetPosition();
            }
        }
    }
}

public class ParticleSettings
{
    public float angle { get; set; }
    public float radius { get; set; }
    public float speed { get; set; }
    public ParticleSettings(float r)
    {
        this.radius = r;
        this.angle = Random.value * 2 * Mathf.PI;
        this.speed = Random.value * Mathf.Sqrt(radius);
    }

    public Vector3 GetPosition()
    {
        return radius * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0);
    }

    public void Rotate()
    {
       
        this.angle += Time.deltaTime * speed / 10;

        if (this.angle > 2 * Mathf.PI)
        {
            this.angle -= 2 * Mathf.PI;
        }
        this.radius += Random.value * 0.2f - 0.1f;
        if(this.radius> ParticleSea.MaxRadius)
        {
            this.radius = ParticleSea.MaxRadius;
        }

        if (this.radius < ParticleSea.MinRadius)
        {
            this.radius = ParticleSea.MinRadius;
        }
    }

}
