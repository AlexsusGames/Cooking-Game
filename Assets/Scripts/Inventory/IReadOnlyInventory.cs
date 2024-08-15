using System;

public interface IReadOnlyInventory 
{
    string OwnerId { get; }
    bool Has(string itemId, int amount);
}
