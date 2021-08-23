using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("TicketDetail")]
    public class TicketDetail
    {
        [Key]
        public int Id { get; set; }       
        public string Title { get; set; }
        public string Description { get; set; }
        public string TicketType { get; set; }
        public string  Status { get; set; }
        public DateTime Created { get; set; }

    }
}