using System.Diagnostics;
using System.Numerics;
using StateMachineLibrary;
using UnityStateMachine;

public class FleeState : BaseState
{
    private float timeSinceFlee = 0f;
    public Vector2 WayPoint;

    public FleeState(Vector2 originalPosition, Vector2 destVector, float elapsedTime) : base(originalPosition, elapsedTime)
    {
        this.WayPoint = destVector;
        Start();
    }

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
            return true;
        }
    }
    public override void Update(Transform transform)
    {
        base.Update(transform);

        timeSinceFlee += Time.deltaTime;
        float tt = TotalTime;
        float result = (timeSinceFlee / tt);
        //float result = timeSinceWaypoint/5000f;
        transform.position = Vector2.Lerp(OriginalPosition, WayPoint, result);

        if (timeSinceFlee > TotalTime)
        {
            Debug.WriteLine("Done!!!!");
            DoAction();
            //StateContext.PushState(new FleeState(10));
        }
        else
        {
            Debug.WriteLine("Flee Result = " + result.ToString()
            + "  timeSince = " + (timeSinceFlee).ToString()
            + "  tt/time = " + (tt / timeSinceFlee).ToString()
            );
        }

    }
    public override void DoAction()
    {
        base.DoAction();


    }

}
