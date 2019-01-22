using System.Numerics;
using StateMachineLibrary;

public class PatrolState : BaseState
{


    public PatrolState(Vector2 originalPosition, float elapsedTimeForDecision) : base(originalPosition, elapsedTimeForDecision) { }


    public override bool CanMoveBetweenWaypoints
    {
        get
        {
            return false;
        }
    }

    public override bool CanCallEnd
    {
        get
        {
            return false;
        }
    }
}
