public class Equipment
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string  Location { get; set; }
    public required string Quantity { get; set; }
    public required decimal Value { get; set; }
    public required int Conditions { get; set; }
    public required string Status { get; set; }
    public required DateTime InstalledDate { get; set; }
}