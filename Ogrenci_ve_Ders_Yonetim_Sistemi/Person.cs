using Ogrenci_ve_Ders_Yonetim_Sistemi.Interfaces;

namespace Ogrenci_ve_Ders_Yonetim_Sistemi.Models
{
    public abstract class Person : IPerson
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public virtual string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
