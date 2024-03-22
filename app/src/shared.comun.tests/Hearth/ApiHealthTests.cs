using Microsoft.Extensions.Diagnostics.HealthChecks;
using shared.comun.hearth.Logica;

namespace shared.comun.tests.Hearth;

public class ApiHealthTests
{
    [Fact]
    public async Task CheckHealthAsync_ReturnsHealthyResult()
    {
        // Arrange
        var apiHealth = new ApiHealth("Test Service");

        // Act
        var result = await apiHealth.CheckHealthAsync(new HealthCheckContext());

        // Assert
        Assert.Equal(HealthStatus.Healthy, result.Status);
    }
}