using Common.Models;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

public class UserController : Controller
{
    private readonly IRepository _repo;

    public UserController(IRepository repository)
    {
        _repo = repository;    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User model)
    {
        if (ModelState.IsValid)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.PasswordHash))
            {
                ModelState.AddModelError("", "Email and password are required");
                return View(model);
            }

            // Generate the password hash
            string passwordHash = HashPassword(model.PasswordHash);

            // Save the hash in the model
            model.PasswordHash = passwordHash;

            _repo.Add(model);
            _repo.SaveChanges();
            return RedirectToAction(nameof(Login));
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(User model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.PasswordHash))
        {
            ModelState.AddModelError("", "Username and password are mandatory.");
            return View(model);
        }     

        var user = await _repo.GetAllUsersAsync(model.Username);

        if (user == null)
        {
            ModelState.AddModelError("", "User not found.");
            return View(model);
        }

        string passwordHash = HashPassword(model.PasswordHash);

        if (passwordHash != model.PasswordHash)
        {
            ModelState.AddModelError("", "Incorrect password.");
            return View(model);
        }

        // Correct password, perform user login
        return RedirectToAction("Home");
    }
    private string HashPassword(string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(passwordBytes);
        StringBuilder passwordHash = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            passwordHash.Append(hashBytes[i].ToString("X2"));
        }
        return passwordHash.ToString();
    }
}
