using StateMachineLibrary;

public class GoToWayPointState : BaseState
{
    public Transform WayPoint;
    private float timeSinceWaypoint = 0f;

    public GoToWayPointState(Vector2 originalPosition, Transform destVector, float elapsedTime) : base(originalPosition, elapsedTime)
    {
        WayPoint = destVector;

    }

    public override bool CanMoveBetweenWaypoints
    {
        get
        {
            return true;
        }
    }
    public override bool CanCallEnd
    {
        get
        {
            return false;
        }
    }
    public override void Start()
    {
        base.Start();
    }

    public override void End()
    {
        base.End();
    }

    public override void Pause()
    {
        base.Pause();
    }
    public override void Resume()
    {
        base.Resume();
        timeSinceWaypoint = 0f;
    }

    public override void Update(Transform transform)
    {
        if (CanMoveBetweenWaypoints && this.StateProgress != CommandTypes.Paused)
        {
            timeSinceWaypoint += Time.deltaTime;
            float tt = TotalTime;
            float result = (timeSinceWaypoint / tt);
            //float result = timeSinceWaypoint/5000f;
            transform.position = Vector2.Lerp(OriginalPosition, WayPoint.position, result);

            if (timeSinceWaypoint > TotalTime)
            {
                Debug.Log("Done!!!!");
                DoAction();
                //StateContext.PushState(new FleeState(10));
            }
            else
            {
                Debug.Log("Result = " + result.ToString()
                + "  timeSince = " + (timeSinceWaypoint).ToString()
                + "  tt/time = " + (tt / timeSinceWaypoint).ToString()
                );
            }
        }
    }
}
