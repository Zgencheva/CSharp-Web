using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework.ViewEngine
{
    public interface IView
    {
        string ExecuteTemplate(object viewModel);

    }
}
