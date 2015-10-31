/*
 *  ********** SVU **********
 ********** Barea_27786 **********
 ********** Object Model **********
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace pro2.Models
{
    public class Objct
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int ExhibitionId { get; set; }
    }
}