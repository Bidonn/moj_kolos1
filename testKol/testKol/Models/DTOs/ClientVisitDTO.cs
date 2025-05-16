namespace testKol.Models.DTOs;

public class ClientVisitDTO
{
   public DateTime Date { get; set; }
   public Client? Client { get; set; } = null;
   public Mechanic? Mechanic { get; set; } = null;
   public List<VisitServ> VisitServices { get; set; } = [];
}

public class Client
{
   public string FirstName { get; set; } = string.Empty;
   public string LastName { get; set; } = string.Empty;
   public DateTime DateOfBirth { get; set; }
}
public class Mechanic
{ 
   public int MechanicId { get; set; }
   public string LicenceNumber { get; set; }
}

public class VisitServ
{
   public string Name { get; set; } = string.Empty;
   public decimal ServiceFee { get; set; }
}