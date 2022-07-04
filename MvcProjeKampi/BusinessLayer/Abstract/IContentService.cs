using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IContentService
    {
        List<Content> GetList();
        List<Content> GetAllList(string p); //Search işlemi için
        List<Content> GetListByWriter(int id);
        List<Content> GetListByHeadingId(int id);//Şöyle düşün bir yazarın bilgileri var onun ayrıntısına tıkladığın zaman onun yazmış olduğu kitaplar listeleneiyor
        void ContentAdd(Content content);
        void ContentDelete(Content content);
        void ContentUpdate(Content content);
        Content GetById(int id);

    }
}
