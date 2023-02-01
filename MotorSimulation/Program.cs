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
      var motor1 = new Motor();
      var actuator = new Actuator( motor1 );
      actuator.Move(10);

      Console.Read();
    }
  }
}
