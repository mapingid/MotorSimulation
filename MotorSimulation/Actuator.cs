using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MotorSimulation
{
  class Actuator
  {
    //EVENT
    private void MoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"{s.Elapsed}   ID: {e.ID} MOVE DONE {e.Position} CODE : {(MotorErrorCode)e.Status}" );
    }
    private void MoveCallback( object sender, MotorMoveEventArgs e )
    {
      Console.WriteLine( $"{s.Elapsed}   ID: {e.ID} MOVE {e.Position}/{e.Goal}" );
    }

    // MAIN
    IMotor Motor;
    public static Stopwatch s = Stopwatch.StartNew();
    
    public Actuator( IMotor motor )
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
