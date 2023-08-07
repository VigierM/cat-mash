using System.ComponentModel.DataAnnotations;

namespace cat_mash_api.Domain.Models.Requests.Votes
{
    public class VotePOST
    {
        [Required]
        public int CatId { get; set; }
    }
}
