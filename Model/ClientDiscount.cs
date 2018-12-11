namespace TAgency.Model
{
    /// <summary>
    /// The relation client-to-discount model.
    /// </summary>
    public class ClientDiscount
    {
        /// <summary>
        /// The client identifier.
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// The discount identifier.
        /// </summary>
        public int DiscountID { get; set; }
    }
}
