using Investec.API.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Investec.UniTest.Fixtures
{
    public static class ClientFixture
    {
        public static List<Client> GetClients() => new List<Client>(){
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
    }
}
