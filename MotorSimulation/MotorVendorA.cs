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
    int Speed;
    MotorMoveDoneEventArgs MoveDoneArgs = new MotorMoveDoneEventArgs();
    MotorMoveEventArgs MoveArgs = new MotorMoveEventArgs();

    public MotorVendorA( string id,int speed, int minPosition, int maxPosition )
    {
      ID = id;
      MaxPosition = maxPosition;
      MinPosition = minPosition;
      Speed = speed;
      MoveDoneArgs.ID = ID;
      MoveArgs.ID = ID;
    }

    public void Move( int goalPosition )
    {
      int MaxStep = Math.Abs( goalPosition - CurrentPosition );
      MoveArgs.Goal = goalPosition;


      while( MaxStep > 0 )
      {
        if( goalPosition > CurrentPosition ) //CW
        {
          if( CurrentPosition < MaxPosition )
          {
            CurrentPosition++;
          }
          else
          {
            break;
          }
        }

        else if( goalPosition < CurrentPosition ) //CCW
        {
          if( CurrentPosition > MinPosition )
          {
            CurrentPosition--;
          }
          else
          {
            break;
          }
        }

        MoveArgs.Position = CurrentPosition;
        OnMove( MoveArgs );

        Thread.Sleep( Speed );
        MaxStep--;
        
      }
      
      MoveDoneArgs.Position = CurrentPosition;
      if( CurrentPosition - goalPosition == 0 ) { MoveDoneArgs.Status = (int)MotorErrorCode.NoError; }
      else if( CurrentPosition == MaxPosition ) { MoveDoneArgs.Status = (int)MotorErrorCode.MaximumPosition; }
      else if( CurrentPosition == MinPosition ) { MoveDoneArgs.Status = (int)MotorErrorCode.MinimumPosition; }
      else if( CurrentPosition - goalPosition != 0 ) { MoveDoneArgs.Status = (int)MotorErrorCode.NotReachTarget; }

      OnMoveDone( MoveDoneArgs );
    }

    

  }

}
