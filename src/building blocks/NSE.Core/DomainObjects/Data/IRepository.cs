﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NSE.Core.DomainObjects.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {

    }
}
