using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorSimulation
{
  class DisposedClassTest
  {
    // To detect redundant calls
    private bool _disposedValue;

    ~DisposedClassTest() => Dispose( false );

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
      Dispose( true );
      GC.SuppressFinalize( this );
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose( bool disposing )
    {
      if( !_disposedValue )
      {
        if( disposing )
        {

          // Call Dispose() on other objects owned by this instance.
          // You can reference other finalizable objects here.
          // ...
        }
        Console.WriteLine( "DISPOSING DisposedClassTest" );
        // Release unmanaged resources owned by (just) this object.
        // ...
        _disposedValue = true;
      }
    }
  }
}
