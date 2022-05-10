using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public enum EState
    {
        Ilde, 
        Run,
        Jump
    }

    private EState m_state;

    void Start()
    {
        switch (m_state)
        {
            case EState.Ilde:
                break;
            case EState.Run:
                break;
            case EState.Jump:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
