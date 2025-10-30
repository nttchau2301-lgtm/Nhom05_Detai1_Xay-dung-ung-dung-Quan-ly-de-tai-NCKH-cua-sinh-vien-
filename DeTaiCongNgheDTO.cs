using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLDeTai
{
    public class DeTaiCongNgheDTO : DeTaiDTO, IHoTroNghienCuuDTO
    {
        protected string moiTruong;

        public string MoiTruong
        {
            get { return moiTruong; }
            set
            {
                if (value == "Web" || value == "mobile" || value == "Window")
                    moiTruong = value;
                else
                    throw new Exception("Moi truong khong hop le");
            }
        }

        public DeTaiCongNgheDTO()
        {
            MoiTruong = "Web";
        }
        public DeTaiCongNgheDTO(string ma, string ten, string tn, string gvhd, string linhvuc, DateTime bd, DateTime kt, string mt) : base(ma, ten, tn, gvhd, linhvuc, bd, kt)
        {
            MoiTruong = mt;
        }

        public double TinhPhiHT()
        {
            if (MoiTruong == "mobile")
                return 1000000;
            if (MoiTruong == "Web")
                return 800000;
            else
                return 500000;
        }
        public override double TinhKinhPhiTH()
        {
            if (MoiTruong == "Web" || MoiTruong == "mobile")
                return 15000000;
            else
                return 10000000;
        }
        public override double TinhTongKinhPhi()
        {
            return TinhKinhPhiTH() + TinhPhiHT();
        }

    }
}
