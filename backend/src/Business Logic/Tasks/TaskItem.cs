namespace Business_Logic.Tasks;

public class TaskItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public bool IsCompleted { get; private set; }

    public TaskItem(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Task name cannot be empty.");
        }

        Id = id;
        Name = name;
        IsCompleted = false;
    }

    public void Update(string name, bool isCompleted)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Task name cannot be empty.");
        }

        Name = name;
        IsCompleted = isCompleted;
    }
}