﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  class MotorMoveDoneEventArgs : EventArgs
  {
    public ushort Status;
    public int Position;
    public string ID;

    /*
    public MotorMoveDoneEventArgs( string id, int position, ushort status )
    {
      Status = status;
      Position = position;
      ID = id;
    }
    */
  }
}
