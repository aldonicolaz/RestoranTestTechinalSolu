using RestoranTestTechinal.Data;
using RestoranTestTechinal.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoranTestTechinal.Models
{
    public class Menus : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
       public string? Category { get; set; }
        public bool IsInStock { get; set; }

     
    }
}
