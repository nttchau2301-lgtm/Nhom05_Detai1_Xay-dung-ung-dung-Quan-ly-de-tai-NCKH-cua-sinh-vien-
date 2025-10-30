using DTO_QLDeTai;
using System.Collections.Generic;
using System.Xml;
namespace DAL_QLDeTai
{
    public class DocFileDAL
    {
        protected string tenTruong;
        protected string diaChi;
        protected string sDT;
        public List<DeTaiDTO> lst;

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

        public DocFileDAL()
        {
            lst = new List<DeTaiDTO>();
        }
        //yêu cầu 1

        public void DocFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\..\\..\\Data\\DeTaiList.xml");
            this.tenTruong = doc.SelectSingleNode("/TruongDH/TenTruong").InnerText;
            this.diaChi = doc.SelectSingleNode("/TruongDH/DiaChi").InnerText;
            this.sDT = doc.SelectSingleNode("/TruongDH/SDT").InnerText;
            XmlNodeList nodes = doc.SelectNodes("/TruongDH/DSDeTai/DeTai");
            foreach (XmlNode node in nodes)
            {
                DeTaiDTO dt = null;
                string ma = node["MaSoDT"].InnerText;
                string ten = node["TenDT"].InnerText;
                string tn = node["TruongNhom"].InnerText;
                string gvhd = node["GVHD"]?.InnerText;
                DateTime bd = DateTime.Parse(node["TGBatDau"].InnerText);
                DateTime kt = DateTime.Parse(node["TGKetThuc"].InnerText);
                string linhvuc = node["LinhVuc"].InnerText;

                if (linhvuc.Equals("LyThuyet"))
                {
                    
                    bool ApDungTT = bool.Parse(node["ApDungTT"].InnerText);
                    dt = new DeTaiNghienCuuLTDTO(ma, ten, tn, gvhd, linhvuc, bd, kt, ApDungTT);
                }
                if (linhvuc.Equals("KinhTe"))
                {
                   
                    int SoCauHoiKS = int.Parse(node["SoCau"].InnerText);
                    dt = new DeTaiKinhTeDTO(ma, ten, tn, gvhd, linhvuc, bd, kt, SoCauHoiKS);
                }
                if (linhvuc.Equals("CongNghe"))
                {
       
                    string MoiTruong = node["MoiTruong"].InnerText;
                    dt = new DeTaiCongNgheDTO(ma, ten, tn, gvhd, linhvuc, bd, kt, MoiTruong);
                }

                lst.Add(dt);

            }
        }
    }
}