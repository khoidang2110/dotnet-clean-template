using Hangfire;
using Microsoft.Extensions.Configuration;
using System;

namespace galaxy_pay.infrastructure.Jobs;

public static class JobScheduler
{
    public static void ScheduleJobs(IConfiguration config)
    {
        int interval = config.GetValue<int>("JobSchedule:IntervalInSeconds");

        RecurringJob.AddOrUpdate(
            "log-message-job",
            () => Console.WriteLine($"🕒 Job chạy lúc {DateTime.Now}"),
            $"*/{interval} * * * * *" // mỗi X giây
        );
           // ✅ Job chạy 1 lần ngay khi app start
        BackgroundJob.Enqueue(() =>
            Console.WriteLine($"🚀 Job chạy ngay khi khởi động: {DateTime.Now}")
        );
    }
}
