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
            employeeRepository.ItemAdded += EmployeeRepository_ItemAdded;

            AddEmployees(employeeRepository);
            AddManagers(employeeRepository);
            AddLeaders(employeeRepository);
            WriteAllToConsole(employeeRepository);

            var organizationRepository = new ListRepository<Organization>();
            AddOrganizations(organizationRepository);
            WriteAllToConsole(organizationRepository);

            Console.ReadLine(); 
        }

        private static void EmployeeRepository_ItemAdded(object? sender, Employee employee)
        {
            Console.WriteLine($"Employee added => {employee.FirstName}");
        }

        private static void AddEmployees(IRepository<Employee> employeeRepository)
        {
            var employees = new[]
            {
                new Employee { FirstName = "Julia" },
                new Employee { FirstName = "Anna" },
                new Employee { FirstName = "Thomas" },
                new Employee { FirstName = "Black" }
            };
            employeeRepository.AddBatch(employees);
        }

        private static void AddManagers(IWriteRepository<Manager> managerRepository)
        {
            var saraManager = new Manager { FirstName = "Sara" };
            var saraManagerCopy = saraManager.Copy();
            managerRepository.Add(saraManager);
            
            if(saraManagerCopy is not null)
            {
                saraManagerCopy.FirstName += "_Copy";
                managerRepository.Add(saraManagerCopy);
            }
            managerRepository.Add(new Manager { FirstName = "Lucy" });
            managerRepository.Add(new Manager { FirstName = "Dana" });
            managerRepository.Save();
        }

        private static void AddLeaders(IWriteRepository<Leader> leaderRepository)
        {   
            leaderRepository.Add(new Leader { FirstName = "Dome" });
            leaderRepository.Add(new Leader { FirstName = "Tony" });
            leaderRepository.Add(new Leader { FirstName = "White" });
            leaderRepository.Save();
        }

        private static void AddOrganizations(IRepository<Organization> organizationRepository)
        {
            var organizations = new[]
            {
                new Organization { Name = "Pluralsight" },
                new Organization { Name = "Globomantics" },
                new Organization { Name = "Udemy" }
            };
            organizationRepository.AddBatch(organizations);
        }

        private static void WriteAllToConsole(IReadRepository<IEntity> repository)
        {
            foreach (var item in repository.GetAll()) Console.WriteLine(item);
        }

        private static void GetEmployeeById(IRepository<Employee> employeeRepository) => Console.WriteLine($@"Employee with Id 2 :  
        {employeeRepository.GetById(2).FirstName}");
    }
}
