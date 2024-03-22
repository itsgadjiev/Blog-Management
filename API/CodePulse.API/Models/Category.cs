using System.ComponentModel.DataAnnotations.Schema;

namespace CodePulse.API.Models
{
    [Table("Categories")]
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
