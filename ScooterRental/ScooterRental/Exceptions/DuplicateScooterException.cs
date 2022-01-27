namespace ScooterRental.Exceptions
{
    public class DuplicateScooterException:System.Exception
    {
        public DuplicateScooterException():base("Scooter with provided id already exists")
        {
            
        }
    }
}
