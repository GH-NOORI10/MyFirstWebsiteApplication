using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebsiteApplication.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CatName { get; set; }
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
