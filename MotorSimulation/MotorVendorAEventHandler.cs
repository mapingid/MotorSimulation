using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  partial class MotorVendorA : IMotor
  {
    //MOVE DONE
    public event EventHandler<MotorMoveDoneEventArgs> MotorMoveDone;
    public virtual void OnMoveDone( MotorMoveDoneEventArgs e )
    {
      MotorMoveDone?.Invoke( this, e );
    }
    public void AddEventMoveDone( EventHandler<MotorMoveDoneEventArgs> e )
    {
      MotorMoveDone += e;
    }


    //MOVE
    public event EventHandler<MotorMoveEventArgs> MotorMove;
    public virtual void OnMove( MotorMoveEventArgs e )
    {
      MotorMove?.Invoke( this, e );
    }
    public void AddEventMove( EventHandler<MotorMoveEventArgs> e )
    {
      MotorMove += e;
    }




  }
}
