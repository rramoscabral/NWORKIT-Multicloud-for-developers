using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Backoffice.Data
{
    public class FlightClientService
    {
        #region Property
        private readonly FlightContext _FlightContext;
        #endregion

        #region Constructor
        public FlightClientService(FlightContext FlightContext)
        {
            _FlightContext = FlightContext;
        }
        #endregion

        #region Get List of FlightClients
        public async Task<List<FlightClient>> GetAllFlightClientsAsync()
        {
            return await _FlightContext.FlightClients.ToListAsync();
        }
        #endregion

        #region Insert FlightClient
        public async Task<bool> InsertFlightClientAsync(FlightClient FlightClient)
        {
            await _FlightContext.FlightClients.AddAsync(FlightClient);
            await _FlightContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Get FlightClient by Id
        public async Task<FlightClient> GetFlightClientAsync(int Id)
        {
            FlightClient FlightClient = await _FlightContext.FlightClients.FirstOrDefaultAsync(c => c.Id.Equals(Id));
            return FlightClient;
        }
        #endregion

        #region Update FlightClient
        public async Task<bool> UpdateFlightClientAsync(FlightClient FlightClient)
        {
             _FlightContext.FlightClients.Update(FlightClient);
            await _FlightContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region DeleteFlightClient
        public async Task<bool> DeleteFlightClientAsync(FlightClient FlightClient)
        {
            _FlightContext.Remove(FlightClient);
            await _FlightContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}