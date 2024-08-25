using Forum.Application.MainTopics;
using Forum.Application.MainUsers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace BackGroundServices.BackGroundWorkers
{
    public class TopicWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private int _topicDays = 5;
        private int _userDays = 7;

        private string Schedule => "* * * */1 * *";

        public TopicWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                if (now > _nextRun)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var topicService = scope.ServiceProvider.GetRequiredService<ITopicService>();
                        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                        await Proccess(topicService, userService, stoppingToken, _topicDays, _userDays);
                    }
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
            }
        }

        private async Task Proccess(ITopicService topicService, IUserService userService, CancellationToken cancellationToken, int topicDays, int userDays)
        {
            var inactiveTopics = await topicService.GetOldTopicsAsync(cancellationToken, topicDays);

            foreach (var topic in inactiveTopics)
            {
                await topicService.ChangeStatusAsync(cancellationToken, topic.Id, false);
            }

            var bannedUsers = await userService.UnbanUsersAsync(cancellationToken, userDays);

            foreach (var user in bannedUsers)
            {
                await userService.BanUserAsync(user.Id, false);
            }
        }
    }
}