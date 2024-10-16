using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using Xunit;
using Xunit.Categories;

namespace InsuranceClaimSystem.API.Tests.IntegrationTests;

[IntegrationTest]
public class ClaimControllerTests
{
    [Fact]
    public async Task Get_ReturnsHttpOk()
    {
        // Arrange
        await using var webApplicationFactory = new WebApplicationFactory<Program>();

        using var httpClient = webApplicationFactory.CreateClient();

        // Act
        using var act = await httpClient.GetAsync("/api/claims");

        // Assert
        act.EnsureSuccessStatusCode();
        act.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

