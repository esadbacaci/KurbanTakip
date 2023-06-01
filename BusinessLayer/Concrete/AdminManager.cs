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
	public class AdminManager : IAdminService
	{
		IAdminDal _adminDal;

		public AdminManager(IAdminDal adminDal)
		{
			_adminDal = adminDal;
		}

		public List<Admin> GetList()
		{
			throw new NotImplementedException();
		}

		public void TAdd(Admin t)
		{
			throw new NotImplementedException();
		}

		public void TDelete(Admin t)
		{
			throw new NotImplementedException();
		}

		public Admin TGetById(int id)
		{
			return _adminDal.GetByID(id);
		}

		public void TUpdate(Admin t)
		{
			_adminDal.Update(t);
		}
	}
}
