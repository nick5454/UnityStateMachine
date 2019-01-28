using System;
using System.Numerics;
using StateMachineLibrary;
using Xunit;

namespace UnityStateMachine.Tests
{
    public class UnitTest1
    {
        private BaseContext _context;

        public UnitTest1()
        {
            _context = new BaseContext();

        }

        [Fact]
        public void Test1()
        {
            Vector2 position = new Vector2(0, 0);
            Vector2[] waypoints = new Vector2[] 
            { 
                new Vector2(10,10)
            };
            _context.PushState(new GoToWayPointState(position, waypoints[0], 2));
            _context.Start();

            bool done = false;

            float tt = 0f;
            while (!done )
            {
                tt += .05f;

                if (tt > .05f)
                {
                    var fleeState = new FleeState(_context.CurrentState.CurrentPosition, new Vector2(10, 10), 2f);
                }

               // Send the current position
               // Do not haves access to physics - use hook
               // End and pause does not update position
                _context.Update(new Transform() { position = waypoints[0] });
                done = false;
            }

        }
    }
}
