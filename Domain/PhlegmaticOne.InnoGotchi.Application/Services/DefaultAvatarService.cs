using PhlegmaticOne.InnoGotchi.Domain.Services;

namespace PhlegmaticOne.InnoGotchi.Application.Services;

public class DefaultAvatarService : IDefaultAvatarService
{
    private const string AvatarsPath = "Resources\\NoAvatar.png";

    public Task<byte[]> GetDefaultAvatarDataAsync(CancellationToken cancellationToken = new())
    {
        var noAvatarFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AvatarsPath);
        return File.ReadAllBytesAsync(noAvatarFile, cancellationToken);
    }
}