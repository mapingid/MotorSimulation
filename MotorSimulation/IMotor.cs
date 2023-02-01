using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  interface IMotor
  {
    int Move( int goalPosition );

  }
}
