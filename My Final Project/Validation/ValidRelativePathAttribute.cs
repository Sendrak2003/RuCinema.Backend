using System;
using System.ComponentModel.DataAnnotations;

namespace My_Final_Project
{
    public class ValidRelativePathAttribute : ValidationAttribute
    {
        private readonly string _rootFolder;

        public ValidRelativePathAttribute(string rootFolder)
        {
            _rootFolder = rootFolder;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var path = value as string;

            if (path == null)
            {
                return new ValidationResult("Путь к файлу не может быть пустым.");
            }

            if (!path.StartsWith(_rootFolder))
            {
                return new ValidationResult($"Путь к файлу должен начинаться с '{_rootFolder}'.");
            }

            return ValidationResult.Success;
        }
    }
}