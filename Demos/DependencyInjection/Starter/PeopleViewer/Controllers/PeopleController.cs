using Microsoft.AspNetCore.Mvc;
using PeopleViewer.Common;
using PersonDataReader.Service;

namespace PeopleViewer.Controllers;

public class PeopleController : Controller
{
    private readonly ServiceReader reader = new ServiceReader();

    public async Task<IActionResult> UseConstructorReader()
    {
        await Task.Delay(1);
        ViewData["Title"] = "Using Constructor Reader";
        //ViewData["ReaderType"] = reader.GetTypeName();

        List<Person> people = (await reader.GetPeople()).ToList();
        return View("Index", people);
    }

    public async Task<IActionResult> UseMethodReader([FromServices] IPersonReader methodReader)
    {
        await Task.Delay(1);
        ViewData["Title"] = "Using Method Reader";
        //ViewData["ReaderType"] = methodReader.GetTypeName();

        List<Person> people = (await reader.GetPeople()).ToList();
        return View("Index", people);
    }
}
