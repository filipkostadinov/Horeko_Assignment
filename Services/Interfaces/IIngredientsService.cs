using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace Services.Interfaces
{
    public interface IIngredientsService
    {
        public IEnumerable<IngredientsUsageViewModel> GetIngredientsUsage();
    }
}
