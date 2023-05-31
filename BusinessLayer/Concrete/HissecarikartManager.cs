using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class HissecarikartManager : IHissecarikartService
	{
		IHissecarikartsDal _hissecarikarts;

		public HissecarikartManager(IHissecarikartsDal hissecarikarts)
		{
			_hissecarikarts = hissecarikarts;
		}

		public List<Hissecarikart> GetList()
		{
		return	_hissecarikarts.GetListAll();
		}

		public void TAdd(Hissecarikart t)
		{
			_hissecarikarts.Insert(t);
		}

		public void TDelete(Hissecarikart t)
		{
			_hissecarikarts.Delete(t);
		}

		public Hissecarikart TGetById(int id)
		{
			return _hissecarikarts.GetByID(id);
		}
		public List<Hissecarikart> GetHisseCarikartById(int Id)
		{
            return _hissecarikarts.GetListAll(x => x.CariKartId == Id);

        }
		public List<Hissecarikart> GetHisseCarikartByStokId(int StokId)
		{
			return _hissecarikarts.GetListAll(x => x.StokId == StokId);

		}
		public void TUpdate(Hissecarikart t)
		{
			_hissecarikarts.Update(t);
		}
	}
}
