using Domain.Results;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public static class ControllerBaseExtensions
{
    public static ActionResult ToActionResult(this ControllerBase controller, Result result)
    {
        return result.IsSuccess
            ? controller.StatusCode((int)result.Status)
            : controller.Problem(result.ErrorMessage, statusCode: (int)result.Status);
    }

    public static ActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result)
    {
        return result.IsSuccess
            ? controller.StatusCode((int)result.Status, result.Value)
            : controller.Problem(result.ErrorMessage, statusCode: (int)result.Status);
    }
}
