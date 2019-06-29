using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControllerBase : MonoBehaviour
{
    [SerializeField] protected ParticleSystem m_mainParticle = null;
    [Tooltip("the life time of all this effect")]
    [SerializeField] protected float m_effectDuration = 2.0f;
    [SerializeField] protected bool m_isLoop = false;

    #region events

    public System.Action OnStart = null;
    public System.Action OnEnd = null;
    public System.Action OnStop = null;

    #endregion

    protected virtual void Awake()
    {
        if (null == m_mainParticle)
        {
            m_mainParticle = GetComponentInChildren<ParticleSystem>();
            m_isLoop = m_mainParticle.main.loop;
        }
        // waitDurationToEnd = new WaitForSeconds(m_effectDuration);
    }

    [ContextMenu("play it test")]
    public virtual void PlayIt()
    {
        if (null != m_mainParticle)
        {
            m_mainParticle.Play();
            Debug.Log("effect play la");
        }
        OnStart?.Invoke();
    }
    // private WaitForSeconds waitDurationToEnd = null;
    protected IEnumerator CheckPlayEnd()
    {
        if (m_isLoop)
            yield break;
        yield return new WaitForSeconds(m_effectDuration);
        OnEnd?.Invoke();
    }

    public virtual void StopIt()
    {
        m_mainParticle.Stop();
        foreach (ParticleSystem patsys in m_mainParticle.GetComponentsInChildren<ParticleSystem>())
        {
            patsys.Stop();
        }
        OnStop?.Invoke();
        OnEnd?.Invoke();
    }

    // class end
}
