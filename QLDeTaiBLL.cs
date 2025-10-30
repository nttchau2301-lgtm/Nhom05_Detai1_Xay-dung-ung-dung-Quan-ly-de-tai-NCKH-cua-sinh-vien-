using DTO_QLDeTai;
using DAL_QLDeTai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BLL_QLDeTai
{
    public class QLDeTaiBLL
    {
        protected string tenTruong;
        protected string diaChi;
        protected string sDT;
        public DocFileDAL h =new DocFileDAL();
        public List<DeTaiDTO> lst=new List<DeTaiDTO>();

        public string TenTruong
        {
            get { return tenTruong; }
            set { tenTruong = value; }
        }
        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }
        public string SDT
        {
            get { return sDT; }
            set { sDT = value; }
        }
        public QLDeTaiBLL()
        {
            // Đọc dữ liệu ngay khi khởi tạo đối tượng
            h.DocFile();
            lst = h.lst;
            tenTruong = h.TenTruong;
            diaChi = h.DiaChi;
            sDT = h.SDT;
        }
        //yêu cầu 2
        public void ThucHienThemDeTai(QLDeTaiBLL ql)
        {
            Console.WriteLine("\n--- THÊM MỚI MỘT ĐỀ TÀI ---");
            Console.Write("Nhap loai de tai (LT/KT/CN): ");
            string loai = Console.ReadLine().ToUpper();


            Console.Write("Nhập mã số đề tài: ");
            string ma = Console.ReadLine();
            Console.Write("Nhập tên đề tài: ");
            string ten = Console.ReadLine();
            Console.Write("Nhập trưởng nhóm: ");
            string tn = Console.ReadLine();
            Console.Write("Nhập GVHD: ");
            string gvhd = Console.ReadLine();


            DateTime bd, kt;
            Console.Write("Nhập thời gian bắt đầu (dd/MM/yyyy): ");

            while (!DateTime.TryParse(Console.ReadLine(), out bd))
            {
                Console.Write("Đinh dạng sai, nhập lại thời gian bắt đầu (dd/MM/yyyy): ");
            }
            Console.Write("Nhập thời gian kết thúc (dd/MM/yyyy): ");
            while (!DateTime.TryParse(Console.ReadLine(), out kt))
            {
                Console.Write("Định dang sai, nhập lại thời gian kết thúc (dd/MM/yyyy): ");
            }

            DeTaiDTO dtMoi = null;

            switch (loai)
            {
                case "LT":
                    Console.Write("có khả năng triển khai thực tế không (true/false): ");
                    bool apDung;
                    bool.TryParse(Console.ReadLine(), out apDung);

                    dtMoi = new DeTaiNghienCuuLTDTO(ma, ten, tn, gvhd, "LyThuyet", bd, kt, apDung);
                    break;

                case "KT":
                    Console.Write("Nhập số câu hỏi khảo sát: ");
                    int soCau;
                    int.TryParse(Console.ReadLine(), out soCau);


                    dtMoi = new DeTaiKinhTeDTO(ma, ten, tn, gvhd, "KinhTe", bd, kt, soCau);
                    break;

                case "CN":
                    Console.Write("Nhập môi trường triển khai (Web/mobile/Window): ");
                    string mt = Console.ReadLine();

                    dtMoi = new DeTaiCongNgheDTO(ma, ten, tn, gvhd, "CongNghe", bd, kt, mt);
                    break;

                default:
                    Console.WriteLine("Loại đề tài không hợp lệ!");
                    return;
            }

            if (dtMoi != null)
            {
                h.DocFile();
                ql.lst.Add(dtMoi);
                Console.WriteLine("\n--> TTHÊM ĐỀ TÀI THÀNH CÔNG!");
                dtMoi.Xuat();
            }
        }
       

        //Yêu cầu 4
        public List<DeTaiDTO> TimKiemDeTai(string tuKhoa)
        {
            if (string.IsNullOrEmpty(tuKhoa))
                return new List<DeTaiDTO>();
            tuKhoa = tuKhoa.Trim();
            return lst
                .OfType<DeTaiDTO>()
                .Where(dt => (!string.IsNullOrEmpty(dt.MaSoDT) && dt.MaSoDT.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0) || (!string.IsNullOrEmpty(dt.TenDT) && dt.TenDT.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0) || (!string.IsNullOrEmpty(dt.GVHD) && dt.GVHD.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0) || (!string.IsNullOrEmpty(dt.TruongNhom) && dt.TruongNhom.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
        }

        //Yêu cầu 5
        public List<DeTaiDTO> DSDeTaiTheoGVHD(string tenGV)
        {
            return lst.OfType<DeTaiDTO>()
                .Where(dt => !string.IsNullOrEmpty(dt.GVHD) && dt.GVHD.IndexOf(tenGV, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }
        //Yêu cầu 6
        public void capNhatKinhPhiTang()
        {
            Console.WriteLine("\n===== DANH SACH KINH PHI SAU KHI TANG 10% =====");
            foreach (DeTaiDTO detai in lst)
            {
                double kinhPhiCu = detai.TinhKinhPhiTH();
                double kinhPhiMoi = kinhPhiCu * 1.1;
                Console.WriteLine($"Ma de tai: {detai.MaSoDT}");
                Console.WriteLine($"Ten de tai: {detai.TenDT}");
                Console.WriteLine($"Kinh phí cũ: {kinhPhiCu:N0} VNĐ");
                Console.WriteLine($"Kinh phí mới: {kinhPhiMoi:N0} VNĐ");
                Console.WriteLine();
            }
        }

        //Yêu cầu 7
        public List<DeTaiDTO> DSDeTaiKinhPhiTren10TR()
        {
            return lst.Where(dt => dt.TinhKinhPhiTH() > 10000000).ToList();
        }
        //yêu cầu 8
        public void ThucHienXuat(QLDeTaiBLL ql)
        {
            Console.WriteLine("\n=======================================================");
            Console.WriteLine("DANH SÁCH ĐỀ TÀI LÝ THUYÊT CÓ KHẢ NĂNG TRIỂN KHAI THỰC TẾ");
            Console.WriteLine("=======================================================");


            var ketQua = ql.lst
                .OfType<DeTaiNghienCuuLTDTO>()
                .Where(dtLT => dtLT.ApDungTT == true)
                .ToList();

            if (ketQua.Any())
            {
                foreach (var dt in ketQua)
                {
                    dt.Xuat();
                }
            }
            else
            {
                Console.WriteLine("KHÔNG CÓ ĐỀ TÀI LÝ THUYẾT NÀO THỎA MÃN ĐIỀU KIỆN TRÊN!");
            }
        }
        //Yêu cầu 9
        public List<DeTaiKinhTeDTO> DSDeTaiKinhTeTren100Cau()
        {
            return lst.OfType<DeTaiKinhTeDTO>()
                      .Where(dt => dt.SoCauHoiKS > 100)
                      .ToList();
        }

        // Yêu cầu 10
        public List<DeTaiDTO> DSCoThoiGianTren4Thang()
        {
            return lst.Where(dt => dt.TinhSoThangThucHien() > 4).ToList();
        }
        public void XuatDS()
        {
            Console.WriteLine(TenTruong);
            Console.WriteLine(DiaChi);
            Console.WriteLine(SDT);
            Console.WriteLine("***Danh sach de tai***");
            foreach (DeTaiDTO detai in lst)
            {
                detai.Xuat();
            }
        }
    }
}