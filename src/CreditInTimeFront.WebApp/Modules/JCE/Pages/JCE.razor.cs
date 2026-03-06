using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CreditInTimeFront.WebApp.Modules.JCE.Pages;

public partial class JCE
{
    private string searchType = "cedula";
    private string _searchInput = "";
    private bool isLoading = false;
    private bool showResults = false;
    private bool showPreview = false;

    private int RequiredLength => searchType == "cedula" ? 13 : 11;
    private string Placeholder => searchType == "cedula" ? "000-0000000-0" : "000-00000-0";

    private string SearchInput
    {
        get => _searchInput;
        set
        {
            _searchInput = ApplyMask(value);
            if (_searchInput.Length < RequiredLength)
            {
                showResults = false;
                showPreview = false;
            }
        }
    }

    private void OnSearchTypeChanged(ChangeEventArgs e)
    {
        searchType = e.Value?.ToString() ?? "cedula";
        _searchInput = "";
        showResults = false;
        showPreview = false;
    }

    private string ApplyMask(string input)
    {
        var digits = new string(input.Where(char.IsDigit).ToArray());

        if (searchType == "cedula")
        {
            if (digits.Length > 11) digits = digits[..11];
            var fmt = digits.Length > 0 ? digits[..Math.Min(3, digits.Length)] : "";
            if (digits.Length > 3) fmt += "-" + digits[3..Math.Min(10, digits.Length)];
            if (digits.Length > 10) fmt += "-" + digits[10..11];
            return fmt;
        }
        else
        {
            if (digits.Length > 9) digits = digits[..9];
            var fmt = digits.Length > 0 ? digits[..Math.Min(3, digits.Length)] : "";
            if (digits.Length > 3) fmt += "-" + digits[3..Math.Min(8, digits.Length)];
            if (digits.Length > 8) fmt += "-" + digits[8..9];
            return fmt;
        }
    }

    private async Task Search()
    {
        if (_searchInput.Length < RequiredLength) return;
        isLoading = true;
        StateHasChanged();
        await Task.Delay(800);
        isLoading = false;
        showResults = true;
        showPreview = false;
    }

    private async Task OnKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter") await Search();
    }

    private void TogglePreview() => showPreview = !showPreview;

    private int CalculateAge(string birthDate)
    {
        var parts = birthDate.Split('/');
        if (parts.Length != 3) return 0;
        if (!int.TryParse(parts[0], out var day) ||
            !int.TryParse(parts[1], out var month) ||
            !int.TryParse(parts[2], out var year)) return 0;
        var today = DateTime.Today;
        var age = today.Year - year;
        if (today.Month < month || (today.Month == month && today.Day < day)) age--;
        return age;
    }
}
