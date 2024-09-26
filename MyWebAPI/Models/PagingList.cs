using System.Security.Cryptography;

namespace MyWebAPI.Models
{
    public class PagingList<T> :List<T>
    {
        //có nghĩa là PagingList<T> thực sự là một danh sách kiểu generic (List<T>), nhưng nó có thêm các thuộc tính và phương thức để hỗ trợ việc phân trang
        //Kiểu T là kiểu dữ liệu của các phần tử mà danh sách này chứa.Có thể là bất kỳ kiểu dữ liệu nào
        public int PageIndex { set; get; } //chỉ số của trang hiện tại.
        public int TotalPage { set; get; } //tổng số trang có sẵn dựa trên tổng số phần tử và kích thước của mỗi trang
        //Phương thức này tính toán tổng số trang dựa trên count và pageSize, sau đó thêm các phần tử vào danh sách PagingList<T>
        public PagingList(List<T> items,int count,int pageIndex, int pageSize) 
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count/(double)pageSize);
            AddRange(items);
        }
        //Phương thức này là nơi thực hiện việc phân trang từ một nguồn dữ liệu
        public static PagingList<T> Create(IQueryable<T> source,int pageIndex,int pageSize) // dùng static vì k cần khởi tạo
        {
            //IQueryable<T> không thực thi truy vấn ngay lập tức(truy vấn chỉ thực sự được thực thi khi bạn gọi một phương thức).
            //Cho phép bạn tạo ra các truy vấn động. 
            var count =source.Count();// Số lượng phần tử tổng cộng trong nguồn dữ liệu
            var items = source.Skip((pageIndex-1)*pageSize)// Bỏ qua các phần tử của trang trước
                                .Take(pageSize).ToList(); // Lấy số lượng phần tử theo `pageSize`

            return new PagingList<T>(items,count,pageIndex,pageSize);
            //trả về một đối tượng PagingList<T> chứa danh sách phần tử đã phân trang, cùng với thông tin về số trang và trang hiện tại.
        }
    }
}
