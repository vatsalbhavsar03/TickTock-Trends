using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TickTockTrends_WEBAPI.Data;
using TickTockTrends_WEBAPI.DTO;
using TickTockTrends_WEBAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TickTockTrends_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TickTockDbContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(TickTockDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

      


        //GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisterUserDTO>>> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    UserId=u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNo = u.PhoneNo,
                    CreatedAt = u.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
                    UpdatedAt = u.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss")
                })
                .ToListAsync();

            return Ok(users);
        }


        //GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // Fetch Roles -----------
        [HttpGet("FetchRoles")]
        public async Task<ActionResult> FetchRoles()
        {
            var roles = await _context.Roles.Select(r =>
            new RoleDTO { RoleId = r.RoleId, RoleName = r.RoleName }).ToListAsync();

            return Ok(new
            {
                success = true,
                message = "roles fetch successfully.",
                Roles = roles,
            });
        }


        // POST: api/Users/Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == registerUserDto.Email))
                {
                    throw new Exception("Email Already Exists.");
                }

                // Convert UTC to Indian Standard Time (IST)
                TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

                var user = new User
                {
                    Name = registerUserDto.Name,
                    Email = registerUserDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                    PhoneNo = registerUserDto.PhoneNo,
                    RoleId = 2, // Always assign RoleId = 2
                    CreatedAt = indianTime,
                    UpdatedAt = indianTime
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Successfully registered",
                    user = new
                    {
                        user.UserId,
                        user.RoleId,  // Always 2
                        user.Name,
                        user.Email,
                        user.PhoneNo,
                        user.CreatedAt,
                        user.UpdatedAt
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


        //[HttpPost("Register")]
        //public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
        //{
        //    try
        //    {
        //        if (_context.Users.Any(u => u.Email == registerUserDto.Email))
        //        {
        //            throw new Exception("Email Already Exists.");
        //        }

        //        var user = new User
        //        {
        //            Name = registerUserDto.Name,
        //            Email = registerUserDto.Email,
        //            Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
        //            PhoneNo = registerUserDto.PhoneNo,
        //            RoleId = registerUserDto.RoleId,
        //            CreatedAt = DateTime.UtcNow, // Set CreatedAt timestamp
        //            UpdatedAt = DateTime.UtcNow  // Set UpdatedAt timestamp
        //        };

        //        _context.Users.Add(user);
        //        await _context.SaveChangesAsync();

        //        return Ok(new
        //        {
        //            success = true,
        //            message = "Successfully registered",
        //            user = new
        //            {
        //                user.UserId,
        //                user.RoleId,
        //                user.Name,
        //                user.Email,
        //                user.PhoneNo,
        //                user.CreatedAt,  // Include CreatedAt in response
        //                user.UpdatedAt   // Include UpdatedAt in response
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { success = false, message = ex.Message });
        //    }
        //}




        //// DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        //// send otp method
        //[HttpPost("SendOTP")]
        //public async Task<IActionResult> SendOtp([FromBody] SendOtpDto sendOtpDto)
        //{
        //    var otp = new Random().Next(100000, 999999).ToString();
        //    HttpContext.Session.SetString("otp", otp);
        //    HttpContext.Session.SetString("otpEmail", sendOtpDto.Email);

        //    return await SendEmail(sendOtpDto.Email, otp)
        //        ? Ok(new { success = true, message = "OTP sent successfully." })
        //        : BadRequest(new { success = false, message = "Failed to send OTP." });
        //}
        [HttpPost("SendOTP")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpDto sendOtpDto)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            // Store OTP and email in session
            HttpContext.Session.SetString("otp", otp);
            HttpContext.Session.SetString("otpEmail", sendOtpDto.Email);

            bool emailSent = await SendEmail(sendOtpDto.Email, otp);

            if (emailSent)
            {
                return Ok(new { success = true, message = "OTP sent successfully." });
            }

            return BadRequest(new { success = false, message = "Failed to send OTP." });
        }


        //[HttpPost("SendOTP")]
        //public async Task<ActionResult> SendOtp([FromBody] SendOtpDto sendOtpDto)
        //{
        //    // ✅ Ensure Session is Available
        //    if (HttpContext.Session == null)
        //    {
        //        return StatusCode(500, new { success = false, message = "Session is not available." });
        //    }

        //    var otp = new Random().Next(100000, 999999).ToString();
        //    HttpContext.Session.SetString("otp", otp);
        //    HttpContext.Session.SetString("otpEmail", sendOtpDto.Email);

        //    bool emailSent = await SendEmail(sendOtpDto.Email, otp);

        //    if (!emailSent)
        //    {
        //        return BadRequest(new { success = false, message = "Failed to send OTP." });
        //    }
        //    return Ok(new { success = true, message = "OTP sent successfully." });
        //}


        // Send Email
        private async Task<bool> SendEmail(string email, string otp)
        {
            try
            {
                string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "OtpTemplate.html");
                string emailBody = await System.IO.File.ReadAllTextAsync(templatePath);

                emailBody = emailBody.Replace("{{OTP}}", otp);

                using var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("bhavsarvatsal337@gmail.com", "tptg vvyt sgdv qgho"),
                    EnableSsl = true,
                };

                var mailMsg = new MailMessage
                {
                    From = new MailAddress("bhavsarvatsal337@gmail.com"),
                    Subject = "Your OTP code for Registration.",
                    Body = emailBody,
                    IsBodyHtml = true,

                };

                mailMsg.To.Add(email);
                await smtp.SendMailAsync(mailMsg);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        // verify otp functionality
        //[HttpPost("VerifyOTP")]
        //public async Task<ActionResult> VerifyOtp([FromBody] VerifyOtpDto verifyOtpDto)
        //{
        //    var sessionOtp = HttpContext.Session.GetString("otp");
        //    var sessionEmail = HttpContext.Session.GetString("otpEmail");

        //    if (sessionOtp == null || sessionEmail == null || sessionOtp != verifyOtpDto.Otp || sessionEmail != verifyOtpDto.Email)
        //    {
        //        return BadRequest(new
        //        {
        //            success = false,
        //            message = "Invalid Or Expired OTP."
        //        });
        //    }

        //    HttpContext.Session.Remove("otp");
        //    HttpContext.Session.Remove("otpEmail");

        //    return Ok(new
        //    {
        //        success = true,
        //        message = "OTP Verified Successfully."
        //    });
        //}
        [HttpPost("VerifyOTP")]
        public async Task<ActionResult> VerifyOtp([FromBody] VerifyOtpDto verifyOtpDto)
        {
            // Retrieve OTP and email from session
            var sessionOtp = HttpContext.Session.GetString("otp");
            var sessionEmail = HttpContext.Session.GetString("otpEmail");

            // Check if OTP or email session is null, or if they don't match
            if (sessionOtp == null || sessionEmail == null || sessionOtp != verifyOtpDto.Otp || sessionEmail != verifyOtpDto.Email)
            {
                return BadRequest(new { success = false, message = "Invalid Or Expired OTP." });
            }

            // Clear OTP and email from session after verification
            HttpContext.Session.Remove("otp");
            HttpContext.Session.Remove("otpEmail");

            return Ok(new { success = true, message = "OTP Verified Successfully." });
        }


        [HttpPost("login")]
        public async Task<ActionResult> login([FromBody] LoginUserDTO loginUserDto)
        {
            var user = _context.Users.Include(u => u.Role).Where(u => u.Email == loginUserDto.Email).FirstOrDefault();
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.Password))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid Email and Password"
                });
            }

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                success = true,
                message = "Login Successfull.",
                token = token,
                roleId = user.RoleId,
                redirectUrl = GetRedirectUrl(user.RoleId)
            });
        }

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("UserId", user.UserId.ToString()),
            new Claim("Role", user.Role.RoleName.ToString()),
            new Claim("RoleId", user.RoleId.ToString()),
        };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetRedirectUrl(int roleId)
        {
            return roleId switch
            {
                1 => "/admin/dashboard",
                2 => "/user/dashboard",
                _ => "/"
            };
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
