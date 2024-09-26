using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Data;
using MyWebAPI.Models;

namespace MyWebAPI.Services
{
    public class HanghoaRepository : IHanghoaRepository
    {
        private readonly MyDbContext _Context;
        public static int PAGE_SIZE { set; get; } = 3;

        public HanghoaRepository(MyDbContext context) {
            _Context = context;
        }
        public HangHoaModel Add(HanghoaMD model)
        {
            //var loai = _Context.loais.FirstOrDefault(lo => lo.TenLoai == model.TenLoai);
            var hanghoa = new HangHoa
            {
                MaHH= Guid.NewGuid(),
                TenHH = model.TenHH,
                Mota = model.Mota,
                DonGia = model.DonGia,
                GiamGia = model.GiamGia,
                MaLoai = model.MaLoai
            };
            _Context.hangHoas.Add(hanghoa);
            _Context.SaveChanges();
            return new HangHoaModel
            {
                MaHH = hanghoa.MaHH,
                TenHH = hanghoa.TenHH,
                Mota = hanghoa.Mota,
                DonGia = hanghoa.DonGia,
                GiamGia = hanghoa.GiamGia,
                TenLoai = hanghoa.Loai?.TenLoai,
            };
        }

        public void Delete(string id)
        {
            var hanghoa = _Context.hangHoas.FirstOrDefault(hh => hh.MaHH == Guid.Parse(id));
            if (hanghoa != null)
            {
                _Context.hangHoas.Remove(hanghoa);
                _Context.SaveChanges();
            }
        }

        public List<HangHoaModel> GetAll(string search, double? from, double? to, string? sortBy, int page = 1)
        {
            var hanghoas = _Context.hangHoas
                           .Include(hh => hh.Loai)
                           .AsQueryable();
            //Phương thức chuyển đổi từ IEnumerable sang IQueryable, giúp thực hiện các truy vấn LINQ trên dữ liệu.

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                hanghoas = hanghoas.Where(hh => hh.TenHH!.Contains(search));
            }
            if(from.HasValue)
            {
                hanghoas = hanghoas.Where(hh => hh.DonGia >= from);
            }
            if (to.HasValue)
            {
                hanghoas=hanghoas.Where(hh => hh.DonGia <= to);
            }
            #endregion

            #region Sorting
            //default sort by name
            hanghoas = hanghoas.OrderBy(hh => hh.TenHH);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "tenhh_desc": 
                        hanghoas = hanghoas.OrderByDescending(hh => hh.TenHH);
                        break;
                    case "gia_asc":
                        hanghoas = hanghoas.OrderBy(hh => hh.DonGia);
                        break;
                    case "gia_desc":
                        hanghoas = hanghoas.OrderByDescending(hh => hh.DonGia);
                        break;
                }
            }
            #endregion

            //#region paging
            //hanghoas= hanghoas.Skip((page - 1)*PAGE_SIZE).Take(PAGE_SIZE);
            //#endregion

            //var results = hanghoas.Select(hh => new HangHoaModel
            //                        {
            //                            MaHH = hh.MaHH,
            //                            TenHH = hh.TenHH,
            //                            Mota = hh.Mota,
            //                            DonGia = hh.DonGia,
            //                            GiamGia = hh.GiamGia,
            //                            TenLoai = hh.Loai!.TenLoai
            //                        });
            //return results.ToList();

            var results = PagingList<HangHoa>.Create(hanghoas, page, PAGE_SIZE);
            var cout  = results.TotalPage;
            Console.WriteLine(cout);
            var index =results.PageIndex;
            Console.WriteLine(index);

            return results.Select(hh => new HangHoaModel
            {
                MaHH = hh.MaHH,
                TenHH = hh.TenHH,
                Mota = hh.Mota,
                DonGia = hh.DonGia,
                GiamGia = hh.GiamGia,
                TenLoai = hh.Loai?.TenLoai
            }).ToList();
        }

        public HangHoaModel GetById(string id)
        {
            var hanghoa = _Context.hangHoas.Include(hh => hh.Loai)
                            .FirstOrDefault(hh => hh.MaHH == Guid.Parse(id));
            if(hanghoa == null)
            {
                return null;
            }
            return new HangHoaModel
            {
                MaHH = hanghoa.MaHH,
                TenHH = hanghoa.TenHH,
                Mota = hanghoa.Mota,
                DonGia = hanghoa.DonGia,
                GiamGia = hanghoa.GiamGia,
                TenLoai = hanghoa.Loai?.TenLoai
            };
        }

        public void Update(string id, HanghoaMD model)
        {
            var hanghoa= _Context.hangHoas
                .Include(hh=>hh.Loai)        
                .FirstOrDefault(hh => hh.MaHH == Guid.Parse(id));

            if(hanghoa != null)
            {
                hanghoa.TenHH = model.TenHH;
                hanghoa.Mota = model.Mota;
                hanghoa.DonGia = model.DonGia;
                hanghoa.GiamGia = model.GiamGia;
                hanghoa.MaLoai = model.MaLoai;
                _Context.SaveChanges();
            }                        
        }
    }
}
