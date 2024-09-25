using System;
namespace Lab_csharp
{
    public class Student
    {
        public int StudentId { get; set; }
        public string LastName { get; set; }
        public string GroupName { get; set; }
    }

    public class Group
    {
        public string GroupName { get; set; }
        public string Faculty { get; set; }
        public int Course { get; set; }
    }

    public class Club
    {
        public string ClubName { get; set; }
        public int StudentId { get; set; }
    }

}

