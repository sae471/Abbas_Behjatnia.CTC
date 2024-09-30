using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abbas_Behjatnia.Shared.Domain.Entities.Auditing;
public interface IHasCreationTime
{
    DateTime CreationTime { get; set; }
}