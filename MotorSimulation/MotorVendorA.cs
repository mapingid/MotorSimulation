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
    //EVENT
    public void MoveDoneHandler( object sender, MotorMoveDoneEventArgs e )
    {
      Console.WriteLine( $"MOVE {e.ID} {e.CurrentPosition}/{e.GoalPosition}" );
      if( e.CurrentPosition - e.GoalPosition == 0 ) Console.WriteLine( $"MOVE {e.ID} DONE" );
    }

    // MAIN
    public void MoveCW( ref int position)
    {
      position++;
      Thread.Sleep( 300 );
    }
    public void MoveCCW(ref int position )
    {
      position--;
      Thread.Sleep( 300 );
    }


  }

}
