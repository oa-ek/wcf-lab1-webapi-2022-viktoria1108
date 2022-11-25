using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_State_Catalog_WCF.Models;
using VolunteerRequestApp.Server.Core;

namespace Real_State_Catalog.API
{
    [ApiController]
    [Route("api/[controller]")]

    public class FilterController : ControllerBase
    {

        private readonly AppContextDb _context;

        public FilterController(AppContextDb context)
        {
            _context = context;
        }
        // GET: api/<FilterController>
        [HttpGet]
        public async Task<IEnumerable<Offer>> Get(string city, string Type, string nbPerson, string PricePerNight)
        {
            IEnumerable<Offer>? offers = null;

            string type = Type;
            int nbPersonInt = int.Parse(nbPerson);
            double PricePerNightDouble = double.Parse(PricePerNight);

            if (!city.Equals(""))
            {
                offers = await _context.Offers

                    .Where(o => o.Accommodation.Address.City == city && o.Accommodation.Type == type && o.Accommodation.MaxTraveler >= nbPersonInt)
                    .Where(o => o.PricePerNight <= PricePerNightDouble)
                    .Select(o => new Offer
                    {
                        Id = o.Id,
                        AddingDateTime = o.AddingDateTime,
                        StartAvailability = o.StartAvailability,
                        EndAvailability = o.EndAvailability,
                        PricePerNight = o.PricePerNight,
                        CleaningFee = o.CleaningFee,

                        Accommodation = new Accommodation
                        {
                            Name = o.Accommodation.Name,
                            Type = o.Accommodation.Type,
                            Description = o.Accommodation.Description,
                            MaxTraveler = o.Accommodation.MaxTraveler,

                            Address = new Address
                            {
                                City = o.Accommodation.Address.City,
                                Country = o.Accommodation.Address.Country
                            },

                            Pictures = o.Accommodation.Pictures
                        }
                    })
                    .ToListAsync();
            }

            return offers;
        }
    }
}
