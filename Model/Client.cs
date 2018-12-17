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

        /// <summary>
        /// Returns a string that represents the client model.
        /// </summary>
        /// <returns>A string that represents the client model.</returns>
        public override string ToString() => string.Format("#{0:0000}: {1}", ClientID, Name);
    }
}
