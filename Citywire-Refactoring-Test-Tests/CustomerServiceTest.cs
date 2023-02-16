using App;
using App.Interfaces;
using Moq;

namespace Citywire_Refactoring_Test_UnitTests
{
    [TestClass]
    public class CustomerServiceTest
    {
        private CustomerService? _customerService;
        private Mock<IClientServices>? _clientRepository ;
        private Mock<ICreditCardServices>? _CreditCardService;

        [TestInitialize]
        public void Setup()
        {
            _clientRepository = new Mock<IClientServices>();
            _CreditCardService = new Mock<ICreditCardServices>();
            _customerService = new CustomerService(_clientRepository.Object, _CreditCardService.Object);
        }


        [TestMethod]
        public void IsValidEmail_ValidEmail_ReturnsTrue()
        {
            // Arrange
            string email = "john@example.com";

            // Act
            bool result = _customerService.IsValidEmail(email);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsValidEmail_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            string email = "john@example";

            // Act
            bool result = _customerService.IsValidEmail(email);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsUnderAge_AgeOver21_ReturnsFalse()
        {
            // Arrange
            DateTime dateOfBirth = DateTime.Now.AddYears(-25);

            // Act
            bool result = _customerService.IsUnderAge(dateOfBirth);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsUnderAge_AgeUnder21_ReturnsTrue()
        {
            // Arrange
            DateTime dateOfBirth = DateTime.Now.AddYears(-20);

            // Act
            bool result = _customerService.IsUnderAge(dateOfBirth);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddCustomer_ValidData_ReturnsTrue()
        {
            // Arrange
            string firstname = "John";
            string surname = "Doe";
            string email = "john.doe@example.com";
            DateTime dateOfBirth = new DateTime(1980, 1, 1);
            int clientId = 1;

            // Act
            var result = _customerService.AddCustomer(firstname, surname, email, dateOfBirth, clientId);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void AddCustomer_InvalidData_ReturnsFalse()
        {
            // Arrange
            string firstname = "";
            string surname = "Doe";
            string email = "johndoe";
            DateTime dateOfBirth = new DateTime(2010, 1, 1);
            int clientId = 1;

            // Act
            var result = _customerService.AddCustomer(firstname, surname, email, dateOfBirth, clientId);

            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void AddCustomer_InvalidClient_ReturnsFalse()
        {
            // Arrange
            string firstname = "John";
            string surname = "Doe";
            string email = "john.doe@example.com";
            DateTime dateOfBirth = new DateTime(1980, 1, 1);
            int clientId = -1;

            // Act
            var result = _customerService.AddCustomer(firstname, surname, email, dateOfBirth, clientId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}