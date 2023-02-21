using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.View_Model
{
    public class MainViewModel
    {
        public TableViewModel TableViewModel { get; set; } = new TableViewModel();
        public MoreInformationViewModel MoreInformationViewModel { get; set; } = new MoreInformationViewModel();
        

    }
}
