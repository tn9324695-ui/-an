using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Models;

namespace RestaurantWeb.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Kiểm tra nếu CSDL cũ chưa có bảng ContactInquiries hoặc cột FrenchSubtitle thì tái tạo lại schema L'Atelier mới
            try
            {
                context.Database.EnsureCreated();
                _ = context.ContactInquiries.Any();
                _ = context.MenuItems.Select(m => m.FrenchSubtitle).FirstOrDefault();
                // Nếu vẫn còn dữ liệu mẫu cũ ("Gỏi Ngó Sen") thì nâng cấp lên dữ liệu L'Atelier Fine Dining
                if (context.MenuItems.Any(m => m.Name.Contains("Gỏi Ngó Sen")))
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
            }
            catch
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            if (!context.AppUsers.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Title = "Ngài",
                        FullName = "Alain Vũ (Tổng Bếp Trưởng & Quản Trị)",
                        Email = "admin@latelier.vn",
                        Phone = "0901234567",
                        MemberCardCode = "LA-ADMIN-0001",
                        MemberTier = "Grand Élite",
                        ConciergePin = "123456",
                        PasswordHash = "123456",
                        Role = "Admin",
                        Address = "128 Đồng Khởi, Phường Bến Nghé, Quận 1, TP.HCM",
                        WinePreference = "Bordeaux Grand Cru Classé",
                        PreferredSeating = "Chef's Counter"
                    },
                    new AppUser
                    {
                        Title = "Bà",
                        FullName = "Marcella Trần (Head Sommelier & Concierge)",
                        Email = "concierge@latelier.vn",
                        Phone = "0912345678",
                        MemberCardCode = "LA-STAFF-0002",
                        MemberTier = "Grand Élite",
                        ConciergePin = "123456",
                        PasswordHash = "123456",
                        Role = "Staff",
                        Address = "128 Đồng Khởi, Quận 1, TP.HCM",
                        WinePreference = "Champagne Vintage & Prestige Cuvée",
                        PreferredSeating = "Bàn Trung Tâm Ấm Cúng"
                    },
                    new AppUser
                    {
                        Title = "Ông",
                        FullName = "Philippe Le Tran",
                        Email = "khachhang@latelier.vn",
                        Phone = "0987654321",
                        MemberCardCode = "LA-8899-VIP-01",
                        MemberTier = "Privilège Noir",
                        ConciergePin = "886999",
                        PasswordHash = "123456",
                        Role = "Customer",
                        DateOfBirth = new DateTime(1988, 10, 18),
                        DietaryPreferences = "Không Gluten (GF)",
                        WinePreference = "Burgundy Pinot Noir & Chardonnay",
                        PreferredSeating = "Phòng Riêng VIP Atelier Salon",
                        Address = "Biệt thự Thảo Điền, TP. Thủ Đức, TP.HCM"
                    }
                };
                context.AppUsers.AddRange(users);
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Khai Vị / Entrées", Description = "Đánh thức các giác quan bằng sự giao thoa giữa độ tươi giòn, chất béo ngậy thanh tao và tầng hương vị đất mẹ", IconCss = "starters", DisplayOrder = 1 },
                    new Category { Name = "Món Chính / Plats Principaux", Description = "Sự chỉn chu tuyệt đối về nhiệt độ, thời gian ủ thịt và kỹ nghệ tạo nước xốt nền Pháp phức hợp", IconCss = "mains", DisplayOrder = 2 },
                    new Category { Name = "Tráng Miệng / Les Desserts", Description = "Bản hòa tấu ngọt ngào từ chocolate thủ công Việt Nam, hương vani Bourbon Madagascar và trái cây mùa màng", IconCss = "desserts", DisplayOrder = 3 },
                    new Category { Name = "Hầm Rượu / Sommelier Selection", Description = "Hơn 350 nhãn rượu vang danh giá từ Bordeaux, Bourgogne, thung lũng Loire được kiểm soát ở 14°C", IconCss = "wines", DisplayOrder = 4 }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.MenuItems.Any())
            {
                var cats = context.Categories.OrderBy(c => c.DisplayOrder).ToList();
                int cStarters = cats[0].Id;
                int cMains = cats[1].Id;
                int cDesserts = cats[2].Id;
                int cWines = cats[3].Id;

                var items = new List<MenuItem>
                {
                    // KHAI VỊ (STARTERS)
                    new MenuItem
                    {
                        Name = "Sò Điệp Áp Chảo & Nhụy Hoa Nghệ Tây",
                        FrenchSubtitle = "Coquilles Saint-Jacques au Safran",
                        Description = "Sò điệp cào tay vùng biển lạnh Hokkaido áp chảo vàng đều hai mặt, hòa cùng nhụy hoa nghệ tây Iran, măng tây biển và bọt sữa dừa tươi sáp Bến Tre.",
                        Price = 680000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBEd5tRJkWJVuA4QePPkh9kh8S2beqryNbcmEcriDlLOxlIG0mg0cirqBsYI3n5v_AgpLw-cOCzPblRmgTd1lcTH9avTt9sFssIjig-gB7jMlkXjWDUtzGKnXJ09A97R4ILZVP4hZwrYGaWjDbh-UJBbLKRwq2NxzZ_cuhJCo2pJD4FcSOBCtw0aRchulUEiEGWpAdYTYvxnCzdHyi88PtNpsncx8iyEvEOeGiWHNvI",
                        DietaryTags = "GF",
                        WinePairingNote = "Gợi ý vang: Chablis Premier Cru",
                        IsFeatured = true,
                        PreparationTimeMinutes = 15,
                        CategoryId = cStarters
                    },
                    new MenuItem
                    {
                        Name = "Cá Hồi Sa Pa Xông Khói & Trứng Cá Tầm",
                        FrenchSubtitle = "Saumon de Sa Pa Fumé & Caviar",
                        Description = "Thịt cá hồi núi Sa Pa xông khói tự nhiên bằng gỗ sồi và vỏ bưởi khô, ăn kèm 10g trứng cá tầm Caviar Oscietra thượng hạng và kem chua thì là.",
                        Price = 720000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuB7w4DBkiKJVbiu9dERNLvfQa40cCPYW8b1WnBM569XM0f01W7cajwLUrPIy-wjcLUTAGwtp97u-ex2UfUb37zElnTR2f0r7JP5dmQ_7xFV0fA6I9F0EqT6Gx_PekWBdtIbfWie-1aLwLLVsRAdVW2rRN6ZoM_rfGVX7tb44VYnIPjJzhrH2vYMcTbRq2QrNMdYJ6sC-zryqFhaIiUjudwOQDykjL7McWZuUpzt5XSa",
                        DietaryTags = "GF",
                        WinePairingNote = "Gợi ý vang: Champagne Brut Réserve",
                        IsFeatured = true,
                        PreparationTimeMinutes = 12,
                        CategoryId = cStarters
                    },
                    new MenuItem
                    {
                        Name = "Terrine Gan Ngỗng & Quả Vả Rừng",
                        FrenchSubtitle = "Terrine de Foie Gras aux Figues",
                        Description = "Gan ngỗng béo vùng Périgord tẩm ướp cùng rượu Armagnac, nén lạnh theo công thức truyền thống, dùng kèm mứt sung rừng Tây Bắc và bánh mì brioche nướng bơ.",
                        Price = 590000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuD-GjpPV_NZtRhRZZSdEC06urh6thsV1riHfY7T3ePmQIhsS7LVZkkgjESz0YYdXwtvjc4KTJBDbf3AOKu6tdRizFV8gagEjwOEUzxtgjaLw28e6oDBeC3BX_jK94PE0n_g8tsuXGLOUegs-Abr_g0dE2FBn7AjE5BIKziPK1miDIkkeC9ZlGwRrXG4OCMzthwI_VScm3hVcxW95D02NNPZpP1MZprDw_2ooYgYwhhl",
                        DietaryTags = "Nuts",
                        WinePairingNote = "Gợi ý vang: Sauternes Château Guiraud",
                        IsFeatured = false,
                        PreparationTimeMinutes = 12,
                        CategoryId = cStarters
                    },

                    // MÓN CHÍNH (MAINS)
                    new MenuItem
                    {
                        Name = "Thăn Nội Bò Wagyu A5 Ủ Khô 28 Ngày",
                        FrenchSubtitle = "Filet de Wagyu A5 Maturé & Truffe Noire",
                        Description = "Phần thịt thăn lưng bò Wagyu A5 tỉnh Miyazaki được bảo quản trong buồng ủ muối Himalaya 28 ngày để cô đọng vị ngọt umami, phủ nấm kim cương Truffe Noire bào tươi và xốt tiêu rừng.",
                        Price = 1850000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuChdy7KCtEm3FJ7KBnkw7EyVK45LPSAvaIN71DIn3l-BcITBzLdO9JU5SQXPGdGgflZPSZK7CHGZWMD74Blo-e2rjIvhatz_qLuW9qpCB1idnO6X46CTOt79hiuz6uEaCLNdpz0r7n1eoxdgF18oKPHew3kitji5yzU_1fH2cj7sFDmZdI8RqZTcRDhdOYrnTQH9d9s-IEzTmiTGUnoQGkNpfCvL4owGgVBBi6zt5JR",
                        DietaryTags = "GF • Signature",
                        WinePairingNote = "Cặp đôi hoàn hảo: Pauillac Château Lynch-Bages 2015",
                        IsFeatured = true,
                        PreparationTimeMinutes = 20,
                        CategoryId = cMains
                    },
                    new MenuItem
                    {
                        Name = "Vịt Rừng Trùng Khánh Xốt Mận",
                        FrenchSubtitle = "Magret de Canard au Poivre Sauvage",
                        Description = "Ức vịt nuôi tự nhiên vùng non cao Trùng Khánh, hong khô da 5 ngày trước khi áp chảo giòn rụm. Dùng cùng purée bí đỏ nướng mật ong hoa nhãn và xốt mận rừng Tả Van.",
                        Price = 1250000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuB1-PCwuuujIRjV3ZxR7GYShKdU9WIP25R9SwzmvdyfhWCjz_frhm3JucqfaCJ2SaFer8MSdckdgv6JMXtKBBAuyOA1v2lsvDyX_2wfaDdcHueWe9XR7iaToRuVkBLaLkLyAwdWMuhrB57sI5zaAXeR5d_ldzwQB_UE6GYIgeq4YCdZBCdo9UUF0Xg_onftpevz6qQEA5HUR3tRXVYKK0LVvyTQrOom-OQ5JiOLb3CM",
                        DietaryTags = "GF",
                        WinePairingNote = "Rượu vang: Côtes du Rhône Syrah",
                        IsFeatured = true,
                        PreparationTimeMinutes = 20,
                        CategoryId = cMains
                    },
                    new MenuItem
                    {
                        Name = "Tôm Hùm Nướng Bơ Tỏi Đen Lý Sơn",
                        FrenchSubtitle = "Homard Bleu Rôti à l'Ail Noir",
                        Description = "Tôm hùm bông hảo hạng nướng bơ tỏi đen Lý Sơn đậm đà, xốt bisque sánh mịn đượm vị biển khơi, ăn kèm gnocchi khoai lang tím thủ công mềm mịn.",
                        Price = 1950000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuDX3Wm3ai-5mpznc4LtrQLUc0_NlVQ9fnXqfa8-t2a0-DveaSTDen49ZL7ohB3wd64BjsxeLzWifFYIZ0bbTYgFF-DC4-mzRkVcHLFxwZ2gspzYXzoGeaYu857CM_ND0EpZ7Qtkrls513ZMLTVWIWv2lnD13JCK3uDVQtAW8Z4wNSZAsyC-H_N-o7r_0wThsIpz0m5anQUe2oU_7Jf2dj1mXislSdVjR71YVdDymG7C",
                        DietaryTags = "Signature",
                        WinePairingNote = "Rượu vang: Meursault Domaine Faiveley",
                        IsFeatured = true,
                        PreparationTimeMinutes = 22,
                        CategoryId = cMains
                    },
                    new MenuItem
                    {
                        Name = "Cá Tuyết Nam Cực Hấp Bơ Chanh Cay",
                        FrenchSubtitle = "Légine Australe Pochée au Beurre Blanc",
                        Description = "Cá tuyết Nam Cực thịt mọng mềm, hấp chậm ở 62°C giữ trọn chất dầu quý giá. Kết hợp cùng nước xốt bơ Pháp Beurre Blanc gia giảm lá chanh Kaffir tươi và măng tây trắng Đà Lạt.",
                        Price = 1250000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBymKwC8ZSBp17htD7lcq_eO0JHLQR-Y0k2b2LtrK6SO3M58raZa0o08JLqD3PgY9_IkqoUJAEqudUdRncrVOkynsFTYfRAXEkc5whQd93qNJkd4uq0Ae8XyOtbqmRdfbMC_QkMQB5-o1wZiUBPa2ZuRVl7kPCclSZSTt9nSnZnKDWYxx9kMVVv1xVXww9YK25PGJWL6LguXbv4LJ0Xc63bpUOVZOs_F9mUcnFC6Z64",
                        DietaryTags = "GF",
                        WinePairingNote = "Rượu vang: Puligny-Montrachet Chardonnay",
                        IsFeatured = false,
                        PreparationTimeMinutes = 18,
                        CategoryId = cMains
                    },

                    // TRÁNG MIỆNG (DESSERTS)
                    new MenuItem
                    {
                        Name = "Sô-cô-la Đen Bến Tre & Muối Khói",
                        FrenchSubtitle = "Soufflé Chaud au Chocolat Marou 70%",
                        Description = "Mousse & Soufflé sô-cô-la 70% ca cao nguyên chất Tiền Giang - Bến Tre kết hợp hạt phỉ rang giòn, kem lạnh tiêu rừng Tiên Phước và vị mặn tinh tế của muối biển hun khói.",
                        Price = 420000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuB2DKSxg6Vo0KjoAwjvShXCi7HX5VjcKWH6c-9Ar1IvT3hZ64FCGGRcJn1_JM-ia1IRiscsNhM00JEPIkrzfXp5XxrymJxCYSyIxaJp6qs5Bg3OQ5AVrJuUQYbNIOFu1yAPpGV9vR4C_TduHaIJkS2PXhSF3VB0LMy47u2aiAtq8h14Y0T2Yc572xzbPxORGDWBBGKfm2SMmt98n3pbOY6Z7dbLDeCHCyaxX5mN10RQ",
                        DietaryTags = "Nuts Included",
                        WinePairingNote = "Được phục vụ nóng tại bàn (18 phút)",
                        IsFeatured = true,
                        PreparationTimeMinutes = 18,
                        CategoryId = cDesserts
                    },
                    new MenuItem
                    {
                        Name = "Tart Lê Vang Đỏ & Vani Bourbon",
                        FrenchSubtitle = "Tarte Fine aux Poires Pochées",
                        Description = "Lê Sa Pa rim chậm trong rượu vang Pinot Noir cùng quế hồi Trà Bồng, lớp đế tart nướng bơ Isigny giòn tan, phủ sốt caramen muối và kem vanilla Madagascar mượt mà.",
                        Price = 320000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuCqgi86DPmIj27o6lnMgAaVVflZzEtcc6ii5oVr2WTgQ7TpSoC47R_7rOG6fSg7xL_GD1M3Y7CogWIIyIdY7ebU-6jSXdxA8fFSwIARBsjGauyjYc2L0kdCGWygsALotE6GNKFTrqVL__sMO4hxhggnWPkCag1efxRG0YYONn1_UkvBPLdpzc2Gf8-WPYKVENKjQ16-yPkeqVnOLfh9pEhkmIpLiUeUzDbRhkhRb2uL",
                        DietaryTags = "V • Signature",
                        WinePairingNote = "Phục vụ kèm kem lạnh thủ công",
                        IsFeatured = false,
                        PreparationTimeMinutes = 12,
                        CategoryId = cDesserts
                    },

                    // RƯỢU VANG (WINES)
                    new MenuItem
                    {
                        Name = "Dom Pérignon Vintage Champagne",
                        FrenchSubtitle = "Épernay, France · Chardonnay & Pinot Noir",
                        Description = "Dòng Champagne biểu tượng với bọt sủi mịn như tơ, tầng hương hoa trắng, hạnh nhân nướng và khoáng chất thanh khiết.",
                        Price = 8200000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBGXOV1IrS0qjrP3uSYxWAF8slO5o092eRFQCs-am9Qh4y6tspS525uEUgkOcxktxF1WYHyzz9BsCYGD9tPsGqqIceMFP9jKGYfKwiy83fmfCuy_TNNrk3RvESkGsYWZmoptYPO6nh6oDgVQx2SzMC2VtFjbGqLWsBlh75cLhV_YdV79d1HNGzdlDWFDx6tl4UfngmsO_QpgNlDilzUb0fmPhEposFo3sRV3GWsF3G2",
                        DietaryTags = "Grand Cru",
                        WinePairingNote = "Khai tiệc & Hải sản cao cấp",
                        IsFeatured = false,
                        PreparationTimeMinutes = 5,
                        CategoryId = cWines
                    },
                    new MenuItem
                    {
                        Name = "Chablis Grand Cru 'Les Clos' William Fèvre",
                        FrenchSubtitle = "Bourgogne, France · 100% Chardonnay",
                        Description = "Vang trắng Grand Cru đỉnh cao vùng Chablis với vị khoáng đá vôi sắc nét, hậu vị kéo dài thanh thoát.",
                        Price = 5200000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuD0QxoqmRs2dpu9PDcsM2dn5l3dWGTyze4D6CuB2m9tkbwwYe2IwA-AIU_EcKLePVD487ZjzVuxZu_LUqAYPEGxkjMuFU7JpNVhCM3304_CNhJt6MYuNM5KRGlsvBd_Gry3McB88_cTKesUNc1z4Lb34mY4T8-7MeBn2F9F85pcsuF5LOE7W1C8LumYCSwSFfjzUXCCL2tXCDT0Q9IiefswrakGB_VMsxyJTQxReCCx",
                        DietaryTags = "Grand Cru",
                        WinePairingNote = "Nhiệt độ phục vụ chuẩn 10°C - 12°C",
                        IsFeatured = false,
                        PreparationTimeMinutes = 5,
                        CategoryId = cWines
                    },
                    new MenuItem
                    {
                        Name = "Châteauneuf-du-Pape Château de Beaucastel",
                        FrenchSubtitle = "Vallée du Rhône, France · 2018 Rouge",
                        Description = "Vang đỏ thượng hạng đượm vị trái cây đen chín mọng, thảo mộc Địa Trung Hải, nấm truffle và gỗ sồi Pháp lâu năm.",
                        Price = 3850000,
                        ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBxhfSQsTSk8JqybDidnMVdK9GyMTRvFCvoTGw0j4dVC1MMefLR7ptAodII4ZYTJiq36Gob_pkupzzPoTTOqqUxYhO7NGonjEVrVl3025FJpeE_TV72LG3wfCMqaJ41o-EWpqcMULatHfARFTmceNKY5TPQ_hqEBU8Xp50oWlfqEBsqi89dB_Vj-a3viBH0jJXO9SI1e2-oRDb_FiZ4pdqq3wgJV9sbZXRZDunKhjci",
                        DietaryTags = "Grand Cru",
                        WinePairingNote = "Decanting thở oxy tối thiểu 45 phút",
                        IsFeatured = false,
                        PreparationTimeMinutes = 5,
                        CategoryId = cWines
                    }
                };

                context.MenuItems.AddRange(items);
                context.SaveChanges();
            }

            if (!context.DiningTables.Any())
            {
                var tables = new List<DiningTable>
                {
                    new DiningTable { TableNumber = "Bàn Cửa Sổ W-01", Capacity = 2, Area = "Bàn Cạnh Cửa Sổ Hướng Vườn", Status = "Trống" },
                    new DiningTable { TableNumber = "Bàn Cửa Sổ W-02", Capacity = 2, Area = "Bàn Cạnh Cửa Sổ Hướng Vườn", Status = "Đã đặt" },
                    new DiningTable { TableNumber = "Bàn Cửa Sổ W-03", Capacity = 4, Area = "Bàn Cạnh Cửa Sổ Hướng Vườn", Status = "Trống" },
                    new DiningTable { TableNumber = "Bàn Salon C-01", Capacity = 4, Area = "Bàn Trung Tâm Ấm Cúng", Status = "Trống" },
                    new DiningTable { TableNumber = "Bàn Salon C-02", Capacity = 4, Area = "Bàn Trung Tâm Ấm Cúng", Status = "Đang phục vụ" },
                    new DiningTable { TableNumber = "Bàn Salon C-03", Capacity = 6, Area = "Bàn Trung Tâm Ấm Cúng", Status = "Trống" },
                    new DiningTable { TableNumber = "Chef's Counter K-01", Capacity = 2, Area = "Chef's Counter (Quầy Bếp Trưởng)", Status = "Trống" },
                    new DiningTable { TableNumber = "Chef's Counter K-02", Capacity = 4, Area = "Chef's Counter (Quầy Bếp Trưởng)", Status = "Trống" },
                    new DiningTable { TableNumber = "Salon Privé L'Amour", Capacity = 10, Area = "Phòng Riêng VIP Atelier Salon", Status = "Đã đặt" },
                    new DiningTable { TableNumber = "Grand Atelier Suite", Capacity = 16, Area = "Phòng Riêng VIP Atelier Salon", Status = "Trống" }
                };
                context.DiningTables.AddRange(tables);
                context.SaveChanges();
            }

            if (!context.Reservations.Any())
            {
                var vipTable = context.DiningTables.FirstOrDefault(t => t.TableNumber.Contains("L'Amour"));
                var winTable = context.DiningTables.FirstOrDefault(t => t.TableNumber.Contains("W-02"));

                var reservations = new List<Reservation>
                {
                    new Reservation
                    {
                        CustomerName = "Ông Nguyễn Hoàng Minh",
                        CustomerPhone = "0908123456",
                        CustomerEmail = "hoangminh.nguyen@craftluxury.vn",
                        ReservationDate = DateTime.Today.AddDays(1),
                        ReservationTime = "19:30 (Tối)",
                        NumberOfGuests = 2,
                        GuestGroupLabel = "2 khách (Duo)",
                        SeatingArea = "Bàn Cạnh Cửa Sổ Hướng Vườn",
                        SelectedMenuPackage = "Tasting Menu Prestige (7 Món)",
                        HasWinePairing = true,
                        EstimatedTotal = 7800000,
                        Occasion = "Hẹn hò lãng mạn",
                        SpecialRequests = "1 khách kiêng hải sản có vỏ; chuẩn bị hoa hồng trắng và nến thơm tại bàn.",
                        Status = "Đã xác nhận",
                        DiningTableId = winTable?.Id,
                        CreatedAt = DateTime.Now.AddHours(-3)
                    },
                    new Reservation
                    {
                        CustomerName = "Ông Philippe Le Tran",
                        CustomerPhone = "0987654321",
                        CustomerEmail = "khachhang@latelier.vn",
                        CompanyVatInfo = "Tập đoàn Monarch Holdings - MST: 0312889900",
                        ReservationDate = DateTime.Today.AddDays(2),
                        ReservationTime = "18:30 (Tối)",
                        NumberOfGuests = 8,
                        GuestGroupLabel = "8+ khách (Phòng tiệc riêng VIP)",
                        SeatingArea = "Phòng Riêng VIP Atelier Salon",
                        SelectedMenuPackage = "Tasting Menu Prestige (7 Món)",
                        HasWinePairing = true,
                        EstimatedTotal = 31200000,
                        Occasion = "Tiệc chiêu đãi đối tác",
                        SpecialRequests = "Yêu cầu Sommelier Marcella Trần trực tiếp mở rượu Château Margaux 2012.",
                        Status = "Chờ xác nhận",
                        DiningTableId = vipTable?.Id,
                        CreatedAt = DateTime.Now.AddHours(-1)
                    }
                };
                context.Reservations.AddRange(reservations);
                context.SaveChanges();
            }

            if (!context.ContactInquiries.Any())
            {
                context.ContactInquiries.Add(new ContactInquiry
                {
                    FullName = "Bà Lê Ngọc Bảo Trân",
                    Phone = "0938998877",
                    Email = "baotran.le@luxuryevents.vn",
                    Purpose = "Tiệc riêng tư (Private Dining)",
                    EstimatedGuests = "Phòng Salon L'Amour (6 - 12 khách)",
                    EventDate = DateTime.Today.AddDays(7),
                    Message = "Mong muốn đặt tiệc kỷ niệm 10 năm ngày cưới, thiết kế riêng thực đơn 7 món và trang trí hoa tươi.",
                    Status = "Mới tiếp nhận",
                    CreatedAt = DateTime.Now.AddHours(-2)
                });
                context.SaveChanges();
            }
        }
    }
}

