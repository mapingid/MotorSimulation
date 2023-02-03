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
      var actuator1 = new Actuator( new MotorVendorA("Single", 500, 0, 5 ) );
      actuator1.Move( 3 );
      actuator1.Move( 8 );
      actuator1.Move( 3 );
      actuator1.Move( -1 );
      */

      
      var actuator3axis = new Actuator3Axis( new MotorVendorA( "MotorX", 300, 0, 10 ),
                                             new MotorVendorA( "MotorY", 300, 0, 10 ),
                                             new MotorVendorA( "MotorZ", 300, 0, 10 ) );
      actuator3axis.Move( 3, 3, 3 );
      actuator3axis.Move( 7, 7, 7 ); //double perintah masih error
      

      Console.Read();
    }
  }
}
