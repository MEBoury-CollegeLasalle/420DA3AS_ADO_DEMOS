using System.Text.Json;

namespace TestPatates.DataAccess;
internal class Demo1DTO {
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? DateCreated { get; set; }


    public override string ToString() {
        string json = JsonSerializer.Serialize(this);
        JsonDocument doc = JsonDocument.Parse(json);
        return JsonSerializer.Serialize(doc, new JsonSerializerOptions() { WriteIndented = true });
    }

}
