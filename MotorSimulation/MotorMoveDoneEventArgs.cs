using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  class MotorMoveDoneEventArgs : EventArgs
  {
    public readonly int GoalPosition;
    public readonly int CurrentPosition;
    public MotorMoveDoneEventArgs( int currentPosition, int goalPosition )
    {
      GoalPosition = goalPosition;
      CurrentPosition = currentPosition;
    }
  }
}
