namespace SOFT703A2.Infrastructure.Commands;

public class ProductSearchCommand
{
    public string productName { get; set; } = "";
    public bool categoryCheckbox { get; set; } = false;
    public bool promotedCheckbox { get; set; } = false;
    public string sortBy { get; set; } = null;
}