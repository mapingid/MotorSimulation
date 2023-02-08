﻿using MotorSimulation;

internal class Program
{
  private static void Main( string[] args )
  {
    /*
      for(int i=0; i<22000; i++ )
      {
        Console.WriteLine( i );
        new Actuator( new MotorVendorA( "Single", 500, 0, 5 ) );
      }
      */
    //var actuator1 =  new Actuator( new MotorVendorA( "Single", 500, 0, 5 ) );
    //actuator1.Move( 3 );
    //actuator1.WaitMoveDone();
    //actuator1.Move( 8 );
    //actuator1.Move( 3 );
    //actuator1.Move( -1 );



    //var actuator3axis = new Actuator3Axis( new MotorVendorA( "MotorX", 500, 0, 10 ),
    //                                       new MotorVendorA( "MotorY", 500, 0, 10 ),
    //                                       new MotorVendorA( "MotorZ", 500, 0, 10 ) );

    //actuator3axis.MoveThread( 3, 3, 3 );
    //actuator3axis.MoveThread( 7, 7, 7 );
    //actuator3axis.MoveThread( 1, 1, 1 );

    //actuator3axis.MoveTask( 3, 3, 3 );
    //actuator3axis.MoveTask( 7, 7, 7 );
    //actuator3axis.MoveTask( 1, 1, 1 );

    // TO USE : change void to async Task in Main method
    //await actuator3axis.MoveAwaitAsync( 3, 3, 3 );
    //await actuator3axis.MoveAwaitAsync( 7, 7, 7 );
    //await actuator3axis.MoveAwaitAsync( 1, 1, 1 );

    //actuator3axis.MoveWCancellation( 3, 3, 3 );
    //actuator3axis.MoveWCancellation( 7, 7, 7 );
    //actuator3axis.MoveWCancellation( 1, 1, 1 );

    //Console.WriteLine( "END" );

    ReadFromFile.MoveWithFile( "D:\\MotorSimulation\\MotorSimulation\\ExampleFile.txt" );

    //Console.ReadLine();
  }
}