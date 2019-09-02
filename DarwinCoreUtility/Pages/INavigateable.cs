using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinCoreUtility.Pages
{
    public interface INavigateable
    {
        string ButtonName { get; }
        string ButtonTag { get; }
    }
}
