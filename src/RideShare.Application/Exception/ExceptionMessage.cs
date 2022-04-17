namespace RideShare.Application.Exception
{
    public class ExceptionMessage
    {
        public static string EntityNotFound(string entityName="Entity")
        {
            return $"{entityName} is not found.";
        }
        public static string EntityIsAlreadyExist(string entityName = "Entity")
        {
            return $"{entityName} is already exist.";
        }
    }
}