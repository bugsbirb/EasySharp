public class Project
{
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; }
}


public class JsonData
{
        public required List<Project> Projects { get; set; }
}
