using System.Collections.Generic;
using System.Threading.Tasks;
using JibbleGroup.Model;

namespace JibbleGroup.Service
{
   
    public interface IPeopleService
    {
        Task<IEnumerable<People>> GetAsync(string filter);

        Task<People> GetOneAsync(string username);
    }
}
