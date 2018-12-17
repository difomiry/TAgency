using System;

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

        /// <summary>
        /// The duration of rest.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// The cost of living in the hotels.
        /// </summary>
        public float Cost { get; set; }

        /// <summary>
        /// The tour start date as string.
        /// </summary>
        public string DateAsString => Date.ToShortDateString();

        /// <summary>
        /// Returns a string that represents the tour model.
        /// </summary>
        /// <returns>A string that represents the tour model.</returns>
        public override string ToString() => string.Format("#{0:0000}: {1} {2}*{3}", TourID, DateAsString, Duration, Cost);
    }
}
