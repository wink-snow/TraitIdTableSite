using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraitBrowser.Models
{
    public class Trait
    {
        public int Id { get; set; }
        public int TraitId { get; set; }
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }

        public string? LocName { get; set; }
        public string? locInfo { get; set; }// clearTag
    }
}