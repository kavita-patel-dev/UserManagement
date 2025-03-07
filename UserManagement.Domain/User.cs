using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
