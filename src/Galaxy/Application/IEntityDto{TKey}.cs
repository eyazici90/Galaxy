using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Application
{
    public interface IEntityDto<TKey> : IEntityDto
    {
        TKey Id { get; set; }
    }
}
