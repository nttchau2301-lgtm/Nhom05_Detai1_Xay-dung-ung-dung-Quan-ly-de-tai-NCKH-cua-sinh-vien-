using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLDeTai
{
    public class DeTaiKinhTeDTO : DeTaiDTO, IHoTroNghienCuuDTO
    {
        protected int soCauHoiKS;

        public int SoCauHoiKS
        {
            get { return soCauHoiKS; }
            set { soCauHoiKS = value; }
        }

        public DeTaiKinhTeDTO()
        {
            SoCauHoiKS = 0;
        }
        public DeTaiKinhTeDTO(string ma, string ten, string tn, string gvhd, string linhvuc, DateTime bd, DateTime kt, int cauhoi) : base(ma, ten, tn, gvhd, linhvuc, bd, kt)
        {
            SoCauHoiKS = cauhoi;
        }

        public double TinhPhiHT()
        {
            return SoCauHoiKS > 100 ? 550 * SoCauHoiKS : 450 * SoCauHoiKS;
        }
        public override double TinhKinhPhiTH()
        {
            return SoCauHoiKS > 100 ? 12000000  : 7000000 ;
        }
        public override double TinhTongKinhPhi()
        {
            return TinhKinhPhiTH() + TinhPhiHT();
        }

    }
}