using System.Collections.Generic;
using System;

namespace CrudAdoDotNet
{
    public interface IPetShopDal{
        IEnumerable<PetShop> GetAll();
        void Add(PetShop petshop);
        void Update(PetShop petshop);
        IEnumerable<PetShop> GetByName(string name);
        void Delete(Guid? id);
    }
}