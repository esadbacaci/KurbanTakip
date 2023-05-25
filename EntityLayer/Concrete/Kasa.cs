using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public partial class Kasa
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public decimal? Tutar { get; set; }
        public string? Aciklama { get; set; }
        public int CariIslemId { get; set; }
        public bool? GirisMi { get; set; }

        public virtual Cariislem CariIslem { get; set; } = null!;
    }
}
