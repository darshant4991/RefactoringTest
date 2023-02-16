using System;
using App.Interfaces;
namespace App.Repository
{
    public class CustomerCreditService : ICreditCardServices
    {
        public int GetCreditLimit(string firstname, string surname, System.DateTime dateOfBirth)
        {
            // DO NOT CHANGE THIS METHOD - Implementation logic is not important
            return 0;
        }
    }
}
