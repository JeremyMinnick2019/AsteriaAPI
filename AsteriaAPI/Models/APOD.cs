namespace AsteriaAPI.Models
{
    public class APOD
    {
        public string? CopyRight { get; set; }

        private string? date;

        public string Date
        {
            get
            {
                if (date == null)
                {
                    return "No Date Available";
                }

                return date;
            }

            set
            {
                if (value == null)
                {
                    date = "No Date Available";
                    return;
                }

                date = DateTime.Parse(value).ToString("MM/dd/yyyy");
            }
        }
        public string? Explanation { get; set; }
        public string? Url { get; set; }
        public string? MediaType { get; set; }
        public string? Title { get; set; }
    }
}
