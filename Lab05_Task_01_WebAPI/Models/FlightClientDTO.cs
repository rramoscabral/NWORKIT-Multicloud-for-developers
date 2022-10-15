using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    // Data Transfer Object (DTO) Model 
    // Don't include secret 
    public class FlightClientDTO
    {
        public long Id { get; set; }
        [Required]
        public string? Name { get; set; }
		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
		public string? PassaportNumber { get; set; }
		public string? Nationality { get; set; }
		public string? PhotoBase64 { get; set; }
    }
}