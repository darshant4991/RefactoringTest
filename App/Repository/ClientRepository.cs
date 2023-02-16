using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using App.Interfaces;
using App.Models;
namespace App.Repository
{
    public class ClientRepository : IClientServices
    {
        public Client GetById(int id)
        {
            // DO NOT CHANGE THIS METHOD - Implementation logic is not important
            return new Client();
        }
    }
}
