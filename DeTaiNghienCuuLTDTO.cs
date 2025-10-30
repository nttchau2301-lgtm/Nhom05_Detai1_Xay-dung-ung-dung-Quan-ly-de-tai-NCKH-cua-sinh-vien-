using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLDeTai
{
    public class DeTaiNghienCuuLTDTO : DeTaiDTO
    {
        protected bool apDungTT;

        public bool ApDungTT
        {
            get { return apDungTT; }
            set { apDungTT = value; }
        }

        public DeTaiNghienCuuLTDTO()
        {
            ApDungTT = true;
        }
        public DeTaiNghienCuuLTDTO(string ma, string ten, string tn, string gvhd, string linhvuc, DateTime bd, DateTime kt, bool thucte) : base(ma, ten, tn, gvhd, linhvuc, bd, kt)
        {
            ApDungTT=thucte;
        }

        public override double TinhKinhPhiTH()
        {
            if (ApDungTT == false)
                return 8000000;
            else
                return 1500000;
        }
    }
}
