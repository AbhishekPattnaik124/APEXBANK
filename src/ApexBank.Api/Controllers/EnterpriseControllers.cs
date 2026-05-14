using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApexBank.Application.Interfaces;

namespace ApexBank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService) => _dashboardService = dashboardService;

        private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        /// <summary>Get the current customer's dashboard KPIs.</summary>
        [HttpGet("customer")]
        public async Task<IActionResult> CustomerDashboard()
        {
            var data = await _dashboardService.GetCustomerDashboardAsync(GetUserId());
            return Ok(data);
        }

        /// <summary>Admin dashboard with platform-wide metrics.</summary>
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            var data = await _dashboardService.GetAdminDashboardAsync();
            return Ok(data);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationsController(INotificationService notificationService) => _notificationService = notificationService;

        private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        /// <summary>Get all notifications for the current user.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationService.GetUserNotificationsAsync(GetUserId());
            return Ok(notifications);
        }

        /// <summary>Get unread notification count.</summary>
        [HttpGet("unread-count")]
        public async Task<IActionResult> UnreadCount()
        {
            var count = await _notificationService.GetUnreadCountAsync(GetUserId());
            return Ok(new { count });
        }

        /// <summary>Mark a single notification as read.</summary>
        [HttpPost("{id:guid}/read")]
        public async Task<IActionResult> MarkRead(Guid id)
        {
            var result = await _notificationService.MarkAsReadAsync(id);
            return result ? Ok(new { message = "Marked as read." }) : NotFound();
        }

        /// <summary>Mark all notifications as read.</summary>
        [HttpPost("read-all")]
        public async Task<IActionResult> MarkAllRead()
        {
            await _notificationService.MarkAllAsReadAsync(GetUserId());
            return Ok(new { message = "All notifications marked as read." });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        public AdminController(IApplicationDbContext context) => _context = context;

        /// <summary>Admin: List all users with pagination.</summary>
        [HttpGet("users")]
        public IActionResult GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var users = _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new
                {
                    u.Id, u.FirstName, u.LastName, u.Email,
                    u.PhoneNumber, u.Role, u.IsActive, u.KycStatus,
                    u.LastLoginAt, u.CreatedAt, u.IsLockedOut
                })
                .ToList();

            var total = _context.Users.Count();
            return Ok(new { data = users, total, page, pageSize });
        }

        /// <summary>Admin: Activate or deactivate a user.</summary>
        [HttpPost("users/{id:guid}/toggle-active")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();
            return Ok(new { message = $"User {(user.IsActive ? "activated" : "deactivated")}.", isActive = user.IsActive });
        }

        /// <summary>Admin: Approve user KYC.</summary>
        [HttpPost("users/{id:guid}/approve-kyc")]
        public async Task<IActionResult> ApproveKyc(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.KycStatus = "Verified";
            user.KycVerifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(new { message = "KYC approved." });
        }

        /// <summary>Admin: Unlock a locked-out user account.</summary>
        [HttpPost("users/{id:guid}/unlock")]
        public async Task<IActionResult> UnlockUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.IsLockedOut = false;
            user.FailedLoginAttempts = 0;
            user.LockoutEndAt = null;
            await _context.SaveChangesAsync();
            return Ok(new { message = "User account unlocked." });
        }

        /// <summary>Admin: View recent audit logs.</summary>
        [HttpGet("audit-logs")]
        public IActionResult GetAuditLogs([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var logs = _context.AuditLogs
                .OrderByDescending(a => a.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(logs);
        }
    }
}
