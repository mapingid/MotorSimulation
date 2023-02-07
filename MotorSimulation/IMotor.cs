using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MotorSimulation
{
  interface IMotor
  {
    
    event EventHandler<MotorMoveDoneEventArgs> MotorMoveDone;
    void AddEventMoveDone( EventHandler<MotorMoveDoneEventArgs> e );
    void OnMoveDone( MotorMoveDoneEventArgs args );


    event EventHandler<MotorMoveEventArgs> MotorMove;
    void AddEventMove( EventHandler<MotorMoveEventArgs> e );
    void OnMove( MotorMoveEventArgs args );
    
    
    
    
    void Move( int goalPosition );
    void MoveWCancellation(int goalPosition, CancellationToken token);

    void SetSpeed( int speed );
  }
}
