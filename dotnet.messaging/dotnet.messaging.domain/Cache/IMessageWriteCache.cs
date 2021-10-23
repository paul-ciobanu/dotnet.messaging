using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet.messaging.domain.Cache
{
    public interface IMessageWriteCache
    {
        void Add(Message message);
    }
}
