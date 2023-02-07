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

      MotorX.AddEventMove( MoveCallback );
      MotorY.AddEventMove( MoveCallback );
      MotorZ.AddEventMove( MoveCallback );

      MotorX.AddEventMoveDone( MoveDoneCallback );
      MotorY.AddEventMoveDone( MoveDoneCallback );
      MotorZ.AddEventMoveDone( MoveDoneCallback );


    }

    public void MoveThread( int x, int y, int z )
    {
      Thread thread1 = new Thread( () => MotorX.Move( x ) );
      Thread thread2 = new Thread( () => MotorY.Move( y ) );
      Thread thread3 = new Thread( () => MotorZ.Move( z ) );
      thread1.Start();
      thread2.Start();
      thread3.Start();
      //_waitHandle.WaitOne();
      thread1.Join();
      thread2.Join();
      thread3.Join();
    }

    public void MoveTask( int x, int y, int z )
    {
      Console.WriteLine( $"MOVE TO {x}, {y}, {z}" );
      int safeZ = 10;

      Console.WriteLine( $"RETRACT Z to {safeZ}" );
      var t1 = Task.Run( () => MotorZ.Move( safeZ ) );
      Task.WaitAll( t1 );

      Console.WriteLine( $"MOVE XY to {x}, {y}" );
      var t2 = Task.Run( () => MotorX.Move( x ) );
      var t3 = Task.Run( () => MotorY.Move( y ) );
      Task.WaitAll( t2, t3 );

      Console.WriteLine( $"DROP Z to {z}" );
      t1 = Task.Run( () => MotorZ.Move( z ) );
      Task.WaitAll( t1 );
    }

    public async Task MoveAwaitAsync( int x, int y, int z ) 
    { 
      var tasks = new List<Task> { MoveXAsync(x), MoveYAsync( y ), MoveZAsync( z )};
      await Task.WhenAll( tasks );
    }

    public async Task MoveXAsync( int i ) { await Task.Run( () => MotorX.Move( i ) ); }
    public async Task MoveYAsync( int i ) { await Task.Run( () => MotorY.Move( i ) ); }
    public async Task MoveZAsync( int i ) { await Task.Run( () => MotorZ.Move( i ) ); }

    

  }
}