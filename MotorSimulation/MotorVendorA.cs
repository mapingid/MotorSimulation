/* TODO
 * RAISE EVENT MOVE
 * RAISE EVENT ERROR
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MotorSimulation
{
  enum MotorErrorCode
  {
    NoError = 0,
    NotReachTarget = 1,
    MaximumPosition = 2,
    MinimumPosition = 3
  }

  partial class MotorVendorA : IMotor
  {
    int CurrentPosition = 0;
    int MaxPosition;
    int MinPosition;
    string ID;
    MotorMoveDoneEventArgs MoveDoneArgs = new MotorMoveDoneEventArgs();
    MotorMoveEventArgs MoveArgs = new MotorMoveEventArgs();

    public MotorVendorA( string id, int minPosition, int maxPosition )
    {
      ID = id;
      MaxPosition = maxPosition;
      MinPosition = minPosition;
      MoveDoneArgs.ID = ID;
      MoveArgs.ID = ID;
    }
    
    public void Move( int goalPosition )
    {
      int MaxStep = Math.Abs( goalPosition - CurrentPosition );
      MoveArgs.Goal = goalPosition;


      while( MaxStep > 0 )
      {       
        if( goalPosition > CurrentPosition ) { CurrentPosition++; }
        else if( goalPosition < CurrentPosition ) { CurrentPosition--; }

        MoveArgs.Position = CurrentPosition;
        OnMove( MoveArgs );
        
        Thread.Sleep( 300 );
        
        MaxStep--;
      }

      MoveDoneArgs.Position = CurrentPosition;
      if( CurrentPosition - goalPosition == 0 ) { MoveDoneArgs.Status = (int)MotorErrorCode.NoError; }
      else if( CurrentPosition - goalPosition != 0 ) { MoveDoneArgs.Status = (int)MotorErrorCode.NotReachTarget; ; }

      OnMoveDone( MoveDoneArgs );
    }

  }

}
