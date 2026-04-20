namespace PatientsApi.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    var patientsSeed = new List<Patient>
{
    new Patient
    {
        PatientId = 1,
        DocumentType = "CC",
        DocumentNumber = "1001",
        FirstName = "Juan",
        LastName = "Perez",
        BirthDate = new DateTime(1990, 1, 1),
        PhoneNumber = "3001111111",
        Email = "juan@test.com",
        CreatedAt = new DateTime(2024, 01, 01)
    },
    new Patient
    {
        PatientId = 2,
        DocumentType = "CC",
        DocumentNumber = "1002",
        FirstName = "Maria",
        LastName = "Gomez",
        BirthDate = new DateTime(1992, 5, 10),
        PhoneNumber = "3002222222",
        Email = "maria@test.com",
        CreatedAt = new DateTime(2024, 02, 01)
    },
    new Patient
    {
        PatientId = 3,
        DocumentType = "CE",
        DocumentNumber = "2001",
        FirstName = "Carlos",
        LastName = "Lopez",
        BirthDate = new DateTime(1988, 8, 20),
        PhoneNumber = null,
        Email = null,
        CreatedAt = new DateTime(2024, 03, 01)
    }
};
}
