using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Logic
{
    public class DiscoveryChannel
    {
        private DiscoveryChannel()
        {

        }
        public int DiscoveryChannelId { get; internal set; }
        public string DiscoveryChannelName { get; internal set; }

        public static Result<DiscoveryChannel> CreateDiscoveryChannel(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<DiscoveryChannel>(Messages.NameIsRequired);

            var discoveryChannel = new DiscoveryChannel()
            {
                DiscoveryChannelName = name
            };

            return Result.Success(discoveryChannel);
        }

        public Result<DiscoveryChannel> UpdateDiscoveryChannel(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<DiscoveryChannel>(Messages.NameIsRequired);

            DiscoveryChannelName = name;

            return Result.Success(this);
        }

        public Result ValidateForDelete(List<int> usedIds)
        {
            if (usedIds.Contains(DiscoveryChannelId))
                return Result.Failure(Messages.DiscoveryChannelCanNotDeleteIsUsed);

            return Result.Success();
        }
    }
}
