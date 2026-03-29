using pwd_checker_api.Features.PasswordValidate.Application.DTOs;
using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.UseCases
{
    public class PasswordValidateUseCase : IPasswordValidateUseCase
    {
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
            var handlerChain = 
                new MinLengthHandler()
                  .SetNext(new DigitHandler()); 

            return handlerChain;
        }
    }
}