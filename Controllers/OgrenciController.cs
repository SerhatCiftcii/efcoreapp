using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace efcoreApp.Controllers
{
    public class OgrenciController :Controller
    {
     // var context=new DataContext(); //bunun yerine  injection yöntemi arayıcıyla olur consrtractur oluşturrarak yapcaz bu artık kullanılmıyor?  
      private readonly DataContext _context;
      public OgrenciController(DataContext context) 
      {
        _context=context;
      }

      public async Task <IActionResult> Index(){

        var ogrenciler= await _context.Ogrenciler.ToListAsync();
      

        return View(ogrenciler);
      }

        public IActionResult Create()
        {
            return View();
        }

[HttpPost]
          public async Task<IActionResult> Create(Ogrenci model)
     {

     _context.Ogrenciler.Add(model);
      await _context.SaveChangesAsync();
      return RedirectToAction("Index","Ogrenci");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
      if(id==null){
        return NotFound();
      }

    //var ogr=await _context.Ogrenciler.FindAsync(id); //bunun yerine firsordefult olabilir. findAsync(sadece keyi alır) null değer gönderebilir onunda kontrolunu yapmak gerekir
   //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.OgrenciId == id);

   var ogr=await _context
                  .Ogrenciler
                  .Include(o=>o.KursKayitlari)
                  .ThenInclude(o=>o.Kurs)//ogrenci yuklendikten sonra kursa eriş
                  .FirstOrDefaultAsync(o=>o.OgrenciId ==id);
  if(ogr==null){
    return NotFound();
  }
  return View(ogr);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Ogrenci model)
    {

      if(id !=model.OgrenciId){
        return NotFound();
      }
    if(ModelState.IsValid)
    {
        try
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException) //yukarda hata varsa catche düşer 
        {
          if(!_context.Ogrenciler.Any(o=> o.OgrenciId==model.OgrenciId)) //veri tabanında kayıt olmadığı durumda 
        {
            return NotFound();
        }
        else{
          throw;
        }

        }
          return RedirectToAction("Index");

    }
  return View(model);

    }
    
  [HttpGet]
  public async Task<IActionResult> Delete(int? id){

    if(id==null)
    {
      return NotFound();
    }
    var öğrenci=await _context.Ogrenciler.FindAsync(id);

    if (öğrenci==null){
      return NotFound();
    }
  return View(öğrenci);
  }
  //model binding yazarak microsftdan inceleyebilirsin[fromformdakinialıyor]
  [HttpPost]
  public async Task<IActionResult> Delete ([FromForm]int id )
  {
    var ogrenci= await _context.Ogrenciler.FindAsync(id);
    if(ogrenci==null)
    {
      return NotFound();
    }
    _context.Ogrenciler.Remove(ogrenci);
    await _context.SaveChangesAsync();
    return RedirectToAction("Index");
  }


    }
}