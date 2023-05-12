namespace ExamManagement.API.Models;

public class MongoDBSettings
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
}
