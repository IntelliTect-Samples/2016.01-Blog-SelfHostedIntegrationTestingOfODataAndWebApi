using System;

namespace Example.Data.Services
{
    public class DataServiceBase : IDisposable
    {
        private bool _Disposed;

        protected DataServiceBase()
        {
            Db = new RaceContext();
        }

        protected RaceContext Db { get; private set; }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        ~DataServiceBase()
        {
            Dispose( false );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( _Disposed )
            {
                return;
            }

            if ( disposing )
            {
                Db.Dispose();
            }

            _Disposed = true;
        }
    }
}