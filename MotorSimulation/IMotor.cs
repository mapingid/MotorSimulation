using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  interface IMotor
  {
    
    event EventHandler<MotorMoveDoneEventArgs> MotorMoveDone;
    void AddEventMoveDone( EventHandler<MotorMoveDoneEventArgs> e );
    void OnMoveDone( MotorMoveDoneEventArgs args );
    void Move( int goalPosition );
  }
}
