using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CrudOpsMongo.API.Models
{
    public class UserDetailModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(30)]
        [BsonElement(elementName: "Name")]
        public string? FirstName { get; set; }
        //public string? firstName { get; set; }
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(30)]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please enter the age")]
        [Range(1,100, ErrorMessage = "Please enter the age within 1 - 100")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please enter the contact number")]
        [StringLength(10)]
        public string? ContactNumber { get; set; }
        public double Salary { get; set; }
        [StringLength(10)]
        public string? Gender {  get; set; }
        public bool IsActive {  get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
