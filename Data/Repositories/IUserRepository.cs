using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<string> GetPhone();
        IEnumerable<string> GetNickName();
    }
}
