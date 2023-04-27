namespace ContactList.Models{
    public class UpdateContactViewModel{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PriMobileNo { get; set; }
        public string SecMobileNo { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}