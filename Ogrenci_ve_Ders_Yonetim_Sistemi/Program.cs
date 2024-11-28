using System;
using System.Collections.Generic;
using Ogrenci_ve_Ders_Yonetim_Sistemi.Models;

namespace Ogrenci_ve_Ders_Yonetim_Sistemi
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static List<Instructor> instructors = new List<Instructor>();
        static List<Courses> courses = new List<Courses>();

        static void Main(string[] args)
        {
            InitializeData();

            while (true)
            {
                Console.WriteLine("\n=== Öğrenci ve Ders Yönetim Sistemi ===");
                Console.WriteLine("1- Öğrenci Yönetimi");
                Console.WriteLine("2- Öğretim Görevlisi Yönetimi");
                Console.WriteLine("3- Ders Yönetimi");
                Console.WriteLine("0- Çıkış");
                Console.Write("Seçiminiz: ");

                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        StudentManagementMenu();
                        break;
                    case "2":
                        InstructorManagementMenu();
                        break;
                    case "3":
                        CourseManagementMenu();
                        break;
                    case "4":
                        AssignCourseToStudent();
                        break;
                    case "0":
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                        break;
                }
            }
        }

        static void InitializeData()
        {
            var course1 = new Courses
            {
                CourseName = "Nesneye Dayalı Programlama",
                Credit = 3
            };

            var course2 = new Courses
            {
                CourseName = "Görsel Programlama",
                Credit = 3
            };

            courses.Add(course1);
            courses.Add(course2);

            var instructor = new Instructor
            {
                FirstName = "Emrah",
                LastName = "Sarıçiçek",
                Email = "esaricicek@stu.pirireis.edu.tr",
                Department = "Bilgisayar Programcılığı"
            };

            course1.Instructor = instructor;
            course2.Instructor = instructor;

            instructors.Add(instructor);
        }

        static void StudentManagementMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Öğrenci Yönetimi ===");
                Console.WriteLine("1- Öğrenci Ekle");
                Console.WriteLine("2- Öğrenci Sil");
                Console.WriteLine("3- Öğrenci Listesi");
                Console.WriteLine("4- Öğrenciye Ders Atama");
                Console.WriteLine("0- Geri Dön");
                Console.Write("Seçiminiz: ");

                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        RemoveStudent();
                        break;
                    case "3":
                        ListStudents();
                        break;
                    case "4":
                        AssignCourseToStudent();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                        break;
                }
            }
        }

        static void AddStudent()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("Kayıtlı ders bulunmuyor, önce ders ekleyin.");
                return;
            }

            Console.Write("Öğrenci Adı: ");
            string firstName = Console.ReadLine() ?? string.Empty;
            Console.Write("Öğrenci Soyadı: ");
            string lastName = Console.ReadLine() ?? string.Empty;
            Console.Write("E-posta: ");
            string email = Console.ReadLine() ?? string.Empty;
            Console.Write("Öğrenci Numarası: ");
            string studentNumber = Console.ReadLine() ?? string.Empty;

            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                StudentNumber = studentNumber
            };
            
            string registerChoice;
            do
            {
                Console.Write("Öğrenciyi bir derse kaydetmek istiyor musunuz? (E/H): ");
                registerChoice = Console.ReadLine()?.ToUpper() ?? string.Empty;

                if (registerChoice == "E")
                {
                    Console.WriteLine("\nMevcut Dersler:");
                    for (int i = 0; i < courses.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {courses[i].CourseName} (Kredi: {courses[i].Credit})");
                    }

                    while (true)
                    {
                        Console.Write("Kayıt etmek istediğiniz dersin numarasını girin: ");
                        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= courses.Count)
                        {
                            var selectedCourse = courses[index - 1];
                            student.EnrolledCourses.Add(selectedCourse);
                            selectedCourse.EnrolledStudents.Add(student);
                            Console.WriteLine($"{student.FirstName} {student.LastName} | {selectedCourse.CourseName} dersine başarıyla kaydedildi.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                        }
                    }
                }
                else if (registerChoice != "H")
                {
                    Console.WriteLine("Geçersiz giriş! Lütfen sadece 'E' veya 'H' girin.");
                }
            } while (registerChoice != "H");

            students.Add(student);
            Console.WriteLine("Öğrenci başarıyla eklendi.");
        }

        static void RemoveStudent()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Mevcut öğrenci yok! Lütfen önce öğrenci ekleyin.");
                return;
            }

            Console.WriteLine("\nMevcut Öğrenciler:");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i].GetFullName()}");
            }

            Console.Write("Silmek istediğiniz öğrencinin numarasını seçin: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= students.Count)
            {
                students.RemoveAt(index - 1);
                Console.WriteLine("Öğrenci başarıyla silindi.");
            }
            else
            {
                Console.WriteLine("Geçersiz seçim.");
            }
        }

        static void ListStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Mevcut öğrenci yok! Lütfen önce öğrenci ekleyin.");
                return;
            }

            Console.WriteLine("\nMevcut Öğrenciler:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.GetFullName()} - Email: {student.Email} - Aldığı Ders Sayısı: {student.EnrolledCourses.Count}");
                Console.WriteLine("Aldığı Dersler:");
                foreach (var course in student.EnrolledCourses)
                {
                    Console.WriteLine($"- {course.CourseName} Kredi: {course.Credit}");
                }
            }
        }

        static void AssignCourseToStudent()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Kayıtlı öğrenci yok! Önce öğrenci ekleyin.");
                return;
            }
            if (courses.Count == 0)
            {
                Console.WriteLine("Kayıtlı ders yok! Önce ders ekleyin.");
                return;
            }

            Console.WriteLine("\nMevcut Öğrenciler:");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i].FirstName} {students[i].LastName}");
            }

            Console.Write("Ders atamak istediğiniz öğrenciyi seçin: ");
            if (int.TryParse(Console.ReadLine(), out int studentIndex) && studentIndex > 0 && studentIndex <= students.Count)
            {
                var student = students[studentIndex - 1];

                var availableCourses = courses.FindAll(course => !student.EnrolledCourses.Contains(course));

                if (availableCourses.Count == 0)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName} için atanabilecek ders bulunmuyor.");
                    return;
                }

                Console.WriteLine("\nAtanabilecek Dersler:");
                for (int i = 0; i < availableCourses.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {availableCourses[i].CourseName} - Kredi: {availableCourses[i].Credit}");
                }

                Console.Write("Atamak istediğiniz dersi seçin: ");
                if (int.TryParse(Console.ReadLine(), out int courseIndex) && courseIndex > 0 && courseIndex <= availableCourses.Count)
                {
                    var selectedCourse = availableCourses[courseIndex - 1];

                    student.EnrolledCourses.Add(selectedCourse);
                    selectedCourse.EnrolledStudents.Add(student);
                    Console.WriteLine($"{student.FirstName} {student.LastName} başarıyla {selectedCourse.CourseName} dersine kaydedildi.");
                }
                else
                {
                    Console.WriteLine("Geçersiz ders seçimi.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz öğrenci seçimi.");
            }
        }

        static void InstructorManagementMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Öğretim Görevlisi Yönetimi ===");
                Console.WriteLine("1- Öğretim Görevlisi Ekle");
                Console.WriteLine("2- Öğretim Görevlisi Sil");
                Console.WriteLine("3- Öğretim Görevlisi Listesi");
                Console.WriteLine("4- Öğretim Görevlisine Ders Atama");
                Console.WriteLine("0- Geri Dön");
                Console.Write("Seçiminiz: ");

                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        AddInstructor();
                        break;
                    case "2":
                        RemoveInstructor();
                        break;
                    case "3":
                        ListInstructors();
                        break;
                    case "4":
                        AssignCourseToInstructor();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                        break;
                }
            }
        }

        static void AddInstructor()
        {
            Console.Write("Adı: ");
            string firstName = Console.ReadLine() ?? string.Empty;
            Console.Write("Soyadı: ");
            string lastName = Console.ReadLine() ?? string.Empty;
            Console.Write("E-posta: ");
            string email = Console.ReadLine() ?? string.Empty;
            Console.Write("Departman: ");
            string department = Console.ReadLine() ?? string.Empty;

            var instructor = new Instructor
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Department = department
            };

            instructors.Add(instructor);
            Console.WriteLine("Öğretim görevlisi başarıyla eklendi.");
        }

        static void AssignCourseToInstructor()
        {
            if (instructors.Count == 0)
            {
                Console.WriteLine("Kayıtlı öğretim görevlisi yok! Önce öğretim görevlisi ekleyin.");
                return;
            }

            if (courses.Count == 0)
            {
                Console.WriteLine("Kayıtlı ders yok! Önce ders ekleyin.");
                return;
            }

            Console.WriteLine("\nMevcut Öğretim Görevlileri:");
            for (int i = 0; i < instructors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {instructors[i].GetFullName()}");
            }

            Console.Write("Ders atamak istediğiniz öğretim görevlisinin numarasını seçin: ");
            if (int.TryParse(Console.ReadLine(), out int instructorIndex) && instructorIndex > 0 && instructorIndex <= instructors.Count)
            {
                var instructor = instructors[instructorIndex - 1];

                var availableCourses = courses.FindAll(course => course.Instructor != instructor);

                if (availableCourses.Count == 0)
                {
                    Console.WriteLine($"{instructor.FirstName} {instructor.LastName} adlı öğretim görevlisine atanabilecek ders bulunmuyor.");
                    return;
                }

                Console.WriteLine("\nAtanabilecek Dersler:");
                for (int i = 0; i < availableCourses.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {availableCourses[i].CourseName}");
                }

                Console.Write("Atamak istediğiniz dersin numarasını seçin: ");
                if (int.TryParse(Console.ReadLine(), out int courseIndex) && courseIndex > 0 && courseIndex <= availableCourses.Count)
                {
                    var course = availableCourses[courseIndex - 1];

                    course.Instructor = instructor;
                    Console.WriteLine($"Ders \"{course.CourseName}\" {instructor.FirstName} {instructor.LastName} adlı öğretim görevlisine başarıyla atandı.");
                }
                else
                {
                    Console.WriteLine("Geçersiz ders seçimi.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz öğretim görevlisi seçimi.");
            }
        }

        static void ListInstructors()
        {
            if (instructors.Count == 0)
            {
                Console.WriteLine("Mevcut öğretim görevlisi yok! Lütfen önce öğretim görevlisi ekleyin.");
                return;
            }

            Console.WriteLine("\nMevcut Öğretim Görevlileri:");
            foreach (var instructor in instructors)
            {
                Console.WriteLine($"{instructor.GetFullName()}");

                var assignedCourses = courses.FindAll(c => c.Instructor == instructor);
                if (assignedCourses.Count > 0)
                {
                    Console.WriteLine("   Verdiği Dersler:");
                    foreach (var course in assignedCourses)
                    {
                        Console.WriteLine($"   - {course.CourseName} (Kredi: {course.Credit})");
                    }
                }
                else
                {
                    Console.WriteLine("   Verdiği Dersler: Yok");
                }
            }
        }

        static void RemoveInstructor()
        {
            if (instructors.Count == 0)
            {
                Console.WriteLine("Mevcut öğretim görevlisi yok! Lütfen önce öğretim görevlisi ekleyin.");
                return;
            }

            Console.WriteLine("\nMevcut Öğretim Görevlileri:");
            for (int i = 0; i < instructors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {instructors[i].GetFullName()}");
            }

            Console.Write("Silmek istediğiniz öğretim görevlisinin numarasını seçin: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= instructors.Count)
            {
                instructors.RemoveAt(index - 1);
                Console.WriteLine("Öğretim görevlisi başarıyla silindi.");
            }
            else
            {
                Console.WriteLine("Geçersiz seçim.");
            }
        }

        static void CourseManagementMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Ders Yönetimi ===");
                Console.WriteLine("1- Ders Ekle");
                Console.WriteLine("2- Ders Sil");
                Console.WriteLine("3- Ders Bilgileri");
                Console.WriteLine("0- Geri Dön");
                Console.Write("Seçiminiz: ");

                string choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        AddCourse();
                        break;
                    case "2":
                        RemoveCourse();
                        break;
                    case "3":
                        ListCourseDetails();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                        break;
                }
            }
        }

        static void AddCourse()
        {
            Console.Write("Ders Adı: ");
            string courseName = Console.ReadLine() ?? string.Empty;

            Console.Write("Kredi: ");
            if (!int.TryParse(Console.ReadLine(), out int credit))
            {
                Console.WriteLine("Geçersiz kredi değeri. Ders eklenemedi.");
                return;
            }

            var course = new Courses
            {
                CourseName = courseName,
                Credit = credit
            };

            courses.Add(course);
            Console.WriteLine("Ders başarıyla eklendi.");
        }

        static void RemoveCourse()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("Mevcut ders yok! Lütfen önce ders ekleyin.");
                return;
            }

            Console.WriteLine("\nMevcut Dersler:");
            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {courses[i].CourseName}");
            }

            Console.Write("Silmek istediğiniz dersin numarasını seçin: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= courses.Count)
            {
                courses.RemoveAt(index - 1);
                Console.WriteLine("Ders başarıyla silindi.");
            }
            else
            {
                Console.WriteLine("Geçersiz seçim.");
            }
        }

        static void ListCourseDetails()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("Mevcut ders yok! Lütfen önce ders ekleyin.");
                return;
            }

            Console.WriteLine("\nMevcut Dersler:");
            for (int i = 0; i < courses.Count; i++)
            {
                var course = courses[i];
                Console.WriteLine($"{i + 1}. Ders Adı: {course.CourseName} - Kredi: {course.Credit}");
                if (course.Instructor != null)
                {
                    Console.WriteLine($"   Öğretim Görevlisi: {course.Instructor.FirstName} {course.Instructor.LastName}");
                }
                else
                {
                    Console.WriteLine("   Öğretim Görevlisi: Atanmamış");
                }
                Console.WriteLine($"   Kayıtlı Öğrenci Sayısı: {course.EnrolledStudents.Count}");
            }
        }
    }
}