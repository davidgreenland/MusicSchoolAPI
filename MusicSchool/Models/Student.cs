using Microsoft.AspNetCore.Mvc;

namespace MusicSchool.Models;

public class Student
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}
