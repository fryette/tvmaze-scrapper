using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMazeScrapper.Domain.App
{
    public class Person
    {
        public DateTime? Birthday { get; set; }
        public string Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRow { get; set; }

        public string Name { get; set; }
    }
}