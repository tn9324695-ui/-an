# L'Atelier Fine Dining Web Constitution

## Core Principles

### I. Stitch Design Fidelity & Design-First Workflow (NON-NEGOTIABLE)
Toàn bộ giao diện và trải nghiệm người dùng của ứng dụng web được xây dựng bám sát 100% gói thiết kế **L'Atelier Fine Dining (`Atelier Espresso`)** trong thư mục `design-system/stitch-latelier/` và `design-system/huong-viet-restaurant/MASTER.md`. Không tự ý thay đổi bảng màu, font chữ hay bố cục đã được duyệt.

### II. Atelier Espresso Design System & GSAP Motion
- **Bảng màu (`Atelier Espresso`)**:
  - `primary`: `#130803` (Deep Roasted Espresso)
  - `primary-container`: `#2C1E16`
  - `secondary`: `#765844` (Warm Hazelnut & Aged Cognac)
  - `surface` / `background`: `#FCF9F4` (Warm Porcelain Cream)
  - `surface-container-low`: `#F6F3EE` (Raw Linen)
  - `outline-variant`: `#D2C4BD` (1px hairline dividers)
- **Typography & Icons**: `Playfair Display` (Headings) + `DM Sans` (Body & Tracked Uppercase Labels) + `Material Symbols Outlined` + Tailwind CSS config chuẩn từ thiết kế Stitch.
- **Chuyển động**: Tích hợp thư viện **GSAP 3.12.5 + ScrollTrigger** (`wwwroot/lib/gsap/`) với chuyển động tinh tế (Quiet Luxury), tôn trọng `prefers-reduced-motion`.

### III. Complete 6-Page Client Architecture + Admin Portal
Hệ thống bao gồm trọn vẹn các trang theo thiết kế Stitch kết hợp cùng cơ sở dữ liệu động:
1. **Trang Chủ (`Home/Index`)**: Hero Banner, Triết lý ẩm thực (giới hạn 14 bàn/tối), Carousel Món đặc trưng theo mùa, Thực đơn nếm thử 7 món (Tasting Menu), Không gian & Đặt bàn.
2. **Thực Đơn Đương Đại (`Home/Menu`)**: Banner Món ký danh, Thanh lọc danh mục Sticky (`Tất Cả`, `Tasting Menu`, `Khai Vị`, `Món Chính`, `Tráng Miệng`, `Rượu Vang & Pairing`), Nhãn chế độ ăn (`[GF]`, `[N]`, `[V]`), Gói Degustation 5 món & 7 món, Danh sách món ăn động từ SQL Server.
3. **Câu Chuyện Thương Hiệu (`Home/Story`)**: Triết lý Bếp trưởng Alain Vũ, Lịch sử biệt thự di sản 128 Đồng Khởi từ 2018, Đội ngũ sáng tạo và Nguồn nguyên liệu bản địa.
4. **Đặt Bàn Trực Tuyến 6 Bước (`Reservation/Index`)**:
   - Bước 1: Chọn Ngày & Khung Giờ (Bữa Trưa 11:30–14:00 / Bữa Tối 17:30–22:30)
   - Bước 2: Số lượng thực khách (1 khách Solo, 2 khách Duo, 3-4 Gia đình, 5-8 Tiệc thân mật, 8+ VIP Salon)
   - Bước 3: Không gian ngồi mong muốn (Bàn Cạnh Cửa Sổ, Bàn Trung Tâm, Chef's Counter, Phòng Riêng VIP Atelier Salon)
   - Bước 4: Chọn trước Trải nghiệm ẩm thực (Tasting Menu Découverte 5 Món, Prestige 7 Món, hoặc À La Carte + tùy chọn Sommelier Grand Cru Wine Pairing)
   - Bước 5: Dịp đặc biệt & Lưu ý dị ứng thực phẩm
   - Bước 6: Thông tin liên hệ & Cột Tóm Tắt Đặt Bàn (Live Summary) lưu trực tiếp vào SQL Server.
5. **Liên Hệ & Vị Trí (`Home/Contact`)**: Thông tin biệt thự 128 Đồng Khởi, Giờ phục vụ, Đường dây Concierge và Biểu mẫu gửi yêu cầu tư vấn tiệc riêng/sự kiện lưu vào CSDL.
6. **Hội Viên Thượng Khách L'Atelier Privilège (`Account/Login` & `Account/Register`)**: Mô phỏng thẻ kim loại 3D `L'Atelier Privilège Elite` (`LA • 8899 • VIP • 01`), Đăng nhập bằng SĐT/Mã thẻ hoặc Email, Kích hoạt thẻ được trao tặng hoặc Đăng ký hội viên mới.
7. **Quản Trị Hệ Thống (`Admin/*`)**: Dashboard quản lý phiếu đặt bàn 6 bước, quản lý thực đơn/Tasting menu, quản lý bàn, hội viên và yêu cầu liên hệ.

### IV. Layered Architecture & Database Persistence
- **Nền tảng**: ASP.NET Core 8.0 MVC (`net8.0`), Entity Framework Core 8.0, Microsoft SQL Server (`.\SQLEXPRESS`, Database: `RestaurantWebDb`).
- **Tự động khởi tạo dữ liệu**: `DbInitializer` tự động tạo bảng và nạp sẵn toàn bộ món ăn chuẩn L'Atelier (Sò Điệp Hokkaido Áp Chảo, Cá Hồi Sa Pa Xông Khói, Thăn Nội Bò Wagyu Ủ Khô, Vịt Rừng Trùng Khánh, Tôm Hùm Nướng Bơ Tỏi Đen, Sô-cô-la Đen Bến Tre, Rượu Vang Grand Cru...), 4 khu vực bàn và tài khoản Hội viên/Admin mẫu.

**Version**: 2.0.0 | **Ratified**: 2026-11-22 | **Last Amended**: 2026-11-22
