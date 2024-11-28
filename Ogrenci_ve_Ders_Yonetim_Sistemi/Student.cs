using System.Collections.Generic;

namespace Ogrenci_ve_Ders_Yonetim_Sistemi.Models
{
    public class Student : Person
    {
        public string StudentNumber { get; set; } = string.Empty;
        public List<Courses> EnrolledCourses { get; set; } = new();

        public override string GetFullName()
        {
            return $"{base.GetFullName()} - Öğrenci Numarası: {StudentNumber}";
        }
    }
}
