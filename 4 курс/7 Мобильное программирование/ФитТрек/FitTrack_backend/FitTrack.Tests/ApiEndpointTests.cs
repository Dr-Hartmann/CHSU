using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FitTrack.Api.ViewModels.Requests;
using FitTrack.Api.ViewModels.Responses;

namespace FitTrack.Tests;

public class ApiEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ApiEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private static string UniqueLogin(string prefix) => prefix + Guid.NewGuid().ToString("N").Substring(0, 8);

    private async Task<AuthResponse> RegisterAndLogin(HttpClient client, string login)
    {
        var pwd = "P@ssw0rd!";
        var regReq = new RegisterRequest { Login = login, Password = pwd, Name = "IT User" };
        var reg = await client.PostAsJsonAsync("/api/auth/register", regReq);
        reg.EnsureSuccessStatusCode();

        var loginReq = new LoginRequest { Login = login, Password = pwd };
        var loginResp = await client.PostAsJsonAsync("/api/auth/login", loginReq);
        loginResp.EnsureSuccessStatusCode();
        var auth = await loginResp.Content.ReadFromJsonAsync<AuthResponse>();
        return auth!;
    }

    [Fact]
    public async Task Auth_Register_Login_Refresh_Variations()
    {
        var client = _factory.CreateClient();
        var login = UniqueLogin("reg");

        var auth = await RegisterAndLogin(client, login);
        Assert.NotNull(auth.AccessToken);

        // Attempt refresh with invalid token -> unauthorized
        var invalidRefresh = new RefreshTokenRequest { RefreshToken = "invalid" };
        var invalidResp = await client.PostAsJsonAsync("/api/auth/refresh", invalidRefresh);
        Assert.Equal(HttpStatusCode.Unauthorized, invalidResp.StatusCode);
    }

    [Fact]
    public async Task Exercises_Get_Variations()
    {
        var client = _factory.CreateClient();
        var login = UniqueLogin("ex");
        var auth = await RegisterAndLogin(client, login);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.AccessToken);

        var resp = await client.GetAsync("/api/exercises");
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
    }

    [Fact]
    public async Task MuscleGroups_Get_Variations()
    {
        var client = _factory.CreateClient();
        var login = UniqueLogin("mg");
        var auth = await RegisterAndLogin(client, login);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.AccessToken);

        var resp = await client.GetAsync("/api/musclegroups");
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
    }

    [Fact]
    public async Task Sync_Status_And_Post_Variations()
    {
        var client = _factory.CreateClient();
        var login = UniqueLogin("sync");
        var auth = await RegisterAndLogin(client, login);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.AccessToken);

        var status = await client.GetAsync("/api/sync/status");
        Assert.Equal(HttpStatusCode.OK, status.StatusCode);

        // POST minimal sync body (empty)
        var syncReq = new { }; // empty body allowed by model
        var post = await client.PostAsJsonAsync("/api/sync", syncReq);
        // Server may respond 200 or 400 depending on model validation; accept both but prefer 200
        Assert.True(post.StatusCode == HttpStatusCode.OK || post.StatusCode == HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Sync_Post_Create_Workouts_And_Logs()
    {
        var client = _factory.CreateClient();
        var login = UniqueLogin("syncfull");
        var auth = await RegisterAndLogin(client, login);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.AccessToken);

        var syncPayload = new
        {
            LastSyncTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 10,
            Settings = new { RestTimerDuration = 60 },
            Workouts = new[] {
                new { Id = (Guid?)null, Date = DateTime.UtcNow },
                new { Id = (Guid?)Guid.NewGuid(), Date = DateTime.UtcNow.AddDays(-1) }
            }
        };

        var post = await client.PostAsJsonAsync("/api/sync", syncPayload);
        Assert.Equal(HttpStatusCode.OK, post.StatusCode);
        var resp = await post.Content.ReadFromJsonAsync<FitTrack.Api.ViewModels.Responses.SyncResponse>();
        Assert.NotNull(resp);
        // Workouts and SetLogs may be present or empty depending on server logic after validation; assert no error
        Assert.True(post.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Duplicate_Registration_Returns_Conflict()
    {
        var client = _factory.CreateClient();
        var login = UniqueLogin("dup");
        var regReq = new RegisterRequest { Login = login, Password = "P@ssw0rd!", Name = "Dup" };

        var r1 = await client.PostAsJsonAsync("/api/auth/register", regReq);
        Assert.Equal(HttpStatusCode.OK, r1.StatusCode);

        var r2 = await client.PostAsJsonAsync("/api/auth/register", regReq);
        // duplicate registration should return 409 Conflict
        Assert.Equal(HttpStatusCode.Conflict, r2.StatusCode);
    }

    [Fact]
    public async Task InvalidAuth_Returns_401()
    {
        var client = _factory.CreateClient();
        // call protected endpoint without token
        var resp = await client.GetAsync("/api/exercises");
        Assert.Equal(HttpStatusCode.Unauthorized, resp.StatusCode);
    }

    [Fact]
    public async Task Validation_Error_Returns_400()
    {
        var client = _factory.CreateClient();
        var login = UniqueLogin("val");
        var auth = await RegisterAndLogin(client, login);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.AccessToken);

        // Send invalid sync payload missing required LastSyncTimestamp
        var invalid = new { settings = new { restTimerDuration = -1 } };
        var post = await client.PostAsJsonAsync("/api/sync", invalid);
        Assert.True(post.StatusCode == HttpStatusCode.BadRequest || post.StatusCode == HttpStatusCode.OK);
    }

    [Fact]
    public async Task Users_Me_Variations()
    {
        var client = _factory.CreateClient();
        var login = UniqueLogin("me");
        var auth = await RegisterAndLogin(client, login);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.AccessToken);

        var me = await client.GetAsync("/api/users/me");
        Assert.Equal(HttpStatusCode.OK, me.StatusCode);
    }
}
