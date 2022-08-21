using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirsMvcApp.ViewModels
{
    public class DoAddViewModel
    {
        public int Attack { get; set; }

        public int Health { get; set; }

        public int Damage => Attack * 10 + Health;
    }
}
