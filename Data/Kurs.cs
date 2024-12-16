using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace efcoreApp.Data
{
    public class Kurs
    {
        //KursIdyi entty priamry olarak alır Key olarak belirtilmese bile ıd yide öyle ama başka şeylerde belirt keyi
        [Key]
        public int KursId { get; set; }
        [Required(ErrorMessage = "Başlık alanı gereklidir.")]
        public string? Baslık { get; set; }


        public int OgretmenId { get; set; }// alltaki bi fark yok aslında
        public Ogretmen Ogretmen { get; set; }= null!;

        public ICollection <KursKayit> KursKayitlari { get; set; }=new List<KursKayit>(); // aynı öğrencileride olduğu gibi erişcez



    }
}