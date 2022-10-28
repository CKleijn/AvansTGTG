namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IStudentService _studentService;
        private readonly ICanteenEmployeeService _canteenEmployeeService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IStudentService studentService, ICanteenEmployeeService canteenEmployeeService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentService = studentService;
            _canteenEmployeeService = canteenEmployeeService;
        }

        public IActionResult Login() => View();

        [ValidateAntiForgeryToken]
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
                        return RedirectToAction("Index", "Packet");

                    ModelState.AddModelError("WrongPassword", "Wachtwoord is onjuist!");
                }
                else
                {
                    ModelState.AddModelError("NoUser", "Er bestaat geen gebruiker met deze gegevens!");
                }
            }

            return View(loginModel);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Packet");
        }

        public IActionResult Register() => View();

        public IActionResult RegisterStudent() => View("Student/Register");

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RegisterStudent(StudentRegisterViewModel studentRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = studentRegisterViewModel.StudentNumber,
                    EmailConfirmed = true
                };

                var student = new Student
                {
                    Name = $"{studentRegisterViewModel.FirstName} {studentRegisterViewModel.LastName}",
                    DateOfBirth = studentRegisterViewModel.DateOfBirth,
                    StudentNumber = studentRegisterViewModel.StudentNumber,
                    EmailAddress = studentRegisterViewModel.EmailAddress,
                    StudyCity = studentRegisterViewModel.StudyCity,
                    PhoneNumber = studentRegisterViewModel.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, studentRegisterViewModel.Password);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Role", "Student"));

                if (result.Succeeded)
                {
                    try
                    {
                        await _studentService.CreateStudentAsync(student);
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "De student is niet aangemaakt!")
                            ModelState.AddModelError("StudentNotCreated", e.Message);
                    }
                    await _signInManager.PasswordSignInAsync(user, studentRegisterViewModel.Password, false, false);
                    return RedirectToAction("Index", "Packet");
                }
                else
                {
                    ModelState.AddModelError("AlreadyUser", "Er bestaat al een account met dit studentennummer!");
                }
            }

            return View("Student/Register", studentRegisterViewModel);
        }

        public IActionResult RegisterCanteenEmployee() => View("CanteenEmployee/Register");

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RegisterCanteenEmployee(CanteenEmployeeRegisterViewModel canteenEmployeeRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = canteenEmployeeRegisterViewModel.EmployeeNumber,
                    EmailConfirmed = true
                };

                var canteenEmployee = new CanteenEmployee
                {
                    Name = $"{canteenEmployeeRegisterViewModel.FirstName} {canteenEmployeeRegisterViewModel.LastName}",
                    EmployeeNumber = canteenEmployeeRegisterViewModel.EmployeeNumber,
                    Location = canteenEmployeeRegisterViewModel.Location
                };

                var result = await _userManager.CreateAsync(user, canteenEmployeeRegisterViewModel.Password);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Role", "CanteenEmployee"));
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Location", canteenEmployeeRegisterViewModel.Location.ToString()!));

                if (result.Succeeded)
                {
                    try
                    {
                        await _canteenEmployeeService.CreateCanteenEmployeeAsync(canteenEmployee);
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "De kantine medewerker is niet aangemaakt!")
                            ModelState.AddModelError("CanteenEmployeeNotCreated", e.Message);
                    }
                    await _signInManager.PasswordSignInAsync(user, canteenEmployeeRegisterViewModel.Password, false, false);
                    return RedirectToAction("Index", "Packet");
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
