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
    public class KasaManager : IKasaService
    {
        IKasaDal _kasaDal;

        public KasaManager(IKasaDal kasaDal)
        {
            _kasaDal = kasaDal;
        }

        public List<Kasa> GetList()
        {
           return _kasaDal.GetListAll();
        }

        public void TAdd(Kasa t)
        {
            _kasaDal.Insert(t);
        }

        public void TDelete(Kasa t)
        {
            _kasaDal.Delete(t);
        }

        public Kasa TGetById(int id)
        {
           return _kasaDal.GetByID(id);
        }

        public void TUpdate(Kasa t)
        {
            _kasaDal.Update(t);
        }
    }
}
