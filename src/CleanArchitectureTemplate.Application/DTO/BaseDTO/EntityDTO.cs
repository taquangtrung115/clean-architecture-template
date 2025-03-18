using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTO.BaseDTO;
public abstract class EntityDTO<TKey>
{
    public TKey Id { get; set; }
}