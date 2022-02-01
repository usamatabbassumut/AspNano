namespace AspNano.DTOs.IdentityDTOs
{

    public class UserDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; } = true;

        //public string PhoneNumber { get; set; }

        //public string ImageUrl { get; set; }
    }


}
