namespace JibbleGroup.Model
{
    /// <summary>
    /// Person properties
    /// </summary>
    public class People
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Gender Gender { get; set; }
        public string Age { get; set; }
        public string[] Emails { get; set; }
        public string FavoriteFeature { get; set; }
        public string[] Features { get; set; }
        public AddressInfo[] AddressInfo { get; set; }
        public AddressInfo HomeAddress { get; set; }
        public string odatatype { get; set; }
        public int Budget { get; set; }
        public string BossOffice { get; set; }
        public int Cost { get; set; }
    }
}