namespace ErishaBusiness.Services.CommonService.EmailServices.EmailHelper
{
    public class EmailToken
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        //Message token for Contact Enquiry Reply Mail
        public string Message { get; set; }

        public string SessionName { get; set; }
        public string SessionDuration { get; set; }
        public string SessionDate { get; set; }
        public string SessionTime { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string ClassSizeName { get; set; }
        public string ShortMessage { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string OrderId { get; set; }
        public string TwitterUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public string FacebookUrl { get; set; }
    }
}
