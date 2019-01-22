namespace StateMachineLibrary

{
    public class Automaton : IAutomonInterface

    {

        public IEventSink State;

        public IEventSink Context;



        public static IAutomonInterface Create(IEventSink context, IEventSink state)

        {

            return new Automaton(context, state);

        }



        private Automaton(IEventSink context, IEventSink state)

        {

            this.State = state;

            this.Context = context;

        }



        public void Send(IEventSink source, ICommand command)

        {

            var target = source == this.State ? this.Context : this.State;



            target.Receive(command);

        }

    }

}
