namespace StateMachineLibrary

{
    public interface IEventSink

    {



        void Send(IEventSink source, ICommand command);

        void Receive(ICommand command);

    }

}
