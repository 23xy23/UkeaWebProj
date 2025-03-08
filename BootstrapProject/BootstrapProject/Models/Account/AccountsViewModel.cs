using System.Collections.Generic;
using BootstrapProject.Models;

namespace BootstrapProject.Models
{
    public class AccountsViewModel
    {
        public IEnumerable<AccountEntries> AccountEntries { get; set; }
        public IEnumerable<CashFlow> CashFlow { get; set; }
    }
}
