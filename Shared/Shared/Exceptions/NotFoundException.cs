using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Shared.Exceptions.Base;

namespace Shared.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string mess) : base(mess, HttpStatusCode.NotFound) { }
        public NotFoundException(string name, object key) : base($@"Entity '{name}' ({key}) wasn't found!", HttpStatusCode.NotFound) { }

    }
}
