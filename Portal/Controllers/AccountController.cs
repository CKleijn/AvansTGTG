using Core.DomainServices.Interfaces.Services;
using Portal.Models;

namespace Portal.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginModel.IdentificationNumber);

                if(user != null)
                {
                    await _signInManager.SignOutAsync();
                    if((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    } 
                    else
                    {
                        ModelState.AddModelError("WrongPassword", "Wachtwoord is onjuist!");
                    }
                } 
                else
                {
                    ModelState.AddModelError("NoUser", "Er bestaat geen gebruiker met deze gegevens!");
                }
            }

            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterStudent()
        {
            return View("Student/Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterStudent(StudentRegisterViewModel studentRegisterViewModel)
        {
            if(ModelState.IsValid)
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

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("AlreadyUser", "Er bestaat al een account met dit studentennummer!");
                }
            }

            return View("Student/Register", studentRegisterViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterCanteenEmployee()
        {
            return View("CanteenEmployee/Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCanteenEmployee(CanteenEmployeeRegisterViewModel canteenEmployeeRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = canteenEmployeeRegisterViewModel.EmployeeNumber,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, canteenEmployeeRegisterViewModel.Password);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Role", "CanteenEmployee"));

                var canteenEmployee = new CanteenEmployee
                {
                    Name = $"{canteenEmployeeRegisterViewModel.FirstName} {canteenEmployeeRegisterViewModel.LastName}",
                    EmployeeNumber = canteenEmployeeRegisterViewModel.EmployeeNumber,
                    City = canteenEmployeeRegisterViewModel.City,
                    Location = canteenEmployeeRegisterViewModel.Canteen
                };

                if (result.Succeeded)
                {
                    await _canteenEmployeeService.CreateCanteenEmployeeAsync(canteenEmployee);
                    await _signInManager.PasswordSignInAsync(user, canteenEmployeeRegisterViewModel.Password, false, false);
                    return RedirectToAction("Index", "Home");
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
