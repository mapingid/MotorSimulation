using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  class Actuator
  {
    // DEFINE EVENT
    public static event EventHandler<MotorMoveDoneEventArgs> MotorMoveDone;
    protected virtual void OnMoveDone( MotorMoveDoneEventArgs e )
    {
      MotorMoveDone?.Invoke( this, e );
    }
    public static void AddEventMoveDone( EventHandler<MotorMoveDoneEventArgs> e )
    {
      MotorMoveDone += e;
    }

    // MAIN
    IMotor _motor;
    public Actuator(IMotor motor)
    {
      _motor = motor;
    }
    public void Move( int goalPosition )
    {
      int position = _motor.Move( goalPosition );
      OnMoveDone( new MotorMoveDoneEventArgs( position, goalPosition ) );
    }
  }
}
