using System;
using System.Collections.Generic;
using connect;
using efficiency;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Npgsql;
using util;

namespace Solar_Panel.Pages;

public class AddResidenceModel : PageModel
{
    private readonly ILogger<AddResidenceModel> _logger;

    public AddResidenceModel(ILogger<AddResidenceModel> logger)
    {
        _logger = logger;
        
    }

    public void OnGet()
    {
    }
}
