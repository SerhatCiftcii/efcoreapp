using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace efcoreApp.Data
{


    public class Ogrenci 
{
    // id=>priamry id yazınca key olarak alır OgrenciIdyide çözer ama OgrenciKimliğq Key koy her durumda konulabilir
    [Key]
    public int OgrenciId { get; set; }

    public string? OgrenciAd { get; set; }

     public string? OgrenciSoyad { get; set; }

    public string? AdSoyad { 
        get  
        {
         return this.OgrenciAd +" " + this.OgrenciSoyad;
    }
    }
     public string? Eposta { get; set; }

     public string? Telefon { get; set; }
     public ICollection <KursKayit> KursKayitlari { get; set; }=new List <KursKayit> (); //new kısmı null olmaması için yapıldı




    }
}