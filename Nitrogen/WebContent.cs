using Nitrogen.Settings;
using System.Net;

namespace Nitrogen;

public class WebContent(WebContentSettings Settings)
{
    private readonly WebContentSettings _settings = Settings;

    /// <summary>
    /// Checks if the specified URL returns 200 (STATUS: OK).
    /// </summary>
    /// <param name="Url">The URL to GET.</param>
    /// <returns>Whether <paramref name="Url"/> leads to a valid resource.</returns>
    public async Task<bool> ValidUrl(string Url)
    {
        if (string.IsNullOrWhiteSpace(Url))
            return false;

        using var client = new HttpClient();

        client.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
        HttpResponseMessage message = await client.GetAsync(Url);

        return message.IsSuccessStatusCode;
    }

    /// <summary>
    /// Gets the <see cref="HttpStatusCode"/> of the requested resource.
    /// </summary>
    /// <param name="Url">The URL to GET.</param>
    /// <returns>The corresponding <see cref="HttpStatusCode"/> from the requested resource.</returns>
    public async Task<HttpStatusCode> GetUrlStatusCode(string Url)
    {
        if (!await ValidUrl(Url))
            return HttpStatusCode.BadRequest;

        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
        HttpResponseMessage message = await client.GetAsync(Url);

        if (message.IsSuccessStatusCode)
            return message.StatusCode;

        return HttpStatusCode.BadRequest;
    }

    /// <summary>
    /// Gets an async <see cref="Stream"/> of the requested resource.
    /// </summary>
    /// <param name="Url">The resource to GET.</param>
    /// <returns>A <see cref="Stream"/> of the requested resource.</returns>
    public async Task<Stream?> GetContentAsStream(string Url)
    {
        if (!await ValidUrl(Url))
            return null;

        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
        HttpResponseMessage message = await client.GetAsync(Url);

        if (message.IsSuccessStatusCode)
            return await message.Content.ReadAsStreamAsync();

        return null;
    }

    /// <summary>
    /// Gets an async <see cref="byte"/> array of the requested resource.
    /// </summary>
    /// <param name="Url">The resource to GET.</param>
    /// <returns>A <see cref="byte"/> array of the requested resource.</returns>
    public async Task<byte[]?> GetContentAsByteArray(string Url)
    {
        if (!await ValidUrl(Url))
            return null;

        using var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(_settings.Timeout);
        HttpResponseMessage message = await client.GetAsync(Url);

        if (message.IsSuccessStatusCode)
            return await message.Content.ReadAsByteArrayAsync();

        return null;
    }
}
