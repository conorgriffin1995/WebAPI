using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace SMSService.Models
{
    public class TextMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(140, MinimumLength = 5, ErrorMessage = "Length of message must be between 5 and 140 characters.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        public string PhoneNumberSender { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        public string PhoneNumberReceiver { get; set; }

    }

    public class SMSContext : DbContext
    {
        public SMSContext() : base("DefaultConnection")
        {
            Database.SetInitializer<SMSContext>(new CreateDatabaseIfNotExists<SMSContext>());
        }
        public DbSet<TextMessage> Messages { get; set; }

    }

    //public class SMSServiceInitializer : CreateDatabaseIfNotExists<SMSContext>
    //{
    //    protected override void Seed(SMSContext smscontext)
    //    {
    //        var messages = new List<TextMessage>()
    //        {
    //            new TextMessage { Content = "Hello there!!", PhoneNumberSender = "0851472216", PhoneNumberReceiver = "0869987745"},
    //            new TextMessage { Content = "Hello world.", PhoneNumberSender = "0877451121", PhoneNumberReceiver = "0895564412"},
    //            new TextMessage { Content = "Hello How are u???!!", PhoneNumberSender = "0851498216", PhoneNumberReceiver = "0859984745"}
    //        };
    //        messages.ForEach(m => smscontext.Messages.Add(m));
    //        smscontext.SaveChanges();
    //    }
    //}
}