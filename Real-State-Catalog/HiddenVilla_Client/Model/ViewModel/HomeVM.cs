﻿using System;

namespace Real_State_Catalog_Client.Model.ViewModel
{
    public class HomeVM
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int NoOfNights { get; set; } = 1;
    }
}
