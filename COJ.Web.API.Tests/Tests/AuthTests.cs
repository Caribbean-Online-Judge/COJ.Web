using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using COJ.Web.API.Tests.Factories;
using COJ.Web.API.Tests.Utils;
using COJ.Web.Domain.Models;
using COJ.Web.Domain.Values;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Xunit;
using HttpContent = COJ.Web.API.Tests.Utils.HttpContent;

namespace COJ.Web.API.Tests.Tests;

public class AuthTests : IClassFixture<WebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory _factory;

    public AuthTests(
        WebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Post_SignInFailed()
    {
        //Content
        var payload = new SignInModel
        {
            Password = "123",
            UsernameOrEmail = "cl8dep"
        };

        // Serialize our concrete class into a JSON String
        var stringPayload = JsonConvert.SerializeObject(payload);

        // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
        var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        // Arrange
        var response = await _client.PostAsync("/v1/auth/sign-in", httpContent);
        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            var body = await response.Content.ReadAsStringAsync();
            FileUtils.LogInternalServerError(body);
        }

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task Post_SignInSuccess()
    {
        //Content
        var payload = new SignInModel
        {
            Password = "COJUser123",
            UsernameOrEmail = "cl8dep@gmail.com"
        };

        // Serialize our concrete class into a JSON String
        var stringPayload = JsonConvert.SerializeObject(payload);

        // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
        var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        // Arrange
        var response = await _client.PostAsync("/v1/auth/sign-in", httpContent);
        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            var body = await response.Content.ReadAsStringAsync();
            FileUtils.LogInternalServerError(body);
        }

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Post_SignUp()
    {
        //Content
        var payload = new SignUpRequest()
        {
            Email = "cl8dep@gmail.com",
            Sex = Sex.Male,
            Birthday = DateTime.UtcNow,
            Password = "COJUser123",
            Username = "cl8dep",
            CountryId = 1,
            FirstName = "Arael David",
            InstitutionId = 1,
            LanguageId = 1,
            LastName = "Espinosa",
            LocaleId = 1
        };

        var httpContent = HttpContent.Get(payload);

        // Arrange
        var response = await _client.PostAsync("/v1/auth/sign-up", httpContent);
        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            var body = await response.Content.ReadAsStringAsync();
            FileUtils.LogInternalServerError(body);
        }

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}