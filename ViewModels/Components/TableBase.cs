using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNest.ViewModels.Components
{
    public class TableBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
