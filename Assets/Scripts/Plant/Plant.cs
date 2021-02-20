using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Plant : MonoBehaviour
{
    [SerializeField]
    private float m_nSeedToSaplingTime = 5;
    [SerializeField]
    private float m_nSaplingToMatureTime = 5;

    private float m_nAnimTimer;
    private float m_nCurrWaitTime;
    private bool m_bDoTimer;
    private Animator m_pAnimator;
    private ParticleSystem m_pParticles;

    public enum PlantState { seed, sapling, mature };

    private PlantState m_pState; 

    public void Awake()
    {
        m_pState        = PlantState.seed;
        m_pAnimator     = GetComponent<Animator>();
        m_pParticles    = transform.Find("GrowParticles").GetComponent<ParticleSystem>();
    }

    public PlantState getState()
    {
        return m_pState;
    }

    public void setSeeded()
    {
        if(m_pState > PlantState.seed)
            return;

        m_pState = PlantState.seed;
        animateToNextState();
    }

    public void Update()
    {
        if (!m_bDoTimer)
            return;

        updateTimer();
    }

    private void updateTimer()
    {
        m_nAnimTimer += Time.deltaTime;

        if (m_nAnimTimer < m_nCurrWaitTime)
            return;

        m_bDoTimer = false;

        if (m_pState == PlantState.seed)
        {
            m_pAnimator.SetTrigger("setSeedToSapling");
            animateToNextState();
            m_pState = PlantState.sapling;
        }
        else if(m_pState == PlantState.sapling)
        {
            m_pAnimator.SetTrigger("setSaplingToMature");
            animateToNextState();
            m_pState = PlantState.mature;
        }
    }

    private void animateToNextState()
    {
        m_nAnimTimer  = 0;
        m_bDoTimer         = true;

        if (m_pState == PlantState.seed)
            m_nCurrWaitTime = m_nSeedToSaplingTime;
        if (m_pState == PlantState.sapling)
            m_nCurrWaitTime = m_nSaplingToMatureTime;
    }

    private void onPlantFullyGrown()
    {
        m_pParticles.Play();
    }

}
