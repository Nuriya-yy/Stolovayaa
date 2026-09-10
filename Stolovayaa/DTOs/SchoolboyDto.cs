using System;

namespace Stolovayaa.DTOs
{
    public class SchoolboyDto
    {
        public int Id { get; set; }     
        public string Name { get; set; }
        public string FirstName { get; set; }    
        public int ClassNumber { get; set; }     
        public decimal Balans { get; set; }      

        // Для отображения в ComboBox
        public string FullName => $"{Name} {FirstName}";
    }
}