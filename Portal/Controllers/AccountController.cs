namespace Portal.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                }
            }

            ModelState.AddModelError("InvalidLogin", "Verkeerde identificatienummer of wachtwoord!");

            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
                    UserName = studentRegisterViewModel.StudentNumber
                };

                var result = await _userManager.CreateAsync(user, studentRegisterViewModel.Password);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Student", "Student"));

                var student = new Student
                {
                    Name = $"{studentRegisterViewModel.FirstName} {studentRegisterViewModel.LastName}",
                    DateOfBirth = studentRegisterViewModel.DateOfBirth,
                    StudentNumber = studentRegisterViewModel.StudentNumber,
                    EmailAddress = studentRegisterViewModel.EmailAddress,
                    StudyCity = studentRegisterViewModel.StudyCity,
                    PhoneNumber = studentRegisterViewModel.PhoneNumber
                };
                // First need to have a service!
                //await _studentRepository.CreateStudentAsync(student);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
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
                    UserName = canteenEmployeeRegisterViewModel.EmployeeNumber
                };

                var result = await _userManager.CreateAsync(user, canteenEmployeeRegisterViewModel.Password);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("CanteenEmployee", "CanteenEmployee"));

                var canteenEmployee = new CanteenEmployee
                {
                    Name = $"{canteenEmployeeRegisterViewModel.FirstName} {canteenEmployeeRegisterViewModel.LastName}",
                    EmployeeNumber = canteenEmployeeRegisterViewModel.EmployeeNumber,
                    Location = $"{canteenEmployeeRegisterViewModel.City}, {canteenEmployeeRegisterViewModel.Canteen}"
                };
                // First need to have a service!
                //await _canteenEmployeeRepository.CreateCanteenEmployeeAsync(canteenEmployee);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("CanteenEmployee/Register", canteenEmployeeRegisterViewModel);
        }
    }
}
