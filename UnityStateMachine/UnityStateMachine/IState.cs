using System.Numerics;
using UnityStateMachine;

namespace StateMachineLibrary

{
    public interface IState

    {
        void EvaluateState(Transform transform);

        void SetAutomon(IAutomonInterface autonomon);

        CommandTypes StateProgress { get; }

        bool IsInterruptable { get; }

        void DoAction();

        //void Update(Transform transform);

        float TotalTime { get; }

        bool CanMoveBetweenWaypoints { get; }
        Vector2 OriginalPosition { get; set; }
        bool HasStarted { get; }
        void Start();
        void Pause(); // pause if continue later
        void Resume(); // resume - restart and continue state
        void End(); // clean up state
        bool CanCallEnd { get; }

    }

}
