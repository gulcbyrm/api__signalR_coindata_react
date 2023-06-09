

using GetFromApiAddDB.Models;
using Microsoft.EntityFrameworkCore;
  
namespace GetFromApiAddDB.Services
{
    //db crud islemleri
    public class CurrencyService
    {
        //degiskenler
        private readonly CryptoGulcinContext _context;
         //constructor
        public CurrencyService(CryptoGulcinContext cryptoGulcinContext)
        {
            _context = cryptoGulcinContext;
        }
        //db ye ekleme
        public async Task Create(GetFromApiAddDB.Models.Currency model)
        {
            if (model == null)
            {
                throw new Exception("Entity set 'CryptoGulcinContext.Currencies'  is null.");
            }
            _context.Currencies.Add(model);
            Console.WriteLine("db ye eklenmistir");
            await _context.SaveChangesAsync();
            
        }

     
        
        
        
         
        
        /*Include(c => c.CurrencyActions) ifadesi, "Currencies" tablosundaki her bir öğe için
         * "CurrencyActions" adlı ilişkili bir tabloyu da yüklemek için kullanılır. Bu,
         * "CurrencyActions" tablosundaki verilerin, "Currencies" tablosundaki her bir 
         * öğeyle ilişkilendirilmiş olarak alınmasını sağlar. Bu işlem, ilişkili verileri
         * (örneğin, döviz hareketleri gibi) birlikte almak ve veritabanı sorguları sırasında 
         * daha fazla veritabanı erişimi yapmaktan kaçınmak için kullanışlıdır.*/
        /*ToListAsync() yöntemi, veritabanı sorgusunu asenkron olarak çalıştırır ve sorgunun sonucunu bir
         * liste olarak döndürür. Bu, sorgunun veritabanında gerçekleştirilmesini ve 
         * sonuçların bellekte bir liste olarak alınmasını sağlar.Bu işlem, veritabanı işlemlerini 
         * optimize etmek, veri alışverişini kolaylaştırmak ve veritabanı sorgularını etkin bir 
         * şekilde kullanmak için kullanılan yaygın bir yapıdır.*/
        //include sayesinde ornegin btc ve btc nin alt listesi goruntulenir
        public async Task<List<GetFromApiAddDB.Models.Currency>> GetAll()
        {
            return await _context.Currencies.Include(c => c.CurrencyActions).ToListAsync();
        }
        /*FirstOrDefaultAsync yöntemi, veritabanından ilgili sorguyu asenkron olarak
         * çalıştırır ve sorgunun sonucunda belirtilen koşulu sağlayan ilk öğeyi döndürür.
         * Eğer koşulu sağlayan bir öğe bulunmazsa,
         * varsayılan değeri (null veya default(Currency)) döndürür.*/
        //id ye gore getir
        public async Task<GetFromApiAddDB.Models.Currency> GetById(int id)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.Id == id);
        }

        //liste halinde eklemeyi saglar
        public async Task BulkCreate(List<GetFromApiAddDB.Models.Currency> model)
        {
            if (model == null)
            {
                throw new Exception("Entity set 'CryptoGulcinContext.Currencies'  is null.");
            }
            _context.Currencies.AddRange(model);
            await _context.SaveChangesAsync();
        }
    }
}
