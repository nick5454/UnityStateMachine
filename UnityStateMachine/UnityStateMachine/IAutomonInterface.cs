namespace StateMachineLibrary

{
    public interface IAutomonInterface

    {

        void Send(IEventSink source,/* IEventSink target,*/ ICommand command);

    }

}
