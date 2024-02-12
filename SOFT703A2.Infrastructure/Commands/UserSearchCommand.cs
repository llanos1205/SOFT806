namespace SOFT703A2.Infrastructure.Commands;

public class UserSearchCommand
{
    public string userName { get; set; } = "";
    public bool visitsCheckbox { get; set; } = false;
    public bool emailCheckbox { get; set; } = false;
    public bool phoneCheckbox { get; set; } = false;
    public string sortBy { get; set; } = null;
}