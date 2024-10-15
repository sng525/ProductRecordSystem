using System;
using Microsoft.EntityFrameworkCore;
using ProductRecordSystem.Data;

namespace ProductRecordSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public string PartNumber { get; set;}
    }

}

