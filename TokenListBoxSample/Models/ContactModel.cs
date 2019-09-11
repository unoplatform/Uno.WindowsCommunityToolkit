namespace TokenListBoxSample.Models
{
    public struct ContactModel
    {
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";
        public string Tooltip => "This is a tooltip";

        public ContactModel(string imageUrl, string firstName, string lastName)
        {
            ImageUrl = imageUrl;
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
