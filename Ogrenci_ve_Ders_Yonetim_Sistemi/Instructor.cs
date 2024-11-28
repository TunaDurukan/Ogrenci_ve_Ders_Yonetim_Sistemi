using System.Collections.Generic;

namespace Ogrenci_ve_Ders_Yonetim_Sistemi.Models
{
    public class Instructor : Person
    {
        public string Department { get; set; } = string.Empty;
        public List<string> Courses { get; set; } = new();

        public override string GetFullName()
        {
            return $"{base.GetFullName()} - Departman: {Department}";
        }
    }
}
