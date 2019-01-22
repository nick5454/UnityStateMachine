using System;
using System.Collections.Generic;

namespace StateMachineLibrary

{
    public class BaseContext : IEventSink, IContext

    {

        public NPCCharacterTypes CharacterTypes { get; set; }

        private IAutomonInterface Communicator { get; set; }

        private IState state;

        private Stack<IState> _states = new Stack<IState>();
        public Vector2 CurrentPosition { get; set; }
        public IState CurrentState

        {

            get

            {

                return state;

            }

        }



        public int StateCount { get { return _states.Count; } }

        public void Start()

        {

            if (_states.Count < 1)

            {

                throw new ArgumentOutOfRangeException("Trying to start a State Machine without an initial State.");

            }







            this.PopState();

        }



        public void Stop()

        {



        }



        public void PushState(IState newState)
        {
            _states.Push(newState);

            if (state == null) return; // if null then we havent started

            switch (state.StateProgress)
            {
                case CommandTypes.Completed:
                    this.PopState();
                    break;

                case CommandTypes.InProgress:
                    if (state.IsInterruptable) this.PopState();
                    break;
            }
        }

        private void PopState()
        {
            if (_states.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Trying to pop a state when there is not an available state to go to.");
            }


            SetState(_states.Count == 1 ? _states.Peek() : _states.Pop());
        }

        private void SetState(IState newState)
        {
            if (state == null ||
                  state.StateProgress == CommandTypes.Completed ||
                  (state.StateProgress == CommandTypes.Paused || state.StateProgress == CommandTypes.InProgress &&
                  state.IsInterruptable))
            {
                Communicator = null;
                if (state != null)
                {
                    if (state.CanCallEnd)
                    {
                        state.End();
                    }
                    else
                    {
                        state.Pause();
                    }
                }

                state = null;
                state = newState;

                state.OriginalPosition = CurrentPosition;
                Debug.Log("Orig " + state.OriginalPosition);
                if (state.HasStarted)
                {
                    state.Resume();
                }
                else
                {
                    state.Start();
                }

                Communicator = Automaton.Create(this, (IEventSink)state);

                state.SetAutomon(Communicator);

            }

        }

        //public virtual void Update(Transform transform)
        //{
        //    state.Update(transform);
        //}

        void IEventSink.Send(IEventSink source, ICommand command)
        {
            throw new NotImplementedException();
        }



        void IEventSink.Receive(ICommand command)

        {

            switch (command.command)

            {

                case CommandTypes.InProgress:

                    break;

                case CommandTypes.Completed:

                    this.PopState();

                    break;

                case CommandTypes.Error:

                    break;

                default:

                    throw new NotImplementedException("Command not supported " + command.command.ToString());

                    break;

            }

        }





    }

}
