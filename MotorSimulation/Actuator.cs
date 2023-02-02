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
    public void MoveDoneCallback( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"MOVE {e.ID} {e.CurrentPosition}/{e.GoalPosition}" ); // MOVE
      if( e.CurrentPosition - e.GoalPosition == 0 ) Console.WriteLine( $"MOVE {e.ID} DONE" ); //DONE
    }

    // MAIN
    IMotor Motor;
    IMotor MotorX, MotorY, MotorZ;

    public Actuator(IMotor motor )
    {
      Motor = motor;
      Motor.AddEventMoveDone( MoveDoneCallback ); 
    }
    
    public void Move( int goalPosition )
    {
      Motor.Move( goalPosition );
    }


  }
}
