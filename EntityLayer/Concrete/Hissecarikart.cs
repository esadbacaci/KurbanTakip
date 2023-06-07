using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public partial class Hissecarikart
    {
        public Hissecarikart()
        {
            Cariislems = new HashSet<Cariislem>();
        }

        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public decimal HisseTutar { get; set; }
        public bool VekaletAlindiMi { get; set; }
        public bool EtTeslimEdildiMi { get; set; }
        public int CariKartId { get; set; }
        public int StokId { get; set; }

        public virtual Carikart CariKart { get; set; } = null!;
        public virtual Stok Stok { get; set; } = null!;
        public virtual ICollection<Cariislem> Cariislems { get; set; }
    }
}
