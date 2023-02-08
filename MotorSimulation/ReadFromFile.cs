using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  class ReadFromFile
  {
    public static void WriteCommand( string gcode, int x, int y, int z, int f )
    {
      FileStream fs = new FileStream( "D:\\MotorSimulation\\MotorSimulation\\ExampleFile.txt", FileMode.Append );
      byte[] buffer = Encoding.UTF8.GetBytes( $"{gcode} X{x} Y{y} Z{z} F{f}\n" ); //every character is encoded into 8bit
      fs.Write( buffer, 0, buffer.Length );
      fs.Close();
    }

    public static void MoveWithFile( string path )
    {
      FileStream fs = new FileStream( path, FileMode.Open, FileAccess.Read );

      byte[] bytes = new byte[fs.Length]; //fs.length return total karakter
      int numBytesToRead = (int)fs.Length;
      int numBytesRead = 0;

      while( numBytesToRead > 0 )
      {
        int n = fs.Read( bytes, numBytesRead, numBytesToRead );
        if( n == 0 )
          break;
        numBytesRead += n;
        numBytesToRead -= n;
      }
      numBytesToRead = bytes.Length;

      //foreach(byte a in bytes )
      //{
      //Console.Write(  );
      //}

      string raw = Encoding.UTF8.GetString( bytes );
      string[] codes = raw.Split( '\n' ).ToArray();
      foreach(string code in codes)
      {
        string[] buffer = code.Split( ' ' ).ToArray();
        
        string gcode = buffer[0];
        int x = Int16.Parse( buffer[1][1..] );
        int y = Int16.Parse( buffer[2][1..] );
        int z = Int16.Parse( buffer[3][1..] );
        int f = Int16.Parse( buffer[4][1..] );

        Console.WriteLine($"{gcode} {x} {y} {z} {f}");
      }
      fs.Close();
    }

  }
}
