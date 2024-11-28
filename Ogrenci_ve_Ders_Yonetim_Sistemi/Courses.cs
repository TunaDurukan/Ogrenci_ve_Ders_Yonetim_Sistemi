using System.Collections.Generic;
using System.Linq;

namespace Ogrenci_ve_Ders_Yonetim_Sistemi.Models
{
    public class Courses
    {
        public string CourseName { get; set; } = string.Empty;
        public int Credit { get; set; } = 0;
        public Instructor? Instructor { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new();

        public string GetCourseDetails()
        {
            var studentInfo = EnrolledStudents.Any()
                ? string.Join(", ", EnrolledStudents.Select(s => s.GetFullName()))
                : "Bu derse kayıtlı öğrenci yok.";

            return $"Ders Adı: {CourseName}, Kredi: {Credit}, Öğretim Görevlisi: {Instructor?.GetFullName() ?? "Atanmamış"}, Kayıtlı Öğrenciler: {studentInfo}";
        }
    }
}
