using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  interface IMotor
  {
    void MoveCW(ref int position);
    void MoveCCW(ref int position );
    void MoveDoneHandler( object sender, MotorMoveDoneEventArgs e );

  }
}
