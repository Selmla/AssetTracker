# AssetTracker

## Project Overview
This console app is a simple Asset Tracking starter project for school.
It stores assets like laptops and phones, accepts user input, and prints data back in a sorted and annotated form.
The app models `Asset` objects with properties (model, price, purchase date) and can track end-of-life.

## Features implemented
- User menu:
  1. Add asset
  2. Show assets
  3. Exit
- Asset types:
  - `Laptop` (subclass of `Asset`)
  - `Phone` (subclass of `Asset`)
- Asset service:
  - Add asset
  - List all assets
- Input validation:
  - `decimal.TryParse` for price
  - `DateTime.TryParse` in loop for date until valid
- Display output:
  - Sorted by type (laptops first, phones second)
  - Then by purchase date ascending
  - Mark with `(RED)` if asset is within 90 days of `purchaseDate + 3 years`

## File structure
- `Program.cs` - Main loop + menu + methods: `AddAsset`, `ShowAssets`
- `Asset.cs` - Base asset class
- `AssetService.cs` - `AddAsset`, `GetAllAssets`
- `Laptop.cs`, `Phone.cs` - Asset subclasses
- `.gitignore` - includes `objective.md` and build artifacts

## How it works
1. Start program (`dotnet run`)
2. Menu shown
3. Add asset:
   - choose type (`1= Laptop`, `2=Phone`)
   - enter model
   - enter price
   - enter purchase date (`yyyy-MM-dd`)
4. Show assets:
   - list sorted by type and date
   - each asset shows type, model, price, purchase date, and `(RED)` if near 3-year EOL

## Requirements coverage
- Level 1:
  - classes/objects with properties
  - console input/output
  - separate asset types
- Level 2:
  - list creation
  - sorted list: class primary, date secondary
  - 3-year threshold + 90-day warning

## Notes
- `3 years` lifecycle used in logic.
- `distanceTo3Year <= TimeSpan.FromDays(90)` activates RED flag.
- `AssetService` holds runtime in-memory list only (no persistence yet).
- Extend: add Computer base class, new asset types, CSV save/load, queries, update/remove.
