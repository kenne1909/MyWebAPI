using MyWebAPI.Data;
using MyWebAPI.Models;

namespace MyWebAPI.Services
{
    public class LoaiRepository : ILoaiRepository
    {
        private readonly MyDbContext _context;

        public LoaiRepository(MyDbContext context) 
        {
            _context = context; 
        }
        public LoaiVM Add(LoaiModel loai)
        {
            var _loai = new Loai
            {
                TenLoai= loai.TenLoai,
            };
            _context.Add(_loai);
            _context.SaveChanges();
            return new LoaiVM
            {
                MaLoai=_loai.MaLoai,
                TenLoai=_loai.TenLoai,
            };
        }

        public void Delete(int id)
        {
            var loai = _context.loais.FirstOrDefault(lo => lo.MaLoai == id);
            if (loai != null)
            {
                _context.Remove(loai);
                _context.SaveChanges();
            }
        }

        public List<LoaiVM> GetAll()
        {
            var loais = _context.loais.Select(lo => new LoaiVM
            {
                MaLoai =  lo.MaLoai,
                TenLoai = lo.TenLoai,
            });
            return loais.ToList();
        }

        public LoaiVM GetById(int id)
        {
            var loai = _context.loais.FirstOrDefault(lo=> lo.MaLoai == id);
            if (loai != null)
            {
                return new LoaiVM
                {
                    MaLoai = loai.MaLoai,
                    TenLoai = loai.TenLoai,
                };
            }
            return null;
        }

        public void Update(LoaiVM loai)
        {
            var _loai = _context.loais.SingleOrDefault(lo => lo.MaLoai == loai.MaLoai);
            if(_loai != null)
            {
                _loai.TenLoai = loai.TenLoai;
                _context.SaveChanges();
            }    
        }
    }
}
