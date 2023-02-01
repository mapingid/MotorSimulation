using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MotorSimulation
{
  class Motor : IMotor
  {
    public Motor()
    {
      //register MoveDoneHandler() to EventMoveDone
      Actuator.AddEventMoveDone( MoveDoneHandler );
    }
    static void MoveDoneHandler( object sender, MotorMoveDoneEventArgs e )
    {
      if(e.CurrentPosition-e.GoalPosition == 0 ) Console.WriteLine( "MOVE DONE" );
    }
    int position = 0;
    public int Move(int goalPosition)
    {
      while( ( goalPosition - position ) != 0 )
      {
        if( position > goalPosition ) { position--; Console.Write( "-" ); }
        else if( position < goalPosition ) { position++; Console.Write( "+" ); }
        Thread.Sleep( 300 );
      }
      return position;
    }
    

  }

}
