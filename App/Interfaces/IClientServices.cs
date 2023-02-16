using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Models;

namespace App.Interfaces
{
    public interface IClientServices
    {
        public Client GetById(int id);
    }
}
