﻿using System;
using System.Collections.Generic;

namespace cw7.Models
{
    public partial class Country
    {
        public Country()
        {
            CountryTrips = new HashSet<CountryTrip>();
        }

        public int IdCountry { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CountryTrip> CountryTrips { get; set; }
    }
}