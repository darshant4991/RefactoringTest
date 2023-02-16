using System;
using App.Interfaces;
using App.Models;
namespace App
{
    public class CustomerService
    {
        private readonly IClientServices _clientRepository;
        private readonly ICreditCardServices _customerCreditService;

        public CustomerService(IClientServices clientRepository, ICreditCardServices customerCreditService)
        {
            _clientRepository = clientRepository;
            _customerCreditService = customerCreditService;
        }

        public bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname) || !IsValidEmail(email) || IsUnderAge(dateOfBirth))
            {
                return false;
            }
            
            var client = _clientRepository.GetById(clientId);
            if (client == null)
            {
                return false;
            }

            var customer = CreateCustomer(firname, surname, email, dateOfBirth, client);
            if (client.Type == ClientType.Important || client.Type == ClientType.VeryImportant)
            {
                EvaluateCreditLimit(customer, client);
            }
            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }
            CustomerDataAccess.AddCustomer(customer);

            return true;
        }
        private void EvaluateCreditLimit(Customer customer, Client client)
        {
            if (client.Type == ClientType.VeryImportant)
            {
                // Skip credit check
                customer.HasCreditLimit = false;
            }
            else
            {
                customer.HasCreditLimit = true;
                customer.CreditLimit = _customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                if (client.Type == ClientType.Important)
                {
                    customer.CreditLimit *= 2;
                }
            }
        }

        public bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
        }
        public bool IsUnderAge(DateTime dateOfBirth)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age))
            {
                age--;
            }
            return age < 21;
        }
        private Customer CreateCustomer(string firstName, string surname, string email, DateTime dateOfBirth, Client client)
        {
            return new Customer
            {
                Firstname = firstName,
                Surname = surname,
                EmailAddress = email,
                DateOfBirth = dateOfBirth,
                Client = client
            };
        }
    }
}
