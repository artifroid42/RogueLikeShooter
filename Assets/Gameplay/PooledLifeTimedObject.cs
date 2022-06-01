using UnityEngine;

public class PooledLifeTimedObject : MonoBehaviour
{
    [SerializeField]
    private float m_lifeTime = 5f;
    private float m_timeOfStart;

    private void OnEnable()
    {
        m_timeOfStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActiveAndEnabled && Time.time - m_timeOfStart > m_lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
}
