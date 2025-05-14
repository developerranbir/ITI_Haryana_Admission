using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class GetBasicModel
    {
        public string Keypath { get; set; }
    }    
    public class GetDropDown
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }   
}