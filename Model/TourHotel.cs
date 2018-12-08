namespace TAgency.Model
{
    /// <summary>
    /// The relation tour-to-hotel model.
    /// </summary>
    public class TourHotel
    {
        /// <summary>
        /// The tour identifier.
        /// </summary>
        public int TourID { get; set; }

        /// <summary>
        /// The hotel identifier.
        /// </summary>
        public int HotelID { get; set; }

        /// <summary>
        /// The duration of rest.
        /// </summary>
        public int Duration { get; set; }
    }
}
