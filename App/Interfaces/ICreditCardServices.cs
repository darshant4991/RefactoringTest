using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface ICreditCardServices
    {
        int GetCreditLimit(string firstname, string surname, System.DateTime dateOfBirth);
    }
}
