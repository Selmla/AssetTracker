class AssetService
{
    List<Asset> assets = new List<Asset>();

    public void AddAsset(Asset asset)
    {
        assets.Add(asset);
    }

    public List<Asset> GetAllAssets()
    {
        return assets;
    }
}
