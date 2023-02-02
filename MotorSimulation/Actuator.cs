using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  class Actuator
  {
    //EVENT
    private void MoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"ID: {e.ID} MOVE DONE {e.Position} CODE : {(MotorErrorCode)e.Status}" );
    }
    private void MoveCallback( object sender, MotorMoveEventArgs e )
    {
      Console.WriteLine( $"ID: {e.ID} MOVE {e.Position}/{e.Goal}" );
    }

    // MAIN
    IMotor Motor;

    public Actuator(IMotor motor )
    {
      Motor = motor;
      Motor.AddEventMoveDone( MoveDoneCallback );
      Motor.AddEventMove( MoveCallback );
    }

    public void Move( int goalPosition )
    {
      Motor.Move( goalPosition );
    }


  }
}
