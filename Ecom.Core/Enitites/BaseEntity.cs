using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Enitites
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
