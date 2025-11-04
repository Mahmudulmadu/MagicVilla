using MagicVilla.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicVilla.Application.Common.Interface
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        void Update(Amenity entity);
    }
}
