using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace efcoreApp.Data
{
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }

        public int OgrenciId { get; set; }
  [ForeignKey("OgrenciId")]
      public Ogrenci Ogrenci { get; set; } = null!; 

         public int KursId { get; set; }
    [ForeignKey("KursId")]
         public Kurs Kurs { get; set; } =null!;

         public DateTime KayitTarihi { get; set; }

  //1=> 5,8 :1numaralı kayıt ve 5 numaralı ıdyie sahip olan öpğrenci 8 no ya kayıt oldu
    }
}