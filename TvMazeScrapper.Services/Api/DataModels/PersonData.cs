using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMazeScrapper.Services.Api.DataModels
{
    public class PersonData
    {
        public string Id { get; set; }
        public DateTime? Birthday { get; set; }
        public string Name { get; set; }
    }
}
