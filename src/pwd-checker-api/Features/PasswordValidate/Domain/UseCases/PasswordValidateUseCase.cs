using pwd_checker_api.Features.PasswordValidate.Application.DTOs;
using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.UseCases
{
    public class PasswordValidateUseCase(ILogger<PasswordValidateUseCase> logger) : IPasswordValidateUseCase
    {
        private readonly ILogger<PasswordValidateUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<PasswordValidateResult> ExecuteAsync(string password)
        {
            var validateChain = BuildValidateHandlerChain();
            var validateChainResult = await validateChain.ExecuteAsync(password);

            var result = new PasswordValidateResult
            {
                IsValid = validateChainResult.IsValid,
                Message = validateChainResult.ResultMessage
            };

            return result;
        }

        private static BaseHandler BuildValidateHandlerChain()
        {
            var handlerChain = new MinLengthHandler();
            
            handlerChain
                .SetNext(new NoRepeatCharHandler())
                .SetNext(new LowercaseHandler())
                .SetNext(new UppercaseHandler())
                .SetNext(new SpecialCharHandler())
                .SetNext(new DigitHandler());

            return handlerChain;
        }
    }
}