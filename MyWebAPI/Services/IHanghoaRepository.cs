using MyWebAPI.Data;
using MyWebAPI.Models;

namespace MyWebAPI.Services
{
    public interface IHanghoaRepository
    {
        List<HangHoaModel> GetAll(string search,double? from, double? to, string? sortBy,int page=1);
        HangHoaModel GetById(string id);
        HangHoaModel Add(HanghoaMD model);
        void Update(string id, HanghoaMD model);
        void Delete(string id);
    }
}
