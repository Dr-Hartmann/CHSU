using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FitTrack.Api.ViewModels.Requests;
using FitTrack.Api.ViewModels.Responses;

namespace FitTrack.Tests;

public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Register_Login_GetExercises_SyncStatus_GetCurrentUser()
    {
        var client = _factory.CreateClient();

        // 1) Register
        var registerReq = new RegisterRequest { Login = "inttestuser", Password = "P@ssw0rd!", Name = "Integration User" };
        var regResp = await client.PostAsJsonAsync("/api/auth/register", registerReq);
        Assert.True(regResp.IsSuccessStatusCode, await regResp.Content.ReadAsStringAsync());

        // 2) Login
        var loginReq = new LoginRequest { Login = "inttestuser", Password = "P@ssw0rd!" };
        var loginResp = await client.PostAsJsonAsync("/api/auth/login", loginReq);
        Assert.True(loginResp.IsSuccessStatusCode, await loginResp.Content.ReadAsStringAsync());
        var auth = await loginResp.Content.ReadFromJsonAsync<AuthResponse>();
        Assert.NotNull(auth?.AccessToken);

        // attach bearer
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth!.AccessToken);

        // 3) Get exercises (should return 200)
        var exercisesResp = await client.GetAsync("/api/exercises");
        Assert.Equal(HttpStatusCode.OK, exercisesResp.StatusCode);

        // 4) Sync status
        var statusResp = await client.GetAsync("/api/sync/status");
        Assert.Equal(HttpStatusCode.OK, statusResp.StatusCode);

        // 5) Get current user
        var meResp = await client.GetAsync("/api/users/me");
        Assert.Equal(HttpStatusCode.OK, meResp.StatusCode);
    }
}
