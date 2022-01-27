namespace ScooterRental.Exceptions
{
    public class ScooterNotFoundException: System.Exception
    {
        public ScooterNotFoundException():base ("Scooter not Found")
        {
            
        }
    }
}
