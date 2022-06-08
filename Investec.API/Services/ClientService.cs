using Investec.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace Investec.API.Services
{
    public interface IClientService
    {
        public Task<Client> SearchClient(string obj);
        public Task CreateClient(Client obj);
        public Task UpdateClient(Client obj);
        //public Task<List<Client>> GetALLClients();
    }
    public class ClientService: IClientService 
    {
        //public Task SearchClient(string Search)
        //{
        //    return new Task();
        //}
        #region practice
        /* public async Task<List<Client>> GetALLClients()
         {
             var response = await _httpClient.GetAsync("https://example.com");
             if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
             {
                 return new List<Client> { };
             }
             var responsecontent = response.Content;
             var all = await responsecontent.ReadFromJsonAsync<List<Client>>();
             return all.ToList();


         }
          public async Task<List<Client>> CreateClient(Client obj)
          {
              var response = await _httpClient.GetAsync("https://example.com");
              if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
              {
                  return new List<Client> { };
              }
              var responsecontent = response.Content;
              var all = await responsecontent.ReadFromJsonAsync<List<Client>>();
              return all.ToList();


          }
          public async Task<List<Client>> UpdateClient(Client obj)
          {
              var response = await _httpClient.GetAsync("https://example.com");
              if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
              {
                  return new List<Client> { };
              }
              var responsecontent = response.Content;
              var all = await responsecontent.ReadFromJsonAsync<List<Client>>();
              return all.ToList();


          }*/
        #endregion
        public static List<Client> Clients => new List<Client>(){
            new Client()
            {
                FirstName = "Clinton",
                    LastName = "lebyane",
                    MobileNumber = "0788441885",
                    IDNumber = "9803275804084",
                    PhysicalAddress = new Address()
                    {
                        street = "3109 Morise tshabalala",
                        city = "pretoria",
                        zipcode ="0152"
                    }
            },
            new Client()
            {
                FirstName = "Faith",
                    LastName = "lebyane",
                    MobileNumber = "0712470290",
                    IDNumber = "0003275804084",
                    PhysicalAddress = new Address()
                    {
                        street = "3109 Morise tshabalala",
                        city = "pretoria",
                        zipcode ="0152"
                    }
            },
        };
        

        public async Task<Client> SearchClient(string id)
        {
            var Client = new Client();
            if (Clients.Any(cl => cl.IDNumber == id) )
            {
                Client = Clients.Where(cl => cl.IDNumber == id).SingleOrDefault();
            }
            else if (Clients.Any(cl => cl.FirstName == id))
            {
                Client = Clients.Where(cl => cl.FirstName == id).SingleOrDefault();
            }
            else if (Clients.Any(cl => cl.MobileNumber == id))
            {
                Client = Clients.Where(cl => cl.MobileNumber == id).SingleOrDefault();
            }

            return await Task.FromResult(Client);
        }

        public async Task CreateClient(Client obj)
        {
			string id = ValidateID(obj.IDNumber);
			bool isID = Clients.Any(x => x.IDNumber == obj.IDNumber);
			bool isPhone = Clients.Any(x => x.MobileNumber == obj.MobileNumber);

			if (obj.FirstName != null && obj.IDNumber != null && obj.LastName != null && isID == false && isPhone == false&&id=="Valid")
			{
				Clients.Add(obj);
				await Task.CompletedTask;
			}
			
        }

        public async Task UpdateClient(Client obj)
        {
			bool isPhone = Clients.Any(x => x.MobileNumber == obj.MobileNumber);
			if (isPhone == false)
			{
				var index = Clients.FindIndex(existingClient => existingClient.IDNumber == obj.IDNumber);

				Clients[index] = obj;
				await Task.CompletedTask;
			}
        }

		public static string ValidateID(string id_number)
		{
			DateTime currentTime = DateTime.Now;

			/* DO ID LENGTH TEST */
			if (id_number.Length == 13)
			{
				/* SPLIT ID INTO SECTIONS */
				string year = id_number.Substring(0, 2);
				int month = Convert.ToInt32(id_number.Substring(2, 2));
				int day = Convert.ToInt32(id_number.Substring(4, 2));
				int gender = Convert.ToInt32(id_number.Substring(6, 4)) * 1;
				int citizen = Convert.ToInt32(id_number.Substring(10, 2)) * 1;
				int check_sum = Convert.ToInt32(id_number.Substring(12, 1)) * 1;

				/* DO YEAR TEST */
				var nowYearNotCentury = currentTime.Year + "";
				nowYearNotCentury = nowYearNotCentury.Substring(2, 2);
				if (Convert.ToInt32(year) <= Convert.ToInt32(nowYearNotCentury))
				{
					year = "20" + year;
				}
				else
				{
					year = "19" + year;
				}//end if
				if ((Convert.ToInt32(year) > 1900) && (Convert.ToInt32(year) < currentTime.Year))
				{
					//correct
				}
				else
				{
					return "Year is not valid";
				}//end if

				/* DO MONTH TEST */
				if ((month > 0) && (month < 13))
				{
					//correct
				}
				else
				{
					return "Month is not valid";
				}//end if

				/* DO DAY TEST */
				if ((day > 0) && (day < 32))
				{
					//correct
				}
				else
				{
					return "Day is not valid ";
				}//end if

				/* DO DATE TEST */
				if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31)
				{
					return "Date not valid. This month does not have 31 days ";
				}
				if (month == 2)
				{ // check for february 29th
					var isleap = (Convert.ToInt32(year) % 4 == 0 && (Convert.ToInt32(year) % 100 != 0 || Convert.ToInt32(year) % 400 == 0));
					if (day > 29 || (day == 29 && !isleap))
					{
						return "Date not valid. February does not have ' + day + ' days for year " + year + " ";
					}//end if
				}//end if

				/* DO GENDER TEST */
				if ((gender >= 0) && (gender < 10000))
				{
					//correct
				}
				else
				{
					return "Gender is not valid, ";
				}//end if

				/* DO CITIZEN TEST */
				//08 or 09 SA citizen
				//18 or 19 Not SA citizen but with residence permit
				if ((citizen == 8) || (citizen == 9) || (citizen == 18) || (citizen == 19))
				{
					//correct
				}
				else
				{
					return "Citizen value is not valid ";
				}//end if

				/* GET CHECKSUM VALUE */
				int check_sum_odd = 0;
				int check_sum_even = 0;
				string check_sum_even_temp = "";
				int check_sum_value = 0;
				int count = 0;
				// Get ODD Value
				for (count = 0; count < 11; count += 2)
				{
					check_sum_odd += Convert.ToInt32(id_number.Substring(count, 1)) * 1;
				}//end for
				 // Get EVEN Value
				for (count = 0; count < 12; count += 2)
				{
					check_sum_even_temp = check_sum_even_temp + Convert.ToInt32(id_number.Substring(count, 1)) + " ";
				}//end for
				check_sum_even_temp = Convert.ToString(Convert.ToInt32(check_sum_even_temp) * 2);
				check_sum_even_temp = check_sum_even_temp + " ";
				for (count = 0; count < check_sum_even_temp.Length; count++)
				{
					check_sum_even += Convert.ToInt32(check_sum_even_temp.Substring(count, 1)) * 1;
				}//end for
				 // GET Checksum Value
				check_sum_value = (check_sum_odd * 1) + (check_sum_even * 1);
				//check_sum_value = check_sum_value;
				check_sum_value = 10 - Convert.ToInt32(Convert.ToString(check_sum_value).Substring(1, 1)) * 1;
				if (check_sum_value == 10)
					check_sum_value = 0;

				/* DO CHECKSUM TEST */
				if (check_sum_value == check_sum)
				{
					//correct
				}
				else
				{
					return "ID Number is not valid ";
				}//end if

			}
			else
			{
				return "ID Number is not the right length";
			}//end if

			return "Valid";
		}//end function
	}
}
