// Allows use of ObservableCollection for UI data binding
using System.Collections.ObjectModel;

// Allows access to database logic (SQLite operations)
using SDC440_3._3PA.DataAccess;

// Allows access to the Item model class
using SDC440_3._3PA.Models;

namespace SDC440_3._3PA;

// MainPage inherits from ContentPage (MAUI page)
public partial class MainPage : ContentPage
{
    // Object used to interact with the SQLite database
    private readonly ItemData itemData;

    // Collection bound to the CollectionView to display stored items
    public ObservableCollection<Item> Items { get; set; } = new();

    // Constructor runs when the page loads
    public MainPage()
    {
        InitializeComponent(); // Loads XAML UI components

        itemData = new ItemData(); // Initialize database access class
        BindingContext = this;     // Allows XAML to bind to Items property

        _ = UpdateItemsList();     // Load items from database when app starts
    }

    // Event handler that runs when the Save button is clicked
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Validate that all fields contain data
        if (string.IsNullOrWhiteSpace(txtItemId.Text) ||
            string.IsNullOrWhiteSpace(txtItemName.Text) ||
            string.IsNullOrWhiteSpace(txtItemDescription.Text))
        {
            await DisplayAlert("Error", "Please enter values for all fields.", "OK");
            return; // Stop execution if validation fails
        }

        // Validate that Item ID is a valid integer
        if (!int.TryParse(txtItemId.Text.Trim(), out int itemId))
        {
            await DisplayAlert("Error", "Item ID must be a valid number.", "OK");
            return; // Stop execution if number is invalid
        }

        // Create a new Item object using the user's input
        var item = new Item
        {
            ItemId = itemId,
            ItemName = txtItemName.Text.Trim(),
            ItemDescription = txtItemDescription.Text.Trim()
        };

        // Save the item to the database
        await itemData.SaveItemAsync(item);

        // Clear input fields after saving
        txtItemId.Text = "";
        txtItemName.Text = "";
        txtItemDescription.Text = "";

        // Refresh the list so the new item appears immediately
        await UpdateItemsList();
    }

    // Retrieves all items from the database and updates the UI list
    private async Task UpdateItemsList()
    {
        var items = await itemData.GetItemsAsync(); // Get all records

        Items.Clear(); // Clear current list
        foreach (var i in items)
        {
            Items.Add(i); // Add each item to ObservableCollection (updates UI)
        }
    }
}
