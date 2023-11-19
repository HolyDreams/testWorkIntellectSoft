using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace testWorkIntellectSoft.API.Models.Struct
{
    [Table("intel_user")]
    public class UserDBStruct
    {
        [Key]
        [Column("user_id", TypeName = "int4")]
        public int ID { get; set; }
        [Column("first_name", TypeName = "text")]
        public string? FirstName { get; set; }
        [Column("last_name", TypeName = "text")]
        public string? LastName { get; set; }
        [Column("birthyear", TypeName = "int2")]
        public int BirthYear { get; set; }
        [Column("delete_state_code", TypeName = "int2")]
        [DefaultValue(0)]
        public int DeleteStateCode { get; set; }
        [ForeignKey("user_id")]
        public ICollection<PhoneDBStruct>? Phones { get; set; }
    }
    [Table("intel_phone")]
    public class PhoneDBStruct
    {
        [Key]
        [Column("phone_id", TypeName = "int4")]
        public int PhoneID { get; set; }
        [Column("phone_number", TypeName = "text")]
        public string? PhoneNumber { get; set; }
        [Column("delete_state_code", TypeName = "int2")]
        [DefaultValue(0)]
        public int DeleteStateCode { get; set; } = 0;
    }
}
