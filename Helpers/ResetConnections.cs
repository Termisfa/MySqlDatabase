using MySqlDatabase.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDatabase.Helpers
{
    public class ResetConnections : IResetConnections
    {
        private readonly IConnectionsHandler _connections;

        public ResetConnections(IConnectionsHandler connections)
        {
            _connections = connections;
        }

        public void Initialize()
        {
            try
            {
                var timer = new System.Timers.Timer(1000 * 60 * 60 * 1); //It should be 1000 * 60 * 60 * 1  (1 hour)
                timer.Start();
                timer.Elapsed += Reset;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void Reset(object? sender, System.Timers.ElapsedEventArgs elapsed)
        {
            try
            {
                _connections.Initialize();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
