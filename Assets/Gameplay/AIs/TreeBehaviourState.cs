using MOtter.StatesMachine;
using UnityEngine;

namespace RLS.Gameplay.AIs
{
    public class TreeBehaviourState : StateMonoBehaviour
    {
        protected bool m_isContinuousState = true;
        public bool IsContinuousState => m_isContinuousState;
    }
    public class TreeBehaviourState<T> : TreeBehaviourState where T : TreeBehaviour
    {
        [SerializeField]
        private T m_owner = null;
        public T Owner => m_owner;
    }
}
