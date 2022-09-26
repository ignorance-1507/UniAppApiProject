using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Application.Repository.Communitys.Dto
{
    public class OperationsRequest
    {
        public int UserId { get; set; }
        public int CommunityId { get; set; }
        public int Type { get; set; }
        public bool State { get; set; }
        public int FollowId { get; set; }
    }
}
