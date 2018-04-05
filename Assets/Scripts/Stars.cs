using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour 
{
    private ParticleSystem m_particleSystem;

    private ParticleSystem.Particle[] m_particleList;

    public int m_maxParticles;

    public int x_min, x_max, y_min, y_max, z_min, z_max;

    private List<Vector3> m_positions = new List<Vector3>();

    void OnEnable()
    {
        //getting particle system
        m_particleSystem = GetComponent<ParticleSystem>();

        for (int i = 0; i < m_maxParticles; i++)
        {
            m_positions.Add(new Vector3(Random.Range(x_min, x_max), Random.Range(y_min, y_max), Random.Range(z_min, z_max)));
        }

        //managing number of particles according to the number of positions
        NumberOfParticles(m_positions.Count);

        //following will go through each position and will place a particle on each position
        StartCoroutine(ParticlesPositionController(2f));    
    }

    void NumberOfParticles(int a_maxParticles)
    {
        //following will ensure that there is an amount to which particle should spawn
        var main = m_particleSystem.main;

        main.maxParticles = a_maxParticles;

        var em = m_particleSystem.emission;

        // this will ensure all the particles get spawn at the same time
        em.rateOverTime = a_maxParticles;
    }

    //following method will be called once in the level which will manage all the particles according to the position provided by the user
    IEnumerator ParticlesPositionController(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        m_particleList = new ParticleSystem.Particle[m_particleSystem.particleCount];

        //getting all the particles in to the list of particles
        m_particleSystem.GetParticles(m_particleList);

        //following loop will place each particle to the position provide by the user
        for (int i = 0; i < m_particleSystem.particleCount; i++)
        {
            //moving the particle to a certain position
            m_particleList[i].position = m_positions[i];
        }

        //setting each particle in the particle system according to the particles stored in the list
        m_particleSystem.SetParticles(m_particleList, m_particleSystem.particleCount);
    }
}