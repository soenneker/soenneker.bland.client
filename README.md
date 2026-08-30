[![](https://img.shields.io/nuget/v/soenneker.bland.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.bland.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.bland.client/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.bland.client/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.bland.client.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.bland.client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.bland.client/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.bland.client/actions/workflows/codeql.yml)

# Soenneker.Bland.Client

Provides a cached, authenticated `HttpClient` for Bland.ai's v1 API.

## Installation

```bash
dotnet add package Soenneker.Bland.Client
```

## Configuration

```json
{
  "Bland": {
    "ApiKey": "your-api-key",
    "EncryptedKey": "your-encrypted-key"
  }
}
```

`Bland:ApiKey` is required and is sent in the `authorization` header. `Bland:EncryptedKey` is optional; when present, it is sent in the `encrypted_key` header. Keep both values in user secrets, environment variables, or a secret store rather than committed configuration.

## Registration

```csharp
using Soenneker.Bland.Client.Registrars;

services.AddBlandClientUtilAsSingleton();
```

`AddBlandClientUtilAsScoped()` is also available. Both registrations use the singleton HTTP-client cache.

## Usage

```csharp
using Soenneker.Bland.Client.Abstract;

public sealed class BlandApiTransport
{
    private readonly IBlandClientUtil _clientProvider;

    public BlandApiTransport(IBlandClientUtil clientProvider)
    {
        _clientProvider = clientProvider;
    }

    public async Task<HttpResponseMessage> Send(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        HttpClient client = await _clientProvider.Get(cancellationToken);
        return await client.SendAsync(request, cancellationToken);
    }
}
```

The client base address is `https://api.bland.ai/v1/`. Pass relative paths such as `calls` so requests stay under that API root.

## Lifetime behavior

- `Get()` creates the named client on first use and returns it afterward.
- Do not dispose the returned `HttpClient` per request.
- Disposing `IBlandClientUtil` removes and disposes its named client from the cache.
- Configuration is captured when the provider is constructed and is not refreshed on an existing instance.
- Prefer the higher-level `Soenneker.Bland.Calls` or `Soenneker.Bland.Suite` packages when you want typed operations rather than raw HTTP.
