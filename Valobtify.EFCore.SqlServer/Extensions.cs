using Microsoft.EntityFrameworkCore;
using Valobtify.EFCore.Core;

namespace Valobtify.EFCore.SqlServer;

public static class Extensions
{
    public static ModelBuilder SetupSingleValueObjects(
        this ModelBuilder modelBuilder,
        Action<SingleValueObjectConfig>? configurationAction = null)
    {
        return Configurator.SetupOwnedSingleValueObjects<OwnedSingleValueObjectConfigurator>
            (modelBuilder, configurationAction);
    }
}