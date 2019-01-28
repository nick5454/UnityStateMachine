using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityStateMachine;

namespace StateMachineLibrary
{
    public class BaseContext : IEventSink, IContext
    {
        //public NPCCharacterTypes CharacterTypes { get; set; }

        private IAutomonInterface Communicator { get; set; }

        private IState state;
        public event StateProgressEventHandler StateProgressChanged;
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

        protected void OnStateProgressChangedEvent(StateProgressEventArgs e)
        {
            if (StateProgressChanged != null)
            {
                StateProgressChanged(this, e);
            }
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
                        //Communicator.Send(this, new Command(null))
                    }
                    else
                    {
                        state.Pause();
                    }
                }

                state = null;
                state = newState;

                state.OriginalPosition = CurrentPosition;
                Debug.WriteLine("Orig " + state.OriginalPosition);
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

        public virtual void Update(Transform transform)
        {
            //state.Update(transform);
            if(Communicator != null)
            {
                Send(this, new Command(transform, CommandTypes.InProgress));
            }
        }

        public void Send(IEventSink source, ICommand command)
        {
            Communicator.Send(source, command);
        }

        public void Receive(ICommand command)
        {
            OnStateProgressChangedEvent(new StateProgressEventArgs(command));
            switch (command.CommandDirective)
            {
                case CommandTypes.InProgress:
                    break;
                case CommandTypes.Completed:
                    this.PopState();
                    break;
                case CommandTypes.Error:
                    break;
                default:
                    throw new NotImplementedException("Command not supported " + command.CommandDirective.ToString());
                    break;
            }
        }
    }

}
