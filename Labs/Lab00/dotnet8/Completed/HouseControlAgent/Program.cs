﻿using HouseControl.Library;
using HouseControl.Sunset;

namespace HouseControlAgent;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Initializing Controller");

        var controller = InitializeHouseController();

        await Task.Delay(1); // placeholder to keep Main signature when test code is not used

        // For hardware/scheduling testing purposes
        // Uncomment this section to ensure that the hardware
        // and scheduling is working as expected.
        await controller.SendCommand(5, DeviceCommand.On);
        await controller.SendCommand(5, DeviceCommand.Off);

        var currentTime = DateTime.Now;
        controller.ScheduleOneTimeItem(currentTime.AddMinutes(1), 3, DeviceCommand.On);
        controller.ScheduleOneTimeItem(currentTime.AddMinutes(2), 5, DeviceCommand.On);
        controller.ScheduleOneTimeItem(currentTime.AddMinutes(3), 3, DeviceCommand.Off);
        controller.ScheduleOneTimeItem(currentTime.AddMinutes(4), 5, DeviceCommand.Off);

        Console.WriteLine("Initialization Complete");

        string command = "";
        while (command != "q")
        {
            command = Console.ReadLine() ?? "";
            if (command == "s")
            {
                var schedule = controller.GetCurrentScheduleItems();
                foreach (var item in schedule)
                {
                    Console.WriteLine($"{item.Info.EventTime:G} - {item.Info.TimeType} ({item.Info.RelativeOffset}), Device: {item.Device}, Command: {item.Command}");
                }
            }
            if (command == "r")
            {
                controller.ReloadSchedule();
            }
        }
    }

    private static HouseController InitializeHouseController()
    {
        //45.6382,-122.7013 = Vancouver, WA, USA
        //38.6530,-90.4084 = St. Louis, MO
        //38.7698,-90.5050 = Embassy Suites, St. Charles, MO

        var fileName = AppDomain.CurrentDomain.BaseDirectory + "ScheduleData";
        var sunsetProvider = new SolarServiceSunsetProvider(38.6530, -90.4084);
        var schedule = new Schedule(fileName, sunsetProvider);
        var controller = new HouseController(schedule);

        var sunset = sunsetProvider.GetSunset(DateTime.Today.AddDays(1));
        Console.WriteLine($"Sunset Tomorrow: {sunset:G}");

        return controller;
    }
}
