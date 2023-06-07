using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class StokManager : IStokService
    {
        IStokDal _stokDal;

        public StokManager(IStokDal stokDal)
        {
            _stokDal = stokDal;
        }

        public List<Stok> GetList()
        {
            return _stokDal.GetListAll();
        }

        public void TAdd(Stok t)
        {
            _stokDal.Insert(t);
        }

        public void TDelete(Stok t)
        {
            _stokDal.Delete(t);
        }

        public Stok TGetById(int id)
        {
           return _stokDal.GetByID(id);
        }
		public List<Stok> GetKurbanlikById(int id)
		{
			return _stokDal.GetListAll(x => x.Id == id);
		}
		public void TUpdate(Stok t)
        {
            _stokDal.Update(t);
        }
    }
}
