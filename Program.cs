using System;
using Lab_csharp;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var students = new List<Student>
                {
                    new Student { StudentId = 1, LastName = "Kovach", GroupName = "Group A" },
                    new Student { StudentId = 2, LastName = "Trykur", GroupName = "Group B" },
                    new Student { StudentId = 3, LastName = "Feysa", GroupName = "Group A" },
                    new Student { StudentId = 4, LastName = "Kobal", GroupName = "Group C" },
                };

            var groups = new List<Group>
                {
                    new Group { GroupName = "Group A", Faculty = "IT", Course = 2 },
                    new Group { GroupName = "Group B", Faculty = "IT", Course = 1 },
                    new Group { GroupName = "Group C", Faculty = "Physics", Course = 2 },
                };

            var clubs = new List<Club>
                {
                    new Club { ClubName = "Chess", StudentId = 1 },
                    new Club { ClubName = "Chess", StudentId = 2 },
                    new Club { ClubName = "Basketball", StudentId = 3 },
                    new Club { ClubName = "Volleyball", StudentId = 4 },
                    new Club { ClubName = "Music", StudentId = 2 },
                    new Club { ClubName = "Music", StudentId = 1 },
                };

            //a
            Console.WriteLine("\nУсі гуртки, крім N гуртків з найменшою кількістю учасників-другокурсників. (N=1)");

            int N = 1;

            var secondYearStudents = students
            .Join(groups, s => s.GroupName, g => g.GroupName, (s, g) => new { s.StudentId, g.Course })
            .Where(sg => sg.Course == 2)
            .Select(sg => sg.StudentId);

            var clubCounts = clubs
            .Where(c => secondYearStudents.Contains(c.StudentId))
            .GroupBy(c => c.ClubName)
            .Select(g => new { ClubName = g.Key, Count = g.Count() })
            .OrderBy(g => g.Count);

            var clubsToDisplay = clubCounts.Skip(N);

            foreach (var club in clubsToDisplay)
            {
                Console.WriteLine($"Гурток: {club.ClubName}, Кількість другокурсників: {club.Count}");
            }

            //b
            var targetClubs = new List<string> { "Chess", "Music" };

            var firstYearITStudents = students
            .Join(groups, s => s.GroupName, g => g.GroupName, (s, g) => new { s.StudentId, g.Faculty, g.Course })
            .Where(sg => sg.Course == 1 && sg.Faculty == "IT")
            .Select(sg => sg.StudentId);

            var countFirstYearITInClubs = clubs
            .Where(c => firstYearITStudents.Contains(c.StudentId) && targetClubs.Contains(c.ClubName))
            .Select(c => c.StudentId)
            .Distinct()
            .Count();

            Console.WriteLine($"Загальна кількість студентів 1-го курсу ІТ, які займаються у зазначених гуртках: {countFirstYearITInClubs}");

            //c
            Console.WriteLine("\nНазви груп, для яких кількість різних гуртків, у яких займаються студенти цих груп, є найбільшою.");

            var groupClubCounts = students
            .Join(clubs, s => s.StudentId, c => c.StudentId, (s, c) => new { s.GroupName, c.ClubName })
            .GroupBy(g => g.GroupName)
            .Select(group => new
            {
                GroupName = group.Key,
                UniqueClubCount = group.Select(g => g.ClubName).Distinct().Count()
            })
            .OrderByDescending(g => g.UniqueClubCount);

            var maxClubCount = groupClubCounts.First().UniqueClubCount;
            var groupsWithMaxClubs = groupClubCounts.Where(g => g.UniqueClubCount == maxClubCount);

            foreach (var group in groupsWithMaxClubs)
            {
                Console.WriteLine($"Група: {group.GroupName}, Унікальні гуртки: {group.UniqueClubCount}");
            }
        }
    }
}
