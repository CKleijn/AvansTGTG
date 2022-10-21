using Core.DomainServices.Interfaces.Services;
using Portal.Models.AccountVM;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private IStudentService _studentService;
        private ICanteenEmployeeService _canteenEmployeeService;

        public AccountController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IStudentService studentService, 
            ICanteenEmployeeService canteenEmployeeService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentService = studentService;
            _canteenEmployeeService = canteenEmployeeService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginModel.IdentificationNumber);

                if(user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user!, loginModel.Password, false, false)).Succeeded)
                        return RedirectToAction("AllPackets", "Packet");

                    ModelState.AddModelError("WrongPassword", "Wachtwoord is onjuist!");
                }
                else
                {
                    ModelState.AddModelError("NoUser", "Er bestaat geen gebruiker met deze gegevens!");
                }
            }

            return View(loginModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Packet");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpGet]
        public IActionResult RegisterStudent() => View("Student/Register");

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(StudentRegisterViewModel studentRegisterViewModel)
        {
            if (studentRegisterViewModel.DateOfBirth > DateTime.Now)
                ModelState.AddModelError("DateNotPossible", "Deze datum is onmogelijk!");

            if (studentRegisterViewModel.DateOfBirth > DateTime.Now.AddYears(-16))
                ModelState.AddModelError("AgeUnder16", "Je moet 16 jaar of ouder zijn voor een account!");

            if (studentRegisterViewModel.Password?.Length < 8)
                ModelState.AddModelError("PasswordToShort", "Het wachtwoord moet minimaal 8 tekens bevatten!");

            if(studentRegisterViewModel.Password != null)
                if (!studentRegisterViewModel.Password!.Any(c => char.IsUpper(c)))
                    ModelState.AddModelError("PasswordNoUpper", "Het wachtwoord moet minimaal 1 hoofdletter bevatten");

            if (studentRegisterViewModel.Password != null)
                if (!studentRegisterViewModel.Password!.Any(c => char.IsNumber(c)))
                    ModelState.AddModelError("PasswordNoNumber", "Het wachtwoord moet minimaal 1 cijfer bevatten");

            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = studentRegisterViewModel.StudentNumber,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, studentRegisterViewModel.Password);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Role", "Student"));

                var student = new Student
                {
                    Name = $"{studentRegisterViewModel.FirstName} {studentRegisterViewModel.LastName}",
                    DateOfBirth = studentRegisterViewModel.DateOfBirth,
                    StudentNumber = studentRegisterViewModel.StudentNumber,
                    EmailAddress = studentRegisterViewModel.EmailAddress,
                    StudyCity = studentRegisterViewModel.StudyCity,
                    PhoneNumber = studentRegisterViewModel.PhoneNumber
                };

                if (result.Succeeded)
                {
                    await _studentService.CreateStudentAsync(student);
                    await _signInManager.PasswordSignInAsync(user, studentRegisterViewModel.Password, false, false);
                    return RedirectToAction("AllPackets", "Packet");
                }
                else
                {
                    ModelState.AddModelError("AlreadyUser", "Er bestaat al een account met dit studentennummer!");
                }
            }

            return View("Student/Register", studentRegisterViewModel);
        }

        [HttpGet]
        public IActionResult RegisterCanteenEmployee() => View("CanteenEmployee/Register");

        [HttpPost]
        public async Task<IActionResult> RegisterCanteenEmployee(CanteenEmployeeRegisterViewModel canteenEmployeeRegisterViewModel)
        {
            if (canteenEmployeeRegisterViewModel.Password?.Length < 8)
                ModelState.AddModelError("PasswordToShort", "Het wachtwoord moet minimaal 8 tekens bevatten!");

            if (canteenEmployeeRegisterViewModel.Password != null)
                if (!canteenEmployeeRegisterViewModel.Password!.Any(c => char.IsUpper(c)))
                    ModelState.AddModelError("PasswordNoUpper", "Het wachtwoord moet minimaal 1 hoofdletter bevatten");

            if (canteenEmployeeRegisterViewModel.Password != null)
                if (!canteenEmployeeRegisterViewModel.Password!.Any(c => char.IsNumber(c)))
                    ModelState.AddModelError("PasswordNoNumber", "Het wachtwoord moet minimaal 1 cijfer bevatten");

            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = canteenEmployeeRegisterViewModel.EmployeeNumber,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, canteenEmployeeRegisterViewModel.Password);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Role", "CanteenEmployee"));
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Location", canteenEmployeeRegisterViewModel.Location.ToString()!));

                var canteenEmployee = new CanteenEmployee
                {
                    Name = $"{canteenEmployeeRegisterViewModel.FirstName} {canteenEmployeeRegisterViewModel.LastName}",
                    EmployeeNumber = canteenEmployeeRegisterViewModel.EmployeeNumber,
                    Location = canteenEmployeeRegisterViewModel.Location
                };

                if (result.Succeeded)
                {
                    await _canteenEmployeeService.CreateCanteenEmployeeAsync(canteenEmployee);
                    await _signInManager.PasswordSignInAsync(user, canteenEmployeeRegisterViewModel.Password, false, false);
                    return RedirectToAction("AllPackets", "Packet");
                } 
                else
                {
                    ModelState.AddModelError("AlreadyUser", "Er bestaat al een account met dit personeelsnummer!");
                }
            }

            return View("CanteenEmployee/Register", canteenEmployeeRegisterViewModel);
        }
    }
}
