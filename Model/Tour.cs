﻿using System;

namespace TAgency.Model
{
    /// <summary>
    /// The tour model.
    /// </summary>
    public class Tour
    {
        /// <summary>
        /// The tour identifier.
        /// </summary>
        public int TourID { get; set; }

        /// <summary>
        /// The tour start date.
        /// </summary>
        public DateTime Date { get; set; }
    }
}