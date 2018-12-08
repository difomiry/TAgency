namespace TAgency.Model
{
    /// <summary>
    /// The relation client-to-tour model.
    /// </summary>
    public class ClientTour
    {
        /// <summary>
        /// The client identifier.
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// The tour identifier.
        /// </summary>
        public int TourID { get; set; }

        /// <summary>
        /// The number of vouchers.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The tour cost.
        /// </summary>
        public float Cost { get; set; }
    }
}
