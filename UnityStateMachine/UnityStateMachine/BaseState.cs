using System;
using System.Numerics;
using UnityStateMachine;

namespace StateMachineLibrary

{


    public delegate void StateProgressEventHandler(object sender, StateProgressEventArgs e);

    public abstract class BaseState : IEventSink, IState

    {

        //protected bool _isPaused = false;

        protected CommandTypes _progress;

        private float _timeToDecision = 1f;
        private float _timeSinceDecision = 0f;

        private IAutomonInterface _automaton;

        //public event EventHandler ReadyForDecision;
        //public event EventHandler UpdateCalled;
        //public event StateProgressEventHandler StateProgressChanged;

        public float TotalTime { get { return _timeToDecision; } }

        public abstract bool CanMoveBetweenWaypoints { get; }
        public Vector2 OriginalPosition { get; set; }
        //protected void OnReadyForDecision(EventArgs e)
        //{
        //    if (ReadyForDecision != null) ReadyForDecision(this, e);
        //}


        public abstract bool CanCallEnd
        {
            get;
        }

        public virtual void Pause()
        { // pause if continue later
            _progress = CommandTypes.Paused;

        }
        public virtual void Resume()
        { // resume - restart and continue state


            _progress = CommandTypes.InProgress;
        }
        public virtual void End()
        { // clean up state

        }
        //protected void OnStateProgressChangedEvent(StateProgressEventArgs e)
        //{
        //    if (StateProgressChanged != null)
        //    {
        //        StateProgressChanged(this, e);
        //    }
        //}

        //protected void OnUpdate(EventArgs e)
        //{
        //    if (this.UpdateCalled != null) UpdateCalled(this, e);
        //}

        public void EvaluateState(Transform transform)
        {
            switch (this._progress)
            {
                case CommandTypes.InProgress:
                    Update(transform);
                    break;
                case CommandTypes.Paused:
                    break;
                case CommandTypes.Completed:
                    break;
                case CommandTypes.Error:
                    break;
                default:
                    throw new ApplicationException("Invalid State!" + this._progress.ToString());
                    break;
            }
        }
        public bool HasStarted { get; private set; }

        public BaseState(Vector2 originalPosition, float elapsedTimeForDescision)
        {
            HasStarted = false;
            this.OriginalPosition = originalPosition;
            //_automaton = automaton;
            _timeToDecision = elapsedTimeForDescision;
        }

        public float DecisionTime { get { return _timeToDecision; } }

        public CommandTypes StateProgress { get { return _progress; } private set { _progress = value; } }

        public void SetAutomon(IAutomonInterface autonomon)
        {
            _automaton = autonomon;
        }

        public virtual bool IsInterruptable { get { return true; } }

        public virtual void DoAction(Transform transform)
        {
            // do nothing
            Completed(transform);
        }

        public virtual void Update(Transform transform)
        {


            switch (_progress)
            {
                case CommandTypes.InProgress:

                    _timeSinceDecision += System.Environment.TickCount;
                    //OnUpdate(EventArgs.Empty);

                    if (_timeSinceDecision > _timeToDecision)
                    {
                        MakeDecision();
                    }
                    break;
                case CommandTypes.Paused:
                    OriginalPosition = transform.position;
                    break;
            }
        }

        // events
        protected virtual void MakeDecision()
        {
            //OnReadyForDecision(EventArgs.Empty);
        }

        protected void Completed(Transform transform)
        {
            if (CanCallEnd)
            {
                End();
            }
            else
            {
                Pause();
            }
            _automaton.Send(this, new Command(transform, CommandTypes.Completed));
            //OnStateProgressChanged(CommandTypes.Completed);
        }

        public virtual void Start()
        {
            this.StateProgress = CommandTypes.InProgress;
            HasStarted = true;
        }

        //private void OnStateProgressChanged(CommandTypes newType)
        //{
        //    if (this.StateProgress != newType)
        //    {
        //        this._progress = newType;

        //        this.Send(this, new Command(newType));
        //    }
        //}

        public void Send(IEventSink source, ICommand command)
        {
            _automaton.Send(source, command);
        }

        public void Receive(ICommand command)

        {

            // do nothing

            //switch(command.command)

            //{

            //     case CommandTypes.Start:



            //}

        }

    }

}
