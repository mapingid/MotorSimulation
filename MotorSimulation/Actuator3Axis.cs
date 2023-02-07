using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace MotorSimulation
{
  class Actuator3Axis
  {
    //EVENT
    void MoveCallback( object sender, MotorMoveEventArgs e )
    {
      Console.WriteLine( $"{s.Elapsed}   ID: {e.ID} move {e.Position}/{e.Goal}" );
    }
    void MoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"{s.Elapsed}   ID: {e.ID} move DONE Code : {(MotorErrorCode)e.Status}" );
    }
    void MotorXMoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"{s.Elapsed}   ID: {e.ID} move DONE Code : {(MotorErrorCode)e.Status}" );
      MotorXDone = true;

      if( MotorXDone && MotorYDone && MotorZDone ) { _waitHandle.Set(); }
    }
    void MotorYMoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"{s.Elapsed}   ID: {e.ID} move DONE Code : {(MotorErrorCode)e.Status}" );
      MotorYDone = true;

      if( MotorXDone && MotorYDone && MotorZDone ) { _waitHandle.Set(); }
    }
    void MotorZMoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"{s.Elapsed}   ID: {e.ID} move DONE Code : {(MotorErrorCode)e.Status}" );
      MotorZDone = true;

      if( MotorXDone && MotorYDone && MotorZDone ) { _waitHandle.Set(); }
    }


    // MAIN
    IMotor MotorX, MotorY, MotorZ;
    bool MotorXDone, MotorYDone, MotorZDone;
    public static AutoResetEvent _waitHandle = new AutoResetEvent( false );
    public static Stopwatch s = Stopwatch.StartNew();
    CancellationTokenSource _tokenSource = new CancellationTokenSource();
    public static CancellationToken Token;

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

      Token = _tokenSource.Token;

      var tCancel = Task.Run( () => Cancellation() );
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

      Console.WriteLine( $"retract z to {safeZ}" );
      var t1 = Task.Run( () => MotorZ.Move( safeZ ) );
      Task.WaitAll( t1 );

      Console.WriteLine( $"move xy to {x}, {y}" );
      var t2 = Task.Run( () => MotorX.Move( x ) );
      var t3 = Task.Run( () => MotorY.Move( y ) );
      Task.WaitAll( t2, t3 );

      Console.WriteLine( $"drop z to {z}" );
      t1 = Task.Run( () => MotorZ.Move( z ) );
      Task.WaitAll( t1 );
    }
    public async Task MoveAwaitAsync( int x, int y, int z )
    {
      var tasks = new List<Task> { MoveXAsync( x ), MoveYAsync( y ), MoveZAsync( z ) };
      await Task.WhenAll( tasks );
    }
    public async Task MoveXAsync( int i ) { await Task.Run( () => MotorX.Move( i ) ); }
    public async Task MoveYAsync( int i ) { await Task.Run( () => MotorY.Move( i ) ); }
    public async Task MoveZAsync( int i ) { await Task.Run( () => MotorZ.Move( i ) ); }


    //BIKIN MOVEWITHCANCELLATION
    public void MoveWCancellation( int x, int y, int z )
    {
      int safeZ = 10;

      if( Token.IsCancellationRequested ) { return; }
      Console.WriteLine( $"MOVE TO {x}, {y}, {z}" );

      Console.WriteLine( $"retract z to {safeZ}" );
      var t1 = Task.Run( () => MotorZ.MoveWCancellation( safeZ, Token ) );
      Task.WaitAll( t1 );
      if( Token.IsCancellationRequested ) { return; }
      Console.WriteLine( $"move xy to {x}, {y}" );
      var t2 = Task.Run( () => MotorX.MoveWCancellation( x, Token ) );
      var t3 = Task.Run( () => MotorY.MoveWCancellation( y, Token ) );
      Task.WaitAll( t2, t3 );
      if( Token.IsCancellationRequested ) { return; }
      Console.WriteLine( $"drop z to {z}" );
      t1 = Task.Run( () => MotorZ.MoveWCancellation( z, Token ) );
      Task.WaitAll( t1 );


    }
    void Cancellation()
    {
      Console.WriteLine( "PRESS ENTER TO ABORT" );
      Console.ReadLine();
      _tokenSource.Cancel();
      Console.WriteLine( "ABORTED BY USER" );
    }

  }
}