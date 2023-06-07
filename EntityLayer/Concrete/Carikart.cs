using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public partial class Carikart
    {
        public Carikart()
        {
            Cariislems = new HashSet<Cariislem>();
            Hissecarikarts = new HashSet<Hissecarikart>();
        }

        public int Id { get; set; }
        public string AdSoyad { get; set; } = null!;
        public string? Telefon { get; set; } = null!;
        public string? Referans { get; set; }
        public DateTime Tarih { get; set; }
        public string? Aciklama { get; set; }
        public virtual ICollection<Cariislem> Cariislems { get; set; }
        public virtual ICollection<Hissecarikart> Hissecarikarts { get; set; }
    }
}
