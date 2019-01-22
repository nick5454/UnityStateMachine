using UnityStateMachine;

namespace StateMachineLibrary
{
    public interface ICommand
    {
        CommandTypes CommandDirective { get; }
        Transform transform { get; set; }

    }
}
