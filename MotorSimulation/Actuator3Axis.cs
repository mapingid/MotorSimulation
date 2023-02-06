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
    void MoveCallback( object sender, MotorMoveEventArgs e )
    {
      Console.WriteLine( $"{GetTimestamp( DateTime.Now )}   ID: {e.ID} move {e.Position}/{e.Goal}" );
    }
    void MoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"{GetTimestamp( DateTime.Now )}   ID: {e.ID} move DONE Code : {(MotorErrorCode)e.Status}" );
    }


    void MotorXMoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"{GetTimestamp( DateTime.Now )}   ID: {e.ID} move DONE Code : {(MotorErrorCode)e.Status}" );
      MotorXDone = true;

      if( MotorXDone && MotorYDone && MotorZDone ) { _waitHandle.Set(); }
    }
    void MotorYMoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"{GetTimestamp( DateTime.Now )}   ID: {e.ID} move DONE Code : {(MotorErrorCode)e.Status}" );
      MotorYDone = true;

      if( MotorXDone && MotorYDone && MotorZDone ) { _waitHandle.Set(); }
    }
    void MotorZMoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"{GetTimestamp( DateTime.Now )}   ID: {e.ID} move DONE Code : {(MotorErrorCode)e.Status}" );
      MotorZDone = true;

      if( MotorXDone && MotorYDone && MotorZDone ) { _waitHandle.Set(); }
    }







    // MAIN
    IMotor MotorX, MotorY, MotorZ;
    bool MotorXDone, MotorYDone, MotorZDone;
    public static AutoResetEvent _waitHandle = new AutoResetEvent( false );

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
      Console.WriteLine( $"MOVE TO {x}, {y}, {z}" );
      MotorXDone = false; MotorYDone = false; MotorZDone = false;

      Thread thread1 = new Thread( () => MotorX.Move( x ) );
      Thread thread2 = new Thread( () => MotorY.Move( y ) );
      Thread thread3 = new Thread( () => MotorZ.Move( z ) );

      thread1.Start();
      thread2.Start();
      thread3.Start();
      _waitHandle.WaitOne();
    }

    public void MoveTask( int x, int y, int z )
    {
      Console.WriteLine( $"MOVE TO {x}, {y}, {z}" );
      var t1 = Task.Run( () => MotorX.Move( x ) );
      var t2 = Task.Run( () => MotorY.Move( y ) );
      var t3 = Task.Run( () => MotorZ.Move( z ) );
      Task.WaitAll(t1, t2, t3);
    }
    public void MoveTaskWRetract( int x, int y, int z )
    {
      int safeZ = 10;
      Console.WriteLine( $"RETRACT Z to {safeZ}" );
      var t1 = Task.Run( () => MotorZ.Move( safeZ ) );
      Task.WaitAll( t1);
      Console.WriteLine( $"MOVE TO {x}, {y}" );
      var t2 = Task.Run( () => MotorX.Move( x ) );
      var t3 = Task.Run( () => MotorY.Move( y ) );
      Task.WaitAll( t2,t3 );
      Console.WriteLine( $"DROPING Z to {safeZ}" );
      t1 = Task.Run( () => MotorZ.Move( z ) );
      Task.WaitAll( t1 );

    }

  }
}