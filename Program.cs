using System;
using WiredBrainCoffee.StorageApp.Repositories;
using WiredBrainCoffee.StorageApp.Entities;
using WiredBrainCoffee.StorageApp.Data;

namespace WiredBrainCoffee.StorageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var employeeRepository = new SqlRepository<Employee>(new StorageAppDbContext());
          
            AddEmployees(employeeRepository);
            AddManagers(employeeRepository);
            WriteAllToConsole(employeeRepository);

            
            var organizationRepository = new ListRepository<Organization>();
            AddOrganizations(organizationRepository);
           WriteAllToConsole(organizationRepository);
            
        }

        private static void AddManagers(IWriteRepository<Manager> managerRepository)
        {
            managerRepository.Add(new Manager { FirstName = "Sara" });
            managerRepository.Add(new Manager { FirstName = "Lucy" });
            managerRepository.Save();
        }

        private static void WriteAllToConsole(IReadRepository<IEntity> repository)
        {
            var items = repository.GetAll();
            foreach(var item in items)
            {
                Console.WriteLine(item);
            }
        }

        private static void GetEmployeeById(IRepository<Employee> employeeRepository)
        {
            var employee = employeeRepository.GetById(2);
            Console.WriteLine($"Employee with Id 2 :  {employee.FirstName} ");
        }

        private static void AddEmployees(IRepository<Employee> employeeRepository)
        {
            var employees = new[]
            {
                new Employee { FirstName = "Julia" },
                new Employee { FirstName = "Anna" },
                new Employee { FirstName = "Thomas" }
            };
            AddBatch(employeeRepository,employees);
        }

        private static void AddOrganizations(IRepository<Organization> organizationRepository)
        {
            var organizations = new[]
            {
                new Organization { Name = "Pluralsight" },
                new Organization { Name = "Globomantics" },
                new Organization { Name = "Udemy" }
            };
            AddBatch(organizationRepository, organizations);
        }

        private static void AddBatch<T>(IRepository<T> repository, T[] items) where T : IEntity
        {
            foreach(var item in items)
            {
                repository.Add(item);
            }

        }
    }
}
