using EntityLayer.Concrete;
using System.Reflection.Metadata;

namespace KurbanTakip.Models
{
	public class CariStokModel
	{
		public IEnumerable<Carikart> Carikarts { get; set; }
		public IEnumerable<Stok> Stoks { get; set; }
		public IEnumerable<Hissecarikart> Hissecarikarts { get; set; }
		public IEnumerable<Cariislem> Cariislems{ get; set; }
		public IEnumerable<Kasa> Kasas{ get; set; }

        //public Stok Stok { get; set; }
    }
}
