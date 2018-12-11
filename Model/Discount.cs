namespace TAgency.Model
{
    /// <summary>
    /// The discount model.
    /// </summary>
    public class Discount
    {
        /// <summary>
        /// The discount identifier.
        /// </summary>
        public int DiscountID { get; set; }

        /// <summary>
        /// The discount name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The discount percent.
        /// </summary>
        public float Percent { get; set; }
    }
}
