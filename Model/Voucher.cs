namespace TAgency.Model
{
    /// <summary>
    /// The voucher model.
    /// </summary>
    public class Voucher
    {
        /// <summary>
        /// The voucher identifier.
        /// </summary>
        public int VoucherID { get; set; }

        /// <summary>
        /// The client identifier.
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// The tour identifier.
        /// </summary>
        public int TourID { get; set; }

        /// <summary>
        /// The number of persons.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The tour cost.
        /// </summary>
        public float Cost { get; set; }
    }
}
