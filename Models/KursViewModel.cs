

using System.ComponentModel.DataAnnotations;
using efcoreApp.Data;

namespace efcoreApp.Models
{
    public class KursViewModel
    {
        //KursIdyi entty priamry olarak alır Key olarak belirtilmese bile ıd yide öyle ama başka şeylerde belirt keyi
        [Key]
        public int KursId { get; set; }
        [Required(ErrorMessage = "Başlık alanı gereklidir.")]
        public string? Baslık { get; set; }


        public int OgretmenId { get; set; }// alltaki bi fark yok aslında
        
         public ICollection <KursKayit> KursKayitlari { get; set; }=new List<KursKayit>(); // aynı öğrencileride olduğu gibi erişcez
    }
}