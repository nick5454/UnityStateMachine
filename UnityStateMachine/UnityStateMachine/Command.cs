using UnityStateMachine;

namespace StateMachineLibrary

{
    public class Command : ICommand

    {

        public Command(Transform transform, CommandTypes command)

        {

            this.CommandDirective = command;
            this.transform = transform;
        }

        public CommandTypes CommandDirective { get; private set; }
        public Transform transform { get; set; }
    }

}
