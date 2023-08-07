using System.ComponentModel.DataAnnotations;

namespace cat_mash_api.Domain.Models.Requests.Cats
{
    public class CatPUT
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
