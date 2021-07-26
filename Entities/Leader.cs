namespace WiredBrainCoffee.StorageApp.Entities
{
    public class Leader :  Manager
    {
        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}" + " ( Leader)";
    }
}
