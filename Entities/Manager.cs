namespace WiredBrainCoffee.StorageApp.Entities
{
    public class Manager : Employee
    {
        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}" + " ( Manager)";

    }
}
