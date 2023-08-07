using cat_mash_api.Domain.Models.Requests.Cats;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cat_mash_api.Database.Shared.EntityModels
{
    public class Cat
    {
        [Key, Column("id")]
        public string Id { get; set; }

        [Required, Column("image_url")]
        public string ImageUrl { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public Cat() 
        { 
            Votes = new List<Vote>();
        }

        public Cat(CatPOST cat)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            Id = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            ImageUrl = cat.ImageUrl;
        }

        public void Update (CatPUT cat)
        {
            ImageUrl = cat.ImageUrl ?? ImageUrl;
        }
    }
}
