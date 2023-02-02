using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MotorSimulation
{
  class Actuator3Axis
  {
    //EVENT
    private void MoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"{GetTimestamp( DateTime.Now )}   ID: {e.ID} move DONE Code : {(MotorErrorCode)e.Status}" );
    }
    private void MoveCallback( object sender, MotorMoveEventArgs e )
    {
      Console.WriteLine( $"{GetTimestamp( DateTime.Now )}   ID: {e.ID} move {e.Position}/{e.Goal}" );
    }

    // MAIN
    IMotor MotorX, MotorY, MotorZ;
    public static String GetTimestamp( DateTime value )
    {
      return value.ToString( "mm:ss:fff" );
    }
    public Actuator3Axis( IMotor motorX, IMotor motorY, IMotor motorZ )
    {
      MotorX = motorX;
      MotorY = motorY;
      MotorZ = motorZ;

      MotorX.AddEventMoveDone( MoveDoneCallback ); MotorX.AddEventMove( MoveCallback );
      MotorY.AddEventMoveDone( MoveDoneCallback ); MotorY.AddEventMove( MoveCallback );
      MotorZ.AddEventMoveDone( MoveDoneCallback ); MotorZ.AddEventMove( MoveCallback );

    }
    public void Move( int x, int y, int z )
    {

      Thread thread1 = new Thread( () => MotorX.Move( x ) );
      Thread thread2 = new Thread( () => MotorY.Move( y ) );
      Thread thread3 = new Thread( () => MotorZ.Move( z ) );

      thread1.Start();
      thread2.Start();
      thread3.Start();

    }

  }
}
