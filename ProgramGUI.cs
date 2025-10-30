using DTO_QLDeTai;
using BLL_QLDeTai;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
public class ProgramGUI
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        QLDeTaiBLL ds = new QLDeTaiBLL();
        bool thoat = false;
        while (!thoat)
        {
            Console.WriteLine("-----------------MENU CHỌN CHỨC NĂNG!------------------");
            Console.WriteLine("1. Xuất danh sách đề tài hiện có (Yêu cầu 3)");
            Console.WriteLine("2. Tìm kiếm đề tài theo từ khóa (Yêu cầu 4)");
            Console.WriteLine("3. Xuất DS đề tài theo tên GVHD (Yêu cầu 5)");
            Console.WriteLine("4. Cập nhật Kinh phí tăng 10% (Yêu cầu 6)");
            Console.WriteLine("5. Xuất DS đề tài có Kinh phí trên 10 Triệu (Yêu cầu 7)");
            Console.WriteLine("6. Xuất DS đề tài Kinh Tế trên 100 Câu hỏi (Yêu cầu 9)");
            Console.WriteLine("7. Xuất DS đề tài có Thời gian thực hiện trên 4 Tháng (Yêu cầu 10)");
            Console.WriteLine("8. THÊM MỚI một đề tài từ bàn phím(yêu cầu 2)");
            Console.WriteLine("9. XUẤT DS đề tài Lý Thuyết có khả năng triển khai thực tế(yêu cầu 8)");
            Console.WriteLine("0. Thoát chương trình");
            Console.WriteLine("-------------------------------------------------------");
            Console.Write("Mời bạn chọn chức năng (0-9): ");

            string luaChon = Console.ReadLine();
            Console.WriteLine();

            switch (luaChon)
            {
                case "1":
                    // Yêu cầu 3: Xuất danh sách
                    Console.WriteLine("DANH SÁCH ĐỀ TÀI HIỆN CÓ");

                    ds.XuatDS();
                    break;

                case "2":
                    // Yêu cầu 4: Tìm kiếm đề tài
                    Console.WriteLine("\n===== TÌM KIẾM ĐỀ TÀI =====");
                    Console.Write("Nhâp từ khóa tìm kiếm(mã số / tên đề tài / GVHD / trưởng nhóm): ");
                    string keyword = Console.ReadLine();

                    List<DeTaiDTO> ketQua = ds.TimKiemDeTai(keyword);
                    if (ketQua.Count == 0)
                    {
                        Console.WriteLine("Không tìm kiếm thấy đề tài nào phù hợp!");
                    }
                    else
                    {

                        Console.WriteLine($"\nKẾT QUẢ TÌM KIẾM CHO: \"{keyword}\"");
                        foreach (DeTaiDTO d in ketQua)
                        {
                            d.Xuat();
                        }
                    }
                    break;

                case "3":
                    // Yêu cầu 5: DS theo GVHD

                    Console.WriteLine("\n===== XUẤT DS THEO GVHD =====");
                    Console.Write("Nhâp tên giảng viên hd: ");
                    string tenGV = Console.ReadLine();

                    List<DeTaiDTO> dsTheoGV = ds.DSDeTaiTheoGVHD(tenGV);
                    if (dsTheoGV.Count == 0)
                    {
                        Console.WriteLine($"Không có đề tài nào do GVHD '{tenGV}' hướng dẫn!");
                    }
                    else
                    {
                        Console.WriteLine($"\nDANH SÁCH ĐỀ TÀI CỦA GVHD '{tenGV}':");
                        foreach (DeTaiDTO d in dsTheoGV)
                        {
                            d.Xuat();
                        }
                    }
                    break;

                case "4":
                    // Yêu cầu 6: Cập nhật kinh phí tăng 10%
                    ds.capNhatKinhPhiTang();
                    break;

                case "5":
                    // Yêu cầu 7: DS có kinh phí trên 10 Triệu
                    Console.WriteLine("DANH SÁCH ĐỀ TÀI CÓ KINH PHÍ TRÊN 10 TRIỆU");

                    List<DeTaiDTO> dsKinhPhiTren10TR = ds.DSDeTaiKinhPhiTren10TR();
                    foreach (DeTaiDTO detai in dsKinhPhiTren10TR)
                    {
                        detai.Xuat();
                    }
                    break;

                case "6":
                    // Yêu cầu 9: DS Đề tài kinh tế > 100 câu hỏi
                    Console.WriteLine("DANH SÁCH ĐỀ TÀI KINH TẾ > 100 CÂU HỎI");
                    List<DeTaiKinhTeDTO> dsKinhTe = ds.DSDeTaiKinhTeTren100Cau();
                    foreach (DeTaiKinhTeDTO dt in dsKinhTe)
                    {
                        dt.Xuat();
                    }
                    break;

                case "7":
                    // Yêu cầu 10: DS đề tài có thời gian > 4 Tháng
                    Console.WriteLine("DANH SÁCH ĐỀ TÀI CÓ THỜI GIAN > 4 THÁNG");
                    List<DeTaiDTO> dsThoiGian = ds.DSCoThoiGianTren4Thang();
                    foreach (DeTaiDTO dt in dsThoiGian)
                    {
                        dt.Xuat();

                    }
                    break;

                case "8":
                    // Yêu cầu thêm mới một đề tài
                    Console.WriteLine("Thêm một đề tài");
                    ds.ThucHienThemDeTai(ds);
                    break;

                case "9":
                    // Yêu cầu xuất DS Lý thuyết có khả năng triển khai
                    ds.ThucHienXuat(ds);
                    break;

                case "0":
                    thoat = true;
                    Console.WriteLine("\nĐã Thoát Chương Trình !!");
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Hãy chọn lại.");
                    break;
            }

            if (!thoat)
            {
                Console.WriteLine("\n(Nhấn Enter để quay lại menu: )");
                Console.ReadKey();
            }
        }
    }
}