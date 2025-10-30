using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLDeTai
{
    public abstract class DeTaiDTO
    {
        protected string maSoDT;
        protected string tenDT;
        protected string truongNhom;
        protected string gVHD;
        protected DateTime thoiGianBD;
        protected DateTime thoiGianKT;
        protected string linhVuc;

        public string MaSoDT
        {
            get { return maSoDT; }
            set { maSoDT = value; }
        }
        public string TenDT
        {
            get { return tenDT; }
            set { tenDT = value; }
        }
        public string TruongNhom
        {
            get { return truongNhom; }
            set { truongNhom = value; }
        }
        public string GVHD
        {
            get { return gVHD; }
            set { gVHD = value; }
        }
        public DateTime ThoiGianBD
        {
            get { return thoiGianBD; }
            set { thoiGianBD = value; }
        }
        public DateTime ThoiGianKT
        {
            get { return thoiGianKT; }
            set { thoiGianKT = value; }
        }
        public string LinhVuc
        {
            get { return linhVuc; }
            set
            {
                if (value == "LyThuyet" || value == "KinhTe" || value == "CongNghe")
                    linhVuc = value;
                else
                    throw new Exception("Linh vuc khong hop le");
            }
        }

        public DeTaiDTO()
        {
            MaSoDT = string.Empty;
            TenDT = string.Empty;
            TruongNhom = string.Empty;
            GVHD = string.Empty;
            LinhVuc = "CongNghe";
            ThoiGianBD = DateTime.Now;
            ThoiGianKT = DateTime.Now;
        }
        public DeTaiDTO(string ma, string ten, string tn, string gvhd, string linhvuc, DateTime bd, DateTime kt)
        {
            MaSoDT = ma;
            TenDT = ten;
            TruongNhom = tn;
            GVHD = gvhd;
            LinhVuc = linhvuc;
            ThoiGianBD = bd;
            ThoiGianKT = kt;
        }

        public abstract double TinhKinhPhiTH();
        public double TinhSoThangThucHien()
        {
            double soNgay = (thoiGianKT - thoiGianBD).TotalDays;
            return soNgay / 30.0;
        }
        public virtual double TinhTongKinhPhi()
        {
            return TinhKinhPhiTH(); 
        }

        public virtual void Xuat()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine($"Mã số đề tài     : {MaSoDT}");
            Console.WriteLine($"Tên đề tài       : {TenDT}");
            Console.WriteLine($"Lĩnh vực         : {LinhVuc}");
            Console.WriteLine($"Trưởng nhóm      : {TruongNhom}");
            Console.WriteLine($"GVHD             : {GVHD}");
            Console.WriteLine($"Thời gian bắt đầu: {ThoiGianBD:dd/MM/yyyy}");
            Console.WriteLine($"Thời gian kết thúc: {ThoiGianKT:dd/MM/yyyy}");
            Console.WriteLine($"Kinh phí thực hiện: {TinhKinhPhiTH():N0} VNĐ");
            Console.WriteLine($"Tổng chi phí: {TinhTongKinhPhi():N0} VNĐ");
            Console.WriteLine($"Thời gian thực hiện: {TinhSoThangThucHien():0.0} tháng");
            Console.WriteLine("=============================================\n");
        }

    }
}
