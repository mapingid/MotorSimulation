using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  class Actuator
  {
    //EVENT
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
    IMotor Motor;
    string ID;
    public Actuator( string id, IMotor motor )
    {
      Motor = motor;
      ID = id;
      AddEventMoveDone( Motor.MoveDoneHandler ); //ini taruh actuator
    }
    int Position = 0;
    public void Move( int goalPosition )
    {
      var args = new MotorMoveDoneEventArgs( ID, Position, goalPosition );

      while( ( goalPosition - Position ) != 0 )
      {
        if( Position > goalPosition )  Motor.MoveCCW( ref Position ); 
        else if( Position < goalPosition )  Motor.MoveCW( ref Position ); 

        args.CurrentPosition = Position;
        OnMoveDone( args );
      }
    }


  }
}
