using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LTCSDL.DAL;
using LTCSDL.DTO;

namespace LTCSDL.BLL
{
    public class CategoryBLL
    {
        private CategoryDAL dal;

        public CategoryBLL()
        {
            dal = new CategoryDAL();
        }

        public List<Category> GetAll()
        {
            return dal.GetAll();
        }

        public int Insert(Category cat)
        {
            return dal.Insert(cat);
        }

        public int Update(Category cat)
        {
            return dal.Update(cat);
        }

        public int Delete(int id)
        {
            return dal.Delete(id);
        }
    }
}
