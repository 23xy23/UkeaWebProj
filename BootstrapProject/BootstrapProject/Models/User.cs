using System.ComponentModel.DataAnnotations;

namespace BootstrapProject.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }  

        public String FirstName {get;set;}  
        public String LastName {get;set;}  
        public String Username {get;set;}  
        public String Email {get;set;}  
        public String ContactNo {get;set;}  
        public String Address {get;set;}  
        public int PostalCode {get;set;}
        public byte[] PasswordHash {get;set;}
        public byte[] Salt {get;set;}

        public DateTime LastLogin {get;set;}
        public DateTime CreatedAt{get;set;}
        public DateTime UpdatedAt{get;set;}
        public string UserType { get; set; } // Discriminator column
        

    }
}