using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  class MotorMoveEventArgs : EventArgs
  {
    public int Goal;
    public int Position;
    public string ID;
    /*
    public MotorMoveEventArgs( string id, int position, int goal )
    {
      Goal = goal;
      Position = position;
      ID = id;
    }
    */
  }
}
