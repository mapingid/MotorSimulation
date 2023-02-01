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
    public void Move( int goalPosition )
    {
      int position = 0;

      var args = new MotorMoveDoneEventArgs( ID, position, goalPosition );

      while( ( goalPosition - position ) != 0 )
      {
        if( position > goalPosition )  Motor.MoveCCW( ref position ); 
        else if( position < goalPosition )  Motor.MoveCW( ref position ); 

        args.CurrentPosition = position;
        OnMoveDone( args );
      }
    }


  }
}
