using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MotorSimulation
{
  class MotorVendorA : IMotor
  {
    //EVENT HANDLER
    public event EventHandler<MotorMoveDoneEventArgs> MotorMoveDone;
    public virtual void OnMoveDone( MotorMoveDoneEventArgs e )
    {
      MotorMoveDone?.Invoke( this, e );
    }
    public void AddEventMoveDone( EventHandler<MotorMoveDoneEventArgs> e )
    {
      MotorMoveDone += e;
    }

    // MAIN
    int CurrentPosition = 0; 
    int MaxPosition;
    int MinPosition;
    string ID;

    public MotorVendorA(string id, int maxPosition, int minPosition)
    {
      ID = id;
      MaxPosition = maxPosition;
      MinPosition = minPosition;
    }
    public MotorVendorA( string id )
    {
      ID = id;
      MaxPosition = 10;
      MinPosition = 0;
    }

    public void Move( int goalPosition )
    {
      var args = new MotorMoveDoneEventArgs( ID, CurrentPosition, goalPosition );
      while( ( goalPosition - CurrentPosition ) != 0 )
      {
        if( goalPosition > CurrentPosition ) { CurrentPosition++; }
        else if( goalPosition < CurrentPosition ) { CurrentPosition--; }
        Thread.Sleep( 300 );

        args.CurrentPosition = CurrentPosition;
        OnMoveDone( args );
      }


    }

  }

}
