using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public partial class Cariislem
    {
        public Cariislem()
        {
            Kasas = new HashSet<Kasa>();
        }

        public int Id { get; set; }
        public int CariKartId { get; set; }
        public DateTime Tarih { get; set; }
        public decimal Borc { get; set; }
        public decimal Alacak { get; set; }
        public string? Aciklama { get; set; }
        public int HisseCariKartId { get; set; }

        public virtual Carikart CariKart { get; set; } = null!;
        public virtual Hissecarikart HisseCariKart { get; set; } = null!;
        public virtual ICollection<Kasa> Kasas { get; set; }
    }
}
