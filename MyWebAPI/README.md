# HTTP Status Codes

## 1xx - Informational Response
- **100-199**: Phản hồi thông tin từ server.

## 2xx - Success
- **200 OK**: Yêu cầu thành công. Thường trả về dữ liệu API hoặc trang web.
- **201 Created**: Tạo mới thành công. Thường sử dụng trong POST request.
- **204 No Content**: Yêu cầu thành công nhưng không có nội dung trả về. Ví dụ: xóa mục khỏi danh sách.

## 3xx - Redirection
- **300-399**: Chuyển hướng. Client cần thực hiện thêm hành động để hoàn tất yêu cầu (thường là chuyển hướng URL).

## 4xx - Client Error
- **400 Bad Request**: Yêu cầu sai cú pháp hoặc dữ liệu không hợp lệ.
  - *Ví dụ*: Gửi JSON sai định dạng hoặc thiếu trường bắt buộc.
- **401 Unauthorized**: Thiếu thông tin xác thực hợp lệ.
  - *Ví dụ*: Truy cập API mà không có token hợp lệ.
- **403 Forbidden**: Yêu cầu bị từ chối dù đã xác thực.
  - *Ví dụ*: Người dùng không có quyền truy cập tài nguyên.
- **404 Not Found**: Tài nguyên không tồn tại tại URL yêu cầu.
- **405 Method Not Allowed**: Phương thức không được phép.
  - *Ví dụ*: Gửi POST đến URL chỉ chấp nhận GET.
- **422 Unprocessable Entity**: Dữ liệu hợp lệ về cấu trúc nhưng không thể xử lý được.
  - *Ví dụ*: Gửi giá trị âm vào trường chỉ nhận giá trị dương.

## 5xx - Server Error
- **500-599**: Server gặp lỗi khi xử lý yêu cầu.

# Repository Pattern

**Repository Pattern** (Mẫu Repository) được sử dụng trong dự án này nhằm cung cấp sự tách biệt rõ ràng giữa logic truy cập dữ liệu và logic nghiệp vụ. 
Mẫu thiết kế này giúp trừu tượng hóa cách dữ liệu được lưu trữ và truy xuất, làm cho mã nguồn dễ bảo trì và kiểm thử hơn.

## Lợi ích:
- **Tách biệt các mối quan tâm**: Lớp truy cập dữ liệu được tách khỏi logic nghiệp vụ, giúp mã dễ bảo trì và sửa đổi.
- **Dễ kiểm thử**: Sử dụng Repository Pattern giúp dễ dàng mock (giả lập) lớp truy cập dữ liệu cho kiểm thử đơn vị.
- **Linh hoạt**: Dễ dàng thay đổi cơ chế lưu trữ dữ liệu (ví dụ: từ SQL sang NoSQL) mà không ảnh hưởng đến logic nghiệp vụ.


# JWT
## Thông tin người dùng
## roles
## expire time