using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
    //Dependency contrainer interface
    public interface IServiceCollection
    {
        //.Add<IUserService, UserService>
        void Add<TSourse, TDestination>();
        public object CreateInstance(Type type);
    }
}
