using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursController: Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context=context;
        }
        
           public async Task<IActionResult> Index()
        {
            var kurslar= await  _context.Kurslar.Include(o=>o.Ogretmen).ToListAsync();
            return View(kurslar);
        }

        

           public async Task <IActionResult> Create()
        {
          ViewBag.Ogretmenler=new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
            return View();
        }

        [HttpPost]
          public async Task<IActionResult> Create(KursViewModel model)
     {
      if(ModelState.IsValid){

        _context.Kurslar.Add(new Kurs(){KursId=model.KursId,Baslık=model.Baslık,OgretmenId=model.OgretmenId});
            await _context.SaveChangesAsync();
      await _context.SaveChangesAsync();
      return RedirectToAction("Index","Kurs");
      } 
        ViewBag.Ogretmenler=new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
      return View(model);
     
    }

    
       public async Task<IActionResult> Edit(int? id)
    {
      if(id==null){
        return NotFound();
      }

  /*  var kurs=await _context
    .Kurslar.FindAsync(id); //bunun yerine firsordefult olabilir. findAsync(sadece keyi alır) null değer gönderebilir onunda kontrolunu yapmak gerekir
   //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.OgrenciId == id);*/
   var kurs=await _context //Icollectıondan sonra düzenleme
    .Kurslar
    .Include(k=>k.KursKayitlari)
    .ThenInclude(k=>k.Ogrenci)
    .Select(k=> new KursViewModel{
      KursId=k.KursId,
      Baslık=k.Baslık,
      OgretmenId=k.OgretmenId,
      KursKayitlari= k.KursKayitlari
    })
    .FirstOrDefaultAsync(k=>k.KursId==id);
  if(kurs==null){
    return NotFound();
  }
   ViewBag.Ogretmenler=new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
  return View(kurs);

    }


     [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, KursViewModel model) // sadece Baslık alıyoruz
{
   if(id !=model.KursId)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(new Kurs(){KursId=model.KursId,Baslık=model.Baslık,OgretmenId=model.OgretmenId});
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Kurslar.Any(o => o.KursId == model.KursId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction("Index");
    }
  ViewBag.Ogretmenler=new SelectList(await _context.Ogretmenler.ToListAsync(),"OgretmenId","AdSoyad");
    return View(model); // hata varsa, kullanıcıya formu tekrar gösteriyoruz
}


    [HttpGet]
  public async Task<IActionResult> Delete(int? id){

    if(id==null)
    {
      return NotFound();
    }
    var Kurs=await _context.Kurslar.FindAsync(id);

    if (Kurs==null){
      return NotFound();
    }
  return View(Kurs);
  }
  //model binding yazarak microsftdan inceleyebilirsin[fromformdakinialıyor]
  [HttpPost]
  public async Task<IActionResult> Delete ([FromForm]int id )
  {
    var Kurs= await _context.Kurslar.FindAsync(id);
    if(Kurs==null)
    {
      return NotFound();
    }
    _context.Kurslar.Remove(Kurs);
    await _context.SaveChangesAsync();
    return RedirectToAction("Index");
  }



    }
}