# Ứng dụng Paint - Windows Forms (C#)

Ứng dụng vẽ trên máy tính được xây dựng bằng **C# .NET Windows Forms**, mô phỏng các chức năng cơ bản của Microsoft Paint. Người dùng có thể vẽ tự do, vẽ các hình hình học, chọn màu sắc, thay đổi kích thước bút và tương tác trực tiếp trên canvas thông qua chuột.

---

## Tính năng

- **Vẽ tự do** — vẽ nét bút liên tục theo chuyển động chuột
- **Các hình hình học** — đường thẳng, hình chữ nhật, hình elip, ...
- **Bảng màu** — chọn màu vẽ tùy chỉnh
- **Điều chỉnh kích thước bút**
- **Tẩy (Eraser)** — xóa vùng vẽ
- **Canvas tương tác** — xử lý sự kiện chuột (MouseDown, MouseMove, MouseUp)
- **File thực thi sẵn** — chạy trực tiếp bằng file `.exe` mà không cần build lại

---

## Công nghệ sử dụng

| Thành phần | Chi tiết |
|---|---|
| Ngôn ngữ | C# |
| Framework | .NET Windows Forms |
| Thư viện đồ họa | GDI+ (System.Drawing) |
| IDE khuyến nghị | Visual Studio |

---

## Cấu trúc thư mục

```
winforms-paint-app
├── 22133044_TranThiKimPhuong/      # Thư mục chứa toàn bộ source code
│   ├── Form1.cs                    # Form chính - logic vẽ và xử lý sự kiện
│   ├── Form1.Designer.cs           # Giao diện được tạo tự động bởi Designer
│   └── ...                        # Các file project khác (.csproj, resources, ...)
└── 22133044_TranThiKimPhuong.exe  # File thực thi - chạy trực tiếp không cần build
```

---

## Hướng dẫn sử dụng

### Chạy nhanh (không cần cài đặt)

Tải file `22133044_TranThiKimPhuong.exe` về và chạy trực tiếp.

> Yêu cầu: máy tính Windows đã cài .NET Runtime tương thích.

### Chạy từ source code

1. Clone repository:

   ```bash
   git clone https://github.com/ventdejanvier/winforms-paint-app
   cd winforms-paint-app
   ```

2. Mở file `.csproj` trong thư mục `22133044_TranThiKimPhuong/` bằng **Visual Studio**.

3. Build và chạy dự án bằng phím `F5` hoặc nút **Start**.

---

## Kiến thức áp dụng

- Lập trình hướng sự kiện (Event-driven programming) với Windows Forms
- Xử lý đồ họa 2D bằng GDI+ (`Graphics`, `Pen`, `Brush`, `Bitmap`)
- Quản lý trạng thái ứng dụng (công cụ đang chọn, màu sắc, kích thước bút)
- Thiết kế giao diện người dùng với WinForms Designer

---

## Yêu cầu hệ thống

- Hệ điều hành: Windows 10 / 11
- .NET Framework hoặc .NET Runtime (phiên bản tương thích với project)
- Visual Studio 2019 trở lên  

---
 
