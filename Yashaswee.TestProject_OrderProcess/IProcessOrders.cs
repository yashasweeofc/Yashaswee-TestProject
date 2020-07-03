using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yashaswee.ProcessOrdersBRE
{
    public interface IProcessOrdersBRE
    {
        string PaymentAction(string paymentTypeAction);
    }
}
