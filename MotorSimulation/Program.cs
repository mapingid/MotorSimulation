using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{

  class Program
  {

    static void Main( string[] args )
    {

      var actuator1 = new Actuator( "Motor1", new MotorVendorA() );
      var actuator2 = new Actuator( "Motor2", new MotorVendorA() );


      actuator1.Move( 10 );
      actuator2.Move( 2 );


      Console.Read();
    }
  }
}
