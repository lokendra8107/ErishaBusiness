using FluentValidation;
using System;
using System.Collections.Generic;
using ErishaBusiness.Models.Model;

namespace ErishaBusiness.Models.Validation
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private static Dictionary<Type, IValidator> _validators = new Dictionary<Type, IValidator>();

        static ValidatorFactory()
        {
           
        } 

        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator;

            if (_validators.TryGetValue(validatorType, out validator))
                return validator;
            return validator;
        }
    }
    public class CustomLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public CustomLanguageManager()
        {
            AddTranslation("en", "NotNullValidator", "'{PropertyName}' is required.");
        }
    }
}