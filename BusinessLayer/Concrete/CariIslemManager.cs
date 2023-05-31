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
	public class CariIslemManager : ICariIslemService
	{
		ICariIslemDal _cariIslemDal;

		public CariIslemManager(ICariIslemDal cariIslemDal)
		{
			_cariIslemDal = cariIslemDal;
		}

		public List<Cariislem> GetList()
		{
			return _cariIslemDal.GetListAll();
		}

		public void TAdd(Cariislem t)
		{
			_cariIslemDal.Insert(t);
		}

		public void TDelete(Cariislem t)
		{
			_cariIslemDal.Delete(t);
		}

		public Cariislem TGetById(int id)
		{
			return _cariIslemDal.GetByID(id);
		}
		public List<Cariislem> GetHisseCariIslemById(int Id)
		{
			return _cariIslemDal.GetListAll(x => x.CariKartId == Id);

		}
		public void TUpdate(Cariislem t)
		{
			_cariIslemDal.Update(t);
		}
	}
}
