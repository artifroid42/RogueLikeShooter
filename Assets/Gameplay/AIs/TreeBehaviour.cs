using MOtter.StatesMachine;

namespace RLS.Gameplay.AIs
{
    public class TreeBehaviour : StateMachineMonoBehaviour
    {
        protected virtual void ProcessChoice()
        {

        }

        public override void DoUpdate()
        {
            base.DoUpdate();
            ProcessChoice();
        }

        protected void RequestState(TreeBehaviourState a_state)
        {
            if(a_state.IsContinuousState && a_state == m_currentState)
            {
                return;
            }
            SwitchToState(a_state);
        }
    }
}