using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badges
{
    public class Badge_Repo
    {
        private Dictionary<int, List<string>> _repo = new Dictionary<int, List<string>>();

        //CREATE
        public bool AddBadge(Badge badge)
        {
            int startingCount = _repo.Count;
            _repo.Add(badge.BadgeID, badge.Doors);

            bool wasAdded = (_repo.Count > startingCount) ? true : false;
            return wasAdded;
        }
        // READ

        public Dictionary<int, List<string>> GetAllBadges()
        {
            return _repo;
        }

    }
}
