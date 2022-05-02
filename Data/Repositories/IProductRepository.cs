using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Data.Repositories
{
    public interface IProductRepository
    {
       // IEnumerable<string> GetAllProducts();
        IEnumerable<string> GetPizza();
        IEnumerable<string> GetDesserts();
        IEnumerable<string> GetDrinks();
        IEnumerable<string> GetSnacks();
        IEnumerable<string> GetSizePizza();
    }
}
