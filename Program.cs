using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace DapperInClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion
            IDbConnection conn = new MySqlConnection(connString);
            
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);
            Console.WriteLine("Hello, here are the current departments");
            
            var depos = repo.GetAllDepartments();

            foreach (var depo in depos)
            {
                Console.WriteLine(depo.Name);
            }

            Console.WriteLine("Do you want to add a department?");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {   
                Console.WriteLine("What is the name of the new department?");
                var newDepartmentName = Console.ReadLine();
                repo.InsertDepartment(newDepartmentName);
                repo.GetAllDepartments();
            }
            Console.WriteLine("Have a great day.");
        }
    }
}
