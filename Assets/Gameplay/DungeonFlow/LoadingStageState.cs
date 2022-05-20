
using UnityEngine;

namespace RLS.Gameplay.DungeonFlow
{
    public class LoadingStageState : DungeonGameState
    {
        public override void EnterState()
        {
            base.EnterState();
            Debug.LogError("ENTER LOADING");
        }

        public override void ExitState()
        {
            Debug.LogError("EXIT LOADING");
            base.ExitState();
        }
    }
}