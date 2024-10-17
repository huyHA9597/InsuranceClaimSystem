using Bogus;
using FluentAssertions;
using InsuranceClaimSystem.API.Features.Claims;
using InsuranceClaimSystem.API.Features.Claims.CreateClaim;
using InsuranceClaimSystem.API.Features.Claims.UpdateClaimStatus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit.Categories;

namespace InsuranceClaimSystem.API.Tests.IntegrationTests;

[IntegrationTest]
public class ClaimControllerTests
{
    [Fact]
    public async Task Get_AllClaims_ReturnsHttpOk()
    {
        // Arrange
        await using var webApplicationFactory = new WebApplicationFactory<Program>();
        using var httpClient = webApplicationFactory.CreateClient();

        // Act
        using var act = await httpClient.GetAsync("/api/claims");

        // Assert
        act.EnsureSuccessStatusCode();
        act.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = await act.Content.ReadFromJsonAsync<List<ClaimResponse>>();
        response.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_ValidStatus_ReturnsHttpOk()
    {
        // Arrange
        await using var webApplicationFactory = new WebApplicationFactory<Program>();
        using var httpClient = webApplicationFactory.CreateClient();

        // Act
        string claimStatus = "Pending";
        using var act = await httpClient.GetAsync($"/api/claims/{claimStatus}");

        // Assert
        act.EnsureSuccessStatusCode();
        act.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = await act.Content.ReadFromJsonAsync<List<ClaimResponse>>();
        response.Should().NotBeNull();
    }

    [Fact]
    public async Task Post_InvalidClaimRequest_ReturnsHttpBadRequest()
    {
        // Arrange
        var request = new CreateClaimRequest(string.Empty, string.Empty, 0, DateTime.Now);
        await using var webApplicationFactory = new WebApplicationFactory<Program>();
        using var httpClient = webApplicationFactory.CreateClient();

        // Act
        using var act = await httpClient.PostAsJsonAsync("/api/claims", request);

        // Assert
        act.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var response = await act.Content.ReadFromJsonAsync<ProblemDetails>();
        response.Should().NotBeNull();
        response.Title.Should().Be("One or more validation errors occurred.");
        response.Status.Should().Be(StatusCodes.Status400BadRequest);
        response.Extensions.Keys.Should().Contain("errors");
    }

    [Fact]
    public async Task Post_ValidClaimRequest_ReturnsHttpOk()
    {
        // Arrange
        var request = new Faker<CreateClaimRequest>()
            .RuleFor(request => request.Name, faker => faker.Person.FullName)
            .RuleFor(request => request.Description, faker => faker.Random.String2(4))
            .RuleFor(request => request.CreatedDate, faker => faker.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now))
            .RuleFor(request => request.Amount, faker => faker.Random.Number(1000))
            .Generate();
        await using var webApplicationFactory = new WebApplicationFactory<Program>();
        using var httpClient = webApplicationFactory.CreateClient();

        // Act
        using var act = await httpClient.PostAsJsonAsync("/api/claims", request);

        // Assert
        act.EnsureSuccessStatusCode();
        act.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Put_ValidClaimRequest_ReturnsHttpOk()
    {
        // Arrange
        var request = new Faker<CreateClaimRequest>()
            .RuleFor(request => request.Name, faker => faker.Person.FullName)
            .RuleFor(request => request.Description, faker => faker.Random.String2(4))
            .RuleFor(request => request.CreatedDate, faker => faker.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now))
            .RuleFor(request => request.Amount, faker => faker.Random.Number(1000))
            .Generate();
        await using var webApplicationFactory = new WebApplicationFactory<Program>();
        using var httpClient = webApplicationFactory.CreateClient();

        // Create claim
        await httpClient.PostAsJsonAsync("/api/claims", request);

        // Prepare request to update 
        var updateRequest = new UpdateClaimStatusRequest
        {
            Name = request.Name,
            Amount = request.Amount,
            CreatedDate = request.CreatedDate,
        };

        // Act
        using var act = await httpClient.PutAsJsonAsync("/api/claims", updateRequest);

        // Assert
        act.EnsureSuccessStatusCode();
        act.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = await act.Content.ReadFromJsonAsync<string>();
        response.Should().NotBeNull();
        response.Should().Contain("update with status");
    }
}

