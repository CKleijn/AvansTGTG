namespace Portal.Extensions
{
    public class StudentExtensions
    {
        public class DateOfBirthValidation : ValidationAttribute
        {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                var student = (StudentRegisterViewModel)validationContext.ObjectInstance;

                if (student.DateOfBirth > DateTime.Now)
                    return new ValidationResult("Deze datum is onmogelijk!");

                if (student.DateOfBirth > DateTime.Now.AddYears(-16))
                    return new ValidationResult("Je moet 16 jaar of ouder zijn voor een account!");

                return ValidationResult.Success;
            }
        }
    }
}
