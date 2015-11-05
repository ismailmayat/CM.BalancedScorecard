namespace CM.BalancedScoreboard.Data.Migrations
{
    using Domain.Model.Users;
    using Domain.Model.Indicators;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Domain.Model.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<CM.BalancedScoreboard.Data.Repository.Implementation.BsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CM.BalancedScoreboard.Data.Repository.Implementation.BsContext context)
        {
            //  This method will be called after migrating to the latest version.

            var indicatorTypes = new List<IndicatorType>();
            var users = new List<User>();
            var indicators = new List<Indicator>();

            for (int index = 0; index < 100; index++)
            {
                if (index % 10 == 0)
                {
                    indicatorTypes.Add(CreateIndicatorType(index));
                    users.Add(CreateUser(index));
                }

                indicators.Add(CreateIndicator(index, indicatorTypes, users));
            }

            context.IndicatorTypes.AddOrUpdate(indicatorTypes.ToArray());
            context.Users.AddOrUpdate(users.ToArray());
            context.Indicators.AddOrUpdate(indicators.ToArray());
        }

        private IndicatorType CreateIndicatorType(int index)
        {
            return new IndicatorType()
            {
                Id = Guid.NewGuid(),
                Code = (index / 10 + 1).ToString("00"),
                Name = string.Format("Indicator Type {0}", (index / 10 + 1))
            };
        }

        private User CreateUser(int index)
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Username = string.Format("Username {0}", (index / 10 + 1)),
                Password = string.Format("Username {0}", (index / 10 + 1)),
                Active = true,
                Email = "a@a.com",
                Firstname = string.Format("Firstname {0}", (index / 10 + 1)),
                Surname = string.Format("Surname {0}", (index / 10 + 1))
            };
        }

        private Indicator CreateIndicator(int index, IList<IndicatorType> indicatorTypes, IList<User> users)
        {
            return new Indicator()
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Indicator {0}", (index + 1)),
                Description = string.Format("Indicator {0} Description", (index + 1)),
                Code = indicatorTypes[index / 10].Code + (index + 1).ToString("000"),
                Unit = "£",
                Active = true,
                ComparisonValueType = ComparisonValueType.Greater,
                StartDate = DateTime.Today,
                PeriodicityType = PeriodicityType.Month,
                IndicatorTypeId = indicatorTypes[index / 10].Id,
                ManagerId = users[index / 10].Id,
            };
        }
    }
}
