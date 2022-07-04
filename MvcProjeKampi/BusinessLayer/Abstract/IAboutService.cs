using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAboutService
    {
        void AboutDelete(About about);
        void UpdateAbout(About about);
        void AddAbout(About about);
        List<About> GetList();
        About GetById(int id);


    }
}
