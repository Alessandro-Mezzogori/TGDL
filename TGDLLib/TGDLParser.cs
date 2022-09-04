using Microsoft.Extensions.Logging;

namespace TGDLLib
{
    public class TGDLParser
    {
        private readonly ILogger<TGDLParser> _logger;
        private readonly TGDLTokenizer _tokenizer;
        public TGDLParser(ILogger<TGDLParser> logger, TGDLTokenizer tokenizer)
        {
            _tokenizer = tokenizer;
            _logger = logger;
        }

        public async Task<GameUnit> ParseAsync(Stream stream, long filesize, CancellationToken cancellationToken = default)
        {
            // Tokenizer / Abstract Syntax Tree Creation
            _logger.LogInformation("Creating Abstract Syntax Tree");
            var syntaxTree = await _tokenizer.ParseAsync(stream, cancellationToken);

            // Analysis of the AST for syntax errors
            _logger.LogInformation("Checking for syntax errors ?");

            // Creation of the GameUnit from AST
            _logger.LogInformation("Creating {0}", nameof(GameUnit));

            return new GameUnit();
        }
    }
}
