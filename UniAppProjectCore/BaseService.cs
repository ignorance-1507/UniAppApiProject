using UniAppProjectCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAppProjectCore
{
    public class BaseService<T> : BaseRepository<T> where T : class, new()
    { }
}
