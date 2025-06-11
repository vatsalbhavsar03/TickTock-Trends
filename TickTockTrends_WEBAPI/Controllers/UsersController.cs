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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

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

        // Fetch Roles 
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


        //GET: api/Users
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users
                .Where(u => u.RoleId == 2)
                .Select(u => new
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNo = u.PhoneNo,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt

                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                message = "Users fetched successfully.",
                users = users
            });
        }

        //GET: api/Admin
        [HttpGet("GetAdmin")]
        public async Task<ActionResult<IEnumerable<User>>> GetAdmin()
        {
            var Admin = await _context.Users
                .Where(u => u.RoleId == 1)
                .Select(u => new
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNo = u.PhoneNo
                })
                .ToListAsync();
            return Ok(new
            {
                success = true,
                message = "Admin fetched successfully.",
                Admin = Admin
            });
        }

        //GET: api/Users/5
        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var user = await _context.Users
                .Where(u => u.UserId == id && u.RoleId == 2)
                .Select(u => new
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNo = u.PhoneNo
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found."
                });
            }

            return Ok(new
            {
                success = true,
                message = "User fetched successfully.",
                user = user
            });
        }


        // POST: api/Users/Register
        //[HttpPost("Register")]
        //public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
        //{
        //    try
        //    {
        //        if (_context.Users.Any(u => u.Email == registerUserDto.Email))
        //        {
        //            throw new Exception("Email Already Exists.");
        //        }

        //        // Convert UTC to Indian Standard Time (IST)
        //        TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        //        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

        //        var user = new User
        //        {
        //            Name = registerUserDto.Name,
        //            Email = registerUserDto.Email,
        //            Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
        //            PhoneNo = registerUserDto.PhoneNo,
        //            RoleId = 2, 
        //            CreatedAt = indianTime,
        //            UpdatedAt = indianTime
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
        //                user.CreatedAt,
        //                user.UpdatedAt
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { success = false, message = ex.Message });
        //    }
        //}
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == registerUserDto.Email))
                {
                    return BadRequest(new { success = false, message = "Email Already Exists." });
                }

                TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

                var user = new User
                {
                    Name = registerUserDto.Name,
                    Email = registerUserDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                    PhoneNo = registerUserDto.PhoneNo,
                    RoleId = 2,
                    CreatedAt = indianTime,
                    UpdatedAt = indianTime
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Generate JWT token using your method
                var token = GenerateJwtToken(user);

                return Ok(new
                {
                    success = true,
                    message = "Successfully registered",
                    token,
                    user = new
                    {
                        user.UserId,
                        user.RoleId,
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


        // Verify OTP
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



        // POST: api/Users/Login
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

        //POST: api/Users/ForgotPassword
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var data = await _context.Users.FirstOrDefaultAsync(fp => fp.Email == forgotPasswordDTO.Email);
            if (data == null)
            {
                return NotFound(new { success = false, message = "User not found." });
            }

            var oldPasswordHash = data.Password;

            if (BCrypt.Net.BCrypt.Verify(forgotPasswordDTO.Password, oldPasswordHash))
            {
                return BadRequest(new { success = false, message = "New password cannot be same as old password." });
            }

            try
            {
                data.Password = BCrypt.Net.BCrypt.HashPassword(forgotPasswordDTO.Password);
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Password updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error in student: {ex.InnerException}");
            }
        }


        // GET: api/Users/5
        private string GenerateJwtToken(User user)
        {
            var key = Convert.FromBase64String(_configuration["Jwt:Key"]);

            // Make sure Role is not null
            string roleName = user.Role?.RoleName ?? "User"; // fallback if Role is null

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Role", roleName),
                new Claim("RoleId", user.RoleId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }





        // GET: api/Users/5
        private string GetRedirectUrl(int roleId)
        {
            return roleId switch
            {
                1 => "/admin/dashboard",
                2 => "/user/dashboard",
                _ => "/"
            };
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("AdminUpdateUser/{id}")]
        public async Task<IActionResult> AdminUpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.UserId)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "User ID mismatch."
                });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found."
                });
            }

            if (_context.Users.Any(u => u.Email == updatedUser.Email && u.UserId != id))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Email already exists."
                });
            }

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.PhoneNo = updatedUser.PhoneNo;
            user.RoleId = updatedUser.RoleId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while updating the user."
                });
            }

            return Ok(new
            {
                success = true,
                message = "User updated successfully.",
                user = new
                {
                    user.UserId,
                    user.Name,
                    user.Email,
                    user.PhoneNo,
                    user.RoleId
                }
            });
        }


        // DELETE: api/Users/5
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

        //[HttpGet("profile")]
        //[Authorize] // Requires authentication
        //public async Task<ActionResult> GetProfile()
        //{
        //    try
        //    {
        //        // Extract user ID from JWT token
        //        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        //        // Fetch user from database
        //        var user = await _context.Users
        //            .Where(u => u.UserId == userId)
        //            .Select(u => new
        //            {
        //                u.UserId,
        //                u.Name,
        //                u.Email,
        //                u.PhoneNo,
        //                u.RoleId,
        //                u.CreatedAt,
        //                u.UpdatedAt
        //            })
        //            .FirstOrDefaultAsync();

        //        if (user == null)
        //        {
        //            return NotFound(new { success = false, message = "User not found." });
        //        }

        //        return Ok(new
        //        {
        //            success = true,
        //            user
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { success = false, message = ex.Message });
        //    }
        //}

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult> GetProfile()
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (!int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token." });
                }

                var user = await _context.Users
                    .Where(u => u.UserId == userId)
                    .Select(u => new
                    {
                        u.UserId,
                        u.Name,
                        u.Email,
                        u.PhoneNo,
                        u.RoleId,
                        u.CreatedAt,
                        u.UpdatedAt
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound(new { success = false, message = "User not found." });
                }

                return Ok(new { success = true, user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }




        [HttpPut("update-profile")]
        [Authorize]
        public async Task<ActionResult> UpdateProfile([FromBody] UpdateProfileDTO updateDto)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (!int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token." });
                }

                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound(new { success = false, message = "User not found." });
                }

                // Update fields
                user.Name = updateDto.Name;
                user.PhoneNo = updateDto.PhoneNo;
                user.Email = updateDto.Email;

                // Update timestamp
                TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                user.UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indianTimeZone);

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Profile updated successfully",
                    user = new
                    {
                        user.UserId,
                        user.Name,
                        user.Email,
                        user.PhoneNo,
                        user.UpdatedAt
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
