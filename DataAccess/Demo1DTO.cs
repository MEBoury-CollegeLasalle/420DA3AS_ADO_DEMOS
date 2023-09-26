using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
