using Microsoft.Extensions.Logging;
using TGDLLib.Constants;

namespace TGDLLib;

public class TGDLTokenizer
{
    private readonly ILogger<TGDLTokenizer> _logger;
    public TGDLTokenizer(ILogger<TGDLTokenizer> logger)
    {
        _logger = logger;
    }

    // Returns an AST
    public async Task<Tree> ParseAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(stream);

        return new Tree();
    }
 }
