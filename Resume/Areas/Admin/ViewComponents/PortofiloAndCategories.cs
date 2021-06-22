using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Areas.Admin.ViewComponents
{
    public class PortofiloAndCategories
    {
        public IEnumerable<Portfolio> Portfolios { get; set; }
        public IEnumerable<PortfolioCategory> Categories { get; set; }
    }
}
