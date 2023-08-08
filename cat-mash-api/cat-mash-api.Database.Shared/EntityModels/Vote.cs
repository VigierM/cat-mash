using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cat_mash_api.Database.Shared.EntityModels
{
    // Added a Vote model to demonstrate a real case which would use a User
    public class Vote
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("cat_id")]
        public string CatId { get; set; }
        public Cat Cat { get; set; }

        public Vote() { }
    }
}
