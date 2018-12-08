namespace TAgency.Model
{
    /// <summary>
    /// The hotel model.
    /// </summary>
    public class Hotel
    {
        /// <summary>
        /// The hotel identifier.
        /// </summary>
        public int HotelID { get; set; }

        /// <summary>
        /// The hotel name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The country in which the hotel is located.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The climate in which the hotel is located.
        /// </summary>
        public string Climate { get; set; }

        /// <summary>
        /// The cost of living in the hotel.
        /// </summary>
        public float Cost { get; set; }
    }
}
