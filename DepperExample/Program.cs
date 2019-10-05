using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DepperExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dapper demo!");

            Insert(new Person()
            {
                Id = Guid.NewGuid(),
                BirthDate = new DateTime(2000, 1, 2),
                Name = "Peter"
            });

            Insert(new Person()
            {
                Id = Guid.NewGuid(),
                BirthDate = DateTime.Now,
                Name = "Samuel"
            });

            Update(null, "Samuel");

            var persons = ReadAll();

            Console.ReadKey();
        }

        private static List<Person> ReadAll()
        {
            using (IDbConnection db = new SqlConnection("Server=localhost,1433;Database=IntegratedPlatformLocal;User Id=sa;Password = yourStrong(!)Password; "))
            {
                return db.Query<Person>("Select * From Person").ToList();
            }
        }

        private static void Insert(Person person)
        {
            var sql = "INSERT INTO PERSON VALUES (@id, @name, @birthdate)";

            using (IDbConnection db = new SqlConnection("Server=localhost,1433;Database=IntegratedPlatformLocal;User Id=sa;Password = yourStrong(!)Password; "))
            {
                db.Execute(sql, person);                
            }
        }

        private static void Update(DateTime? newBirthDate, string personName)
        {
            var sql = "UPDATE PERSON SET birthdate = @newBirthDate WHERE name = @personName";

            using (IDbConnection db = new SqlConnection("Server=localhost,1433;Database=IntegratedPlatformLocal;User Id=sa;Password = yourStrong(!)Password; "))
            {
                db.Execute(sql, new { newBirthDate, personName });
            }
        }
    }
}
