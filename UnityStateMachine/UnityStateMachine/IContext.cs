using System.Numerics;

namespace StateMachineLibrary

{
    public interface IContext

    {

        void Start();

        void Stop();


        Vector2 CurrentPosition { get; set; }
        void PushState(IState newState);

        //void Update(Transform transform);

        IState CurrentState { get; }

        int StateCount { get; }

    }

}
