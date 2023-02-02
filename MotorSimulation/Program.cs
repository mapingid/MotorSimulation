using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MotorSimulation
{

  class Program
  {
    static void Main( string[] args )
    {

      /*
      var actuator1 = new Actuator( new MotorVendorA("Single", 5000, 0, 5 ) );
      actuator1.Move( 3 );
      actuator1.Move( 8 );
      actuator1.Move( 3 );
      actuator1.Move( -1 );
      */

      ///*
      var actuator3axis = new Actuator3Axis( new MotorVendorA( "MotorX", 100, 0, 50 ),
                                             new MotorVendorA( "MotorY", 500, 0, 50 ),
                                             new MotorVendorA( "MotorZ", 2000, 0, 50 ) );
      actuator3axis.Move( 40, 55, 30 );
      //*/

      Console.Read();
    }
  }
}
