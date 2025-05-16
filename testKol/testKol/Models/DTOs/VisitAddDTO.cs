namespace testKol.Models.DTOs;

public class VisitAddDTO
{
    public int VisitId { get; set; }
    public int ClientId { get; set; }
    public List<Service> Services { get; set; }
}

public class Service
{
    public string Name { get; set; } = string.Empty;
    public decimal ServiceFee { get; set; }
}