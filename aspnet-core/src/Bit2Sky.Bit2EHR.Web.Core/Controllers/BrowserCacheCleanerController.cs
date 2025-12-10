using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bit2Sky.Bit2EHR.Notifications;

namespace Bit2Sky.Bit2EHR.Web.Controllers;

public class BrowserCacheCleanerController : Bit2EHRControllerBase
{
    private readonly INotificationAppService _notificationAppService;

    public BrowserCacheCleanerController(INotificationAppService notificationAppService)
    {
        _notificationAppService = notificationAppService;
    }

    public async Task<IActionResult> Clear()
    {
        var result = await _notificationAppService.SetAllAvailableVersionNotificationAsRead();

        HttpContext.Response.Headers.Append("Clear-Site-Data", "\"cache\"");

        return Json(new { Result = result });
    }
}