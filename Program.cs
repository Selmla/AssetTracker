Console.WriteLine("Welcome to Asset Tracker");

AssetService service = new AssetService();

while (true)
{
    Console.WriteLine("\n--- Main Menu ---");
    Console.WriteLine("1. Add asset");
    Console.WriteLine("2. Show assets");
    Console.WriteLine("3. Exit");
    Console.Write("\nSelect an option: ");

    string? choice = Console.ReadLine();

    if (string.IsNullOrEmpty(choice))
    {
        Console.WriteLine("Invalid input.");
        return;
    }

    switch (choice)
    {
        case "1":
            AddAsset(service);
            break;
        case "2":
            ShowAssets(service);
            break;
        case "3":
            Console.WriteLine("Exiting program...");
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

void AddAsset(AssetService service)
{
    Console.WriteLine("\n--- Add Asset ---");
    Console.WriteLine("1. Laptop");
    Console.WriteLine("2. Phone");
    Console.Write("Select asset type: ");

    string? assetType = Console.ReadLine();

    if (string.IsNullOrEmpty(assetType))
    {
        Console.WriteLine("Invalid input.");
        return;
    }

    Console.Write("Enter model name: ");
    string? modelName = Console.ReadLine();

    if (string.IsNullOrEmpty(modelName))
    {
        Console.WriteLine("Invalid model name.");
        return;
    }

    Console.Write("Enter price: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
    {
        Console.WriteLine("Invalid price entered.");
        return;
    }

    DateTime purchaseDate;
    while (true)
    {
        Console.Write("Enter purchase date (yyyy-MM-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out purchaseDate))
        {
            break;
        }
        Console.WriteLine("Invalid date entered. Please try again.");
    }

    Asset? asset = null;

    if (assetType == "1")
    {
        asset = new Laptop { ModelName = modelName, Price = price, PurchaseDate = purchaseDate };
    }
    else if (assetType == "2")
    {
        asset = new Phone { ModelName = modelName, Price = price, PurchaseDate = purchaseDate };
    }
    else
    {
        Console.WriteLine("Invalid asset type.");
        return;
    }

    if (asset != null)
    {
        service.AddAsset(asset);
        Console.WriteLine("Asset added successfully!");
    }
}

void ShowAssets(AssetService service)
{
    Console.WriteLine("\n--- All Assets ---");
    List<Asset> assets = service.GetAllAssets();

    if (assets.Count == 0)
    {
        Console.WriteLine("No assets found.");
        return;
    }

    var sortedAssets = assets
        .OrderBy(a => a is Phone ? 1 : 0)  // Laptops first (0), Phones second (1)
        .ThenBy(a => a.PurchaseDate)       // Then sort by purchase date
        .ToList();

    for (int i = 0; i < sortedAssets.Count; i++)
    {
        var asset = sortedAssets[i];
        var threeYearDate = asset.PurchaseDate.AddYears(3); //target date for 3 years after purchase
        var timeFromThreeYears = threeYearDate - DateTime.Now; //time from 3 years span to now
        string warning = timeFromThreeYears <= TimeSpan.FromDays(90)
            || timeFromThreeYears < TimeSpan.Zero
            ? " (RED)"
            : string.Empty;

        Console.WriteLine($"\nAsset {i + 1}:");
        Console.WriteLine($"  Type: {asset.GetType().Name}");
        Console.WriteLine($"  Model: {asset.ModelName}");
        Console.WriteLine($"  Price: ${asset.Price}");
        Console.WriteLine($"  Purchase Date: {asset.PurchaseDate:yyyy-MM-dd}{warning}");
    }
}