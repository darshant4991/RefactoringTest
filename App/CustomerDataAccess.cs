﻿using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using App.Models;
namespace App
{
    /*
     *	DO NOT CHANGE THIS CLASS - THE CLASS AND ITS METHOD MUST REMAIN STATIC
    */
    public static class CustomerDataAccess
    {
        public static void AddCustomer(Customer customer)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspAddCustomer"
                };

                var firstNameParameter = new SqlParameter("@Firstname", SqlDbType.VarChar, 50) { Value = customer.Firstname };
                command.Parameters.Add(firstNameParameter);
                var surnameParameter = new SqlParameter("@Surname", SqlDbType.VarChar, 50) { Value = customer.Surname };
                command.Parameters.Add(surnameParameter);
                var dateOfBirthParameter = new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = customer.DateOfBirth };
                command.Parameters.Add(dateOfBirthParameter);
                var emailAddressParameter = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 50) { Value = customer.EmailAddress };
                command.Parameters.Add(emailAddressParameter);
                var hasCreditLimitParameter = new SqlParameter("@HasCreditLimit", SqlDbType.Bit) { Value = customer.HasCreditLimit };
                command.Parameters.Add(hasCreditLimitParameter);
                var creditLimitParameter = new SqlParameter("@CreditLimit", SqlDbType.Int) { Value = customer.CreditLimit };
                command.Parameters.Add(creditLimitParameter);
                var clientIdParameter = new SqlParameter("@ClientId", SqlDbType.Int) { Value = customer.Client.Id };
                command.Parameters.Add(clientIdParameter);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
