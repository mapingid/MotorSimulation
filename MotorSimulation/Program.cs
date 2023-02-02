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
      var actuator1 = new Actuator( "Motor1", new MotorVendorA() );
      var actuator2 = new Actuator( "Motor2", new MotorVendorA() );

      actuator1.Move( 10 );
      actuator1.Move( 20 );



      //Thread tread1 = new Thread( () => actuator1.Move( 10 ) );
      //Thread tread2 = new Thread( () => actuator1.Move( 20 ) );
      //tread1.Start();
      //tread2.Start();







      Console.Read();
    }
  }
}
