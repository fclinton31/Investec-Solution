using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investec.API.Model;
using Investec.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Investec.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
       
        private readonly IClientService _clientService;

        
        public ClientController( IClientService clientService)
        {
            _clientService = clientService;
        }
		#region Practice 
		/*[HttpGet(Name ="Clients")]
        public async Task<IActionResult> Get1()
        {
            var client = await _clientService.GetALLClients();
            if(client.Any())
            {
                return Ok(client);
            }
            return NotFound();
            
	}*/

	#endregion
	// GET /Search client using FirstName or IDNumber or Phone Number
	/// <summary>
	/// SearchClient
	/// </summary>
	/// <param name="search"></param>
	/// <returns></returns>
	   [HttpGet]
	   [Route("Get")]

		public async Task<ActionResult<Client>> SearchClient(string search)
		{
			var client = await _clientService.SearchClient(search);

			if (client is null)
			{
				return NotFound();
			}

			return Ok(client);
		}

		// POST / Create a new client
		/// <summary>
		/// CreateClient
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("POST")]
		public async Task<ActionResult<Client>> CreateClient(Client obj)
		{
			Client client = new Client
			{
				FirstName = obj.FirstName,
				LastName = obj.LastName,
				MobileNumber = obj.MobileNumber,
				IDNumber = obj.IDNumber,
				PhysicalAddress = new Address()
				{
					street = obj.PhysicalAddress.street,
					city = obj.PhysicalAddress.city,
					zipcode = obj.PhysicalAddress.zipcode
				}
			};

			await _clientService.CreateClient(obj);

			return CreatedAtAction(nameof(SearchClient), new { id = obj.IDNumber }, Ok(client));
		}

		// PUT /Create a new client
		/// <summary>
		/// UpdateClient
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("update")]
		public async Task<ActionResult> UpdateClient(Client obj)
		{
			var existingClient = await _clientService.SearchClient(obj.IDNumber);

			if (existingClient is null)
			{
				return NotFound();
			}

			existingClient.FirstName = obj.FirstName;
				existingClient.LastName = obj.LastName;
				existingClient.MobileNumber = obj.MobileNumber;

			existingClient.PhysicalAddress = new Address()
			{
				street = obj.PhysicalAddress.street,
				city = obj.PhysicalAddress.city,
				zipcode = obj.PhysicalAddress.zipcode
			};

			await _clientService.UpdateClient(existingClient);

			return NoContent();
		}
		
	}
}
