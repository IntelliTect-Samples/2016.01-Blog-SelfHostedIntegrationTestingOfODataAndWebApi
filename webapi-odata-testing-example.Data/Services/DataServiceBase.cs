using System;

namespace Example.Data.Services
{
    public class DataServiceBase : IDisposable
    {
        private bool _disposed;

        public DataServiceBase()
        {
            Db = new RaceContext();
        }

        protected RaceContext Db { get; set; }

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
            if ( _disposed )
            {
                return;
            }

            if ( disposing )
            {
                Db.Dispose();
            }

            _disposed = true;
        }
    }
}