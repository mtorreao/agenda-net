using AgendaNet.Domain.Entities;

namespace AgendaNet.Tests.Entities
{
  public class ContactTest
  {
    private const string ExpectedPhone = "81912341234";
    private const string ExpectedName = "Test name";
    private const string ExpectedEmail = "a@a.com";

    [Fact]
    public void Should_Create_A_Contact_With_Email()
    {
      var contact = new Contact("", ExpectedEmail, "", new DateTime(), new Guid());

      Assert.Equal(contact.Email, ExpectedEmail);
    }

    [Fact]
    public void Should_Create_A_Contact_With_Phone()
    {
      var contact = new Contact("", "", ExpectedPhone, new DateTime(), new Guid());

      Assert.Equal(contact.Phone, ExpectedPhone);
    }

    [Fact]
    public void Should_Create_A_Contact_With_Name()
    {
      var contact = new Contact(ExpectedName, "", "", new DateTime(), new Guid());

      Assert.Equal(contact.Name, ExpectedName);
    }
  }
}