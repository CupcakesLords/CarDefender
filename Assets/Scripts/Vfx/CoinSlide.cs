using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CoinSlide : MonoBehaviour
{
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    public float m_Drift = 0.01f;

    private void LateUpdate()
    {
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            if (timerCount < timer / 2)
            {
                m_Particles[i].position = Vector3.MoveTowards(m_Particles[i].position, new Vector3(x, y, 0), Time.deltaTime * 30f);
            }
        }

        // Apply the particle changes to the Particle System
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
    }

    bool played;
    float timer;
    float timerCount;
    int slot_i; int slot_j;
    float x; float y;
    
    private void Awake()
    {
        played = false;
        timer = GetComponent<ParticleSystem>().main.duration;
        timerCount = timer;
    }

    void Start()
    {

    }

    void Update()
    {
        if (played)
        {
            if (GetComponent<ParticleSystem>().isPlaying)
            {
                timerCount -= Time.deltaTime;
                return;
            }
            else
            {
                AddToPool();
                played = false;
                timerCount = timer;
            }
        }
    }

    public void Play(int i, int j)
    {
        slot_i = i; slot_j = j;
        float temp;
        if (slot_i == 0)
            temp = 1.5f;
        else if (slot_i == 1)
            temp = 0.5f;
        else if (slot_i == 2)
            temp = -0.5f;
        else if (slot_i == 3)
            temp = -1.5f;
        else
            temp = 0;
        x = temp;
        y = 4 + (3 - slot_j);
        played = true;
        GetComponent<ParticleSystem>().Play();
    }

    void AddToPool()
    {
        CoinPopUp.AddToPool(gameObject);
    }
}
