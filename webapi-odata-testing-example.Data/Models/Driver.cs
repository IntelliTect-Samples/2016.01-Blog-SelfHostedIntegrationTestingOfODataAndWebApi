using System.ComponentModel.DataAnnotations;

namespace webapi_odata_testing_example.Data.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}