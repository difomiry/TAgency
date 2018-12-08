namespace TAgency.Model
{
    /// <summary>
    /// The client model.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// The client identifier.
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// The client name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The client address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The client phone number.
        /// </summary>
        public string Phone { get; set; }
    }
}
