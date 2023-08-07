using System.ComponentModel.DataAnnotations;

namespace cat_mash_api.Domain.Models.Requests.Cats
{
    public class CatPOST
    {
        [Required]
        public string ImageUrl { get; set; }
    }
}
