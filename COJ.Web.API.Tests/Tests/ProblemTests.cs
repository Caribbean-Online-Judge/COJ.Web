using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using COJ.Web.API.Tests.Factories;
using COJ.Web.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace COJ.Web.API.Tests.Tests;

public class ProblemTests
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory _factory;

    public ProblemTests(WebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async void CreateProblem()
    {
        //Content
        var signInPayload = new SignInModel
        {
            Password = "COJUser123",
            UsernameOrEmail = "cl8dep@gmail.com"
        };
        var signInStringPayload = JsonConvert.SerializeObject(signInPayload);
        var signInContent = new StringContent(signInStringPayload, Encoding.UTF8, "application/json");

        var signInResponse = await _client.PostAsync("/v1/auth/sign-in", signInContent);
        var signInBody = await signInResponse.Content.ReadAsStringAsync();
        var token = ((dynamic) JsonConvert.DeserializeObject(signInBody)).token;

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var description = @"### Description

For this problem, you must calculate A + B, numbers given in the input.

### Input specification

The only line of input contains two space separated integers A, B (0 <= A, B <= 10).

### Output specification
The only line of output should contain one integer: the sum of A and B.

#### Sample input
1 2

#### Sample output
3";
        var createProblemPayload = new CreateProblemRequest()
        {
            Author = "Arael David",
            Title = "A+B Problem",
            Description = description,
            Multidata = false,
            Points = 0.1,
            ClassificationId = 1,
            MemoryLimit = 0,
            OutputLimit = 0,
            SizeLimit = 64,
            SpecialJudge = false,
            TimeLimit = 0,
            CaseTimeLimit = 0,
        };
        var createProblemStringPayload = JsonConvert.SerializeObject(createProblemPayload);
        var createProblemContent = new StringContent(createProblemStringPayload, Encoding.UTF8, "application/json");

        var createProblemResponse = await _client.PostAsync("/v1/problem", createProblemContent);
        var body = await createProblemResponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Created, createProblemResponse.StatusCode);
    }
}