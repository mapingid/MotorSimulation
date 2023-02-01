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
    public int CurrentPosition;
    public readonly string ID;
    public MotorMoveDoneEventArgs( string id, int currentPosition, int goalPosition )
    {
      GoalPosition = goalPosition;
      CurrentPosition = currentPosition;
      ID = id;
    }
  }
}
