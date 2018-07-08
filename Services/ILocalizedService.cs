using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techno.Core.Data.Entities;
using Techno.Core.DTO;

namespace Techno.Localization.Services
{
    public interface ILocalizedService<T, TModel> where T : BaseEntity where TModel : BaseDTO
    {
        void UpdateLocales(T entity, TModel model);
    }
}
