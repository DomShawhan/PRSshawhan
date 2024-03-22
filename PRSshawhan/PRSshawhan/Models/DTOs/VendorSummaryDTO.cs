namespace PRSshawhan.Models.DTOs
{
    public record VendorSummaryDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int CountOfProducts { get; set; }

        public VendorSummaryDTO(string code, string name, int countofproducts) 
        {
            this.Code = code;
            this.Name = name;
            this.CountOfProducts = countofproducts;
        }
    }
}
