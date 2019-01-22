namespace StateMachineLibrary
{
    public interface IEventSink
    {
        void Send(IEventSink source, ICommand command);

        void Receive(ICommand command);
    }

    public enum CommandTypes
    {
        InProgress,
        Paused,
        Completed,
        Error
    }

    public enum NPCCharacterTypes
    {
        Normal,
        Patrol,
        Watch,
        Wander
    }
}
