using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public partial class Stok
    {
        public Stok()
        {
            Hissecarikarts = new HashSet<Hissecarikart>();
        }

        public int Id { get; set; }
        public int SiraNo { get; set; }
        public string Ad { get; set; } = null!;
        public string Kod { get; set; } = null!;
        public int HisseAdet { get; set; }
        public decimal HisseFiyat { get; set; }
        public decimal Kilo { get; set; }
        public int Yas { get; set; }
        public string KupeNo { get; set; } = null!;
        public string Cins { get; set; } = null!;

        public virtual ICollection<Hissecarikart> Hissecarikarts { get; set; }
    }
}
