using MainLibrary.Model;
using Valit;

namespace ConsoleApp
{
    public static class Validator
    {
        public static IValitRules<UserModel> GetUserValidator()
        {
            var Validator = ValitRules<UserModel>
                .Create()
                .Ensure(m => m.Email, _=>_
                    .Required()
                    .Email())
                .Ensure(m => m.Password, _=>_ 
                    .Required()
                    .MinLength(10))
                .Ensure(m => m.Age, _=>_
                    .IsGreaterThan(16));

            return Validator;
        }
    }
}