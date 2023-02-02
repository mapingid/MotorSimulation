/* TODO
 * BUG WHEN RUNNING SAME ACTUATOR IN DIFFERENT THREAD
 * 
 * 
 */


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
      var actuator1 = new Actuator( new MotorVendorA("A") );
      actuator1.Move( 3 );
    
      //Thread tread1 = new Thread( () => actuator1.Move( 10 ) );
      //Thread tread2 = new Thread( () => actuator1.Move( 20 ) );
      //tread1.Start();
      //tread2.Start();

      Console.Read();
    }
  }
}
