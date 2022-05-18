using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class Kereso
    {
        public string Megnevezes { get; set; }
        public string Tipus { get; set; }
        public List<Alkatresz> AlkatreszLista { get; set; }
        public SelectList TipusLista { get; set; }
    }
}
