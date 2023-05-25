using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CarikartManager : ICariKartService
    {
        ICarikartDal _carikartDal;

        public CarikartManager(ICarikartDal carikartDal)
        {
            _carikartDal = carikartDal;
        }

        public List<Carikart> GetList()
        {
           return _carikartDal.GetListAll();
        }

        public void TAdd(Carikart t)
        {
            _carikartDal.Insert(t);
        }

        public void TDelete(Carikart t)
        {
            _carikartDal.Delete(t);
        }

        public Carikart TGetById(int id)
        {
            return _carikartDal.GetByID(id);
        }
		public List<Carikart> GetCariById(int id)
		{
			return _carikartDal.GetListAll(x => x.Id == id);

		}
		public void TUpdate(Carikart t)
        {
            _carikartDal.Update(t);
        }
    }
}
