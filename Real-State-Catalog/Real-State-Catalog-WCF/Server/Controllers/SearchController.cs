﻿using Microsoft.AspNetCore.Mvc;
using Real_State_Catalog_WCF.Models;
using System.Data.Entity;
using Real_State_Catalog_WCF.Data;

namespace Real_State_Catalog_WCF.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {

        private readonly AppContextDb _context;
        public SearchController(AppContextDb context)
        {
            _context = context;
        }

        // GET: api/<SearchController>
        [HttpGet("{city}/{arrivalDate}/{departureDate}/{nbPerson}")]

        public async Task<IEnumerable<Offer>> Get(string city, string arrivalDate, string departureDate, string nbPerson)
        {
            IEnumerable<Offer>? offers = null;

            DateTime arrivalDateTime = DateTime.ParseExact(arrivalDate, "yyyy-MM-dd", null);
            DateTime departureDateTime = DateTime.ParseExact(departureDate, "yyyy-MM-dd", null);
            int nbPersonInt = int.Parse(nbPerson);

            if (arrivalDateTime < departureDateTime && !city.Equals(""))
            {
                offers = await _context.Offers
                    .Where(o => o.StartAvailability <= arrivalDateTime && o.EndAvailability > arrivalDateTime && o.EndAvailability >= departureDateTime)
                    .Where(o => o.Accommodation.Address.City == city && o.Accommodation.MaxTraveler >= nbPersonInt)

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


