using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
   public class KursKayitController :Controller
   {
    private readonly DataContext _context;
    public KursKayitController(DataContext context)
    {
        _context=context;
    }
    public async Task<IActionResult>Index()
    {
        var KursKayit = await _context.KursKayitlari
        .Include(k => k.Ogrenci)    // Ogrenci'yi dahil et
        .Include(k => k.Kurs)       // Kurs'u dahil et
        .ToListAsync();
    
      return View(KursKayit);
    }

    public async Task<IActionResult> Create()
    {

        ViewBag.Ogrenciler= new SelectList (await  _context.Ogrenciler.ToListAsync(),"OgrenciId","AdSoyad");//select kutsunun anlıycağı bir tipe çevircez awaitden önce new SelectList diycez

  ViewBag.Kurslar= new SelectList (await  _context.Kurslar.ToListAsync(),"KursId","Baslık");
  return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(KursKayit model)
    {
          
          model.KayitTarihi=DateTime.Now;
          _context.KursKayitlari.Add(model);
          await _context.SaveChangesAsync();
        return RedirectToAction("Index");;

   }

   // GET: KursKayit/Delete/5
[HttpGet]
public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var kursKayit = await _context.KursKayitlari
                                   .Include(k => k.Kurs)  // İlgili kursu da dahil ediyoruz.
                                   .FirstOrDefaultAsync(k => k.KayitId == id);

    if (kursKayit == null)
    {
        return NotFound();
    }

    return View(kursKayit);  // KursKayit modelini view'a gönder
}

   // POST: KursKayit/Delete/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(int id)
{
    var kursKayit = await _context.KursKayitlari.FindAsync(id);
    if (kursKayit == null)
    {
        return NotFound();
    }

    _context.KursKayitlari.Remove(kursKayit);  // Kurs kaydını silme işlemi
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));  // Silme işleminden sonra kurs kayıtları sayfasına yönlendir
}

[HttpGet]
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var kursKayit = await _context.KursKayitlari
                                  .Include(k => k.Ogrenci)
                                  .Include(k => k.Kurs)
                                  .FirstOrDefaultAsync(k => k.KayitId == id);

    if (kursKayit == null)
    {
        return NotFound();
    }

    return View(kursKayit);
}


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, KursKayit kursKayit)
{
    if (id != kursKayit.KayitId)
    {
        return NotFound();
    }

    var existingKayit = await _context.KursKayitlari.FindAsync(id);
    if (existingKayit == null)
    {
        return NotFound();
    }

    // Sadece düzenlenebilir alanı güncelle
    existingKayit.KayitTarihi = kursKayit.KayitTarihi;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!KursKayitExists(kursKayit.KayitId))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return RedirectToAction(nameof(Index));
}

private bool KursKayitExists(int id)
{
    return _context.KursKayitlari.Any(e => e.KayitId == id);
}




}


}

