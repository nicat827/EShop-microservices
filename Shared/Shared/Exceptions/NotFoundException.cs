using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mess) : base(mess) { }
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) wasn't found!") { }
    }
}
