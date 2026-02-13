using System.Collections.ObjectModel;
using SDC440_3._3PA.DataAccess;
using SDC440_3._3PA.Models;

namespace SDC440_3._3PA;

public partial class MainPage : ContentPage
{
    private readonly ItemData itemData;

    public ObservableCollection<Item> Items { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();

        itemData = new ItemData();
        BindingContext = this;

        _ = UpdateItemsList();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtItemId.Text) ||
            string.IsNullOrWhiteSpace(txtItemName.Text) ||
            string.IsNullOrWhiteSpace(txtItemDescription.Text))
        {
            await DisplayAlert("Error", "Please enter values for all fields.", "OK");
            return;
        }

        if (!int.TryParse(txtItemId.Text.Trim(), out int itemId))
        {
            await DisplayAlert("Error", "Item ID must be a valid number.", "OK");
            return;
        }

        var item = new Item
        {
            ItemId = itemId,
            ItemName = txtItemName.Text.Trim(),
            ItemDescription = txtItemDescription.Text.Trim()
        };

        await itemData.SaveItemAsync(item);

        txtItemId.Text = "";
        txtItemName.Text = "";
        txtItemDescription.Text = "";

        await UpdateItemsList();
    }

    private async Task UpdateItemsList()
    {
        var items = await itemData.GetItemsAsync();

        Items.Clear();
        foreach (var i in items)
        {
            Items.Add(i);
        }
    }
}
